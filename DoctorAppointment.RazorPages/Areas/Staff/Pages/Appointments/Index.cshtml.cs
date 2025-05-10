using DoctorAppointment.Application.Features.Bills.Commands.Create;
using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using DoctorAppointment.Application.Features.Bills.Queries;
using System.Security.Claims;

namespace DoctorAppointment.RazorPages.Areas.Staff.Pages.Appointments
{
    [Area("Staff")]
    [Authorize(Roles = Roles.Staff)]
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IBillService _billService;
        private readonly IMediator _mediator;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IAppointmentService appointmentService,
            IBillService billService,
            IMediator mediator,
            ILogger<IndexModel> logger)
        {
            _appointmentService = appointmentService;
            _billService = billService;
            _mediator = mediator;
            _logger = logger;
        }

        public List<Appointment> ApprovedAppointments { get; set; } = [];

        public async Task OnGetAsync()
        {
            ApprovedAppointments = (await _appointmentService
                .ListAppointmentsAsync(AppointmentStatus.Approved))
                .OrderBy(a => a.Date)
                .ToList();
        }

        public async Task<IActionResult> OnPostGeneratePdfAsync(int appointmentId)
        {
            _logger.LogInformation("Generating PDF for appointmentId: {AppointmentId}", appointmentId);

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("User not authenticated or invalid user ID: {UserId}", userId);
                    TempData["ErrorMessage"] = "User not authenticated.";
                    return RedirectToPage();
                }

                var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
                if (appointment == null || appointment.Status != AppointmentStatus.Approved)
                {
                    _logger.LogError("Appointment not found or not approved: {AppointmentId}", appointmentId);
                    TempData["ErrorMessage"] = "Appointment not found or not approved.";
                    return RedirectToPage();
                }

                // Check for existing bill or create a new one
                var existingBills = await _mediator.Send(new GetBillsByAppointmentQuery(appointmentId));
                Bill bill;
                if (existingBills.IsSuccess && existingBills.Value.Any())
                {
                    bill = await _billService.GetBillByIdAsync(existingBills.Value.First().Id);
                }
                else
                {
                    var billResult = await _mediator.Send(new CreateBillCommand(
                        new BillDTO
                        {
                            AppointmentId = appointmentId,
                            Amount = 100.00m,
                            Description = $"Consultation for appointment {appointmentId}",
                          //  GeneratedById = userId // Uncommented and fixed
                        }));

                    if (!billResult.IsSuccess)
                    {
                        _logger.LogError("Bill creation failed: {Error}", billResult.Error.ToString());
                        TempData["ErrorMessage"] = $"Failed to create bill: {billResult.Error.Description}";
                        return RedirectToPage();
                    }

                    bill = await _billService.GetBillByIdAsync(billResult.Value);
                }

                // Generate PDF using Syncfusion
                using PdfDocument document = new();
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 40;

                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                // Header
                PdfFont headingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16, PdfFontStyle.Bold);
                graphics.DrawString($"Invoice #{bill.Id}", headingFont, PdfBrushes.Black, new PointF(10, 10));

                // Invoice details
                PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                float yPosition = 40;
                graphics.DrawString($"Date: {bill.GeneratedDate:MM/dd/yyyy}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
                graphics.DrawString($"Generated By: User #{bill.GeneratedById}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 20));

                // Appointment details
                yPosition += 50;
                graphics.DrawString("Appointment Details", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
                yPosition += 20;
                graphics.DrawString($"Appointment ID: {appointment.Id}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
                graphics.DrawString($"Date: {appointment.Date:MM/dd/yyyy}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 20));
                graphics.DrawString($"Patient ID: {appointment.PatientId}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 40));
                graphics.DrawString($"Doctor ID: {appointment.DoctorId}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 60));

                // Billing table
                yPosition += 100;
                PdfGrid pdfGrid = new();
                DataTable table = new();
                table.Columns.Add("Description");
                table.Columns.Add("Amount");
                table.Rows.Add(bill.Description, bill.Amount.ToString("C"));
                pdfGrid.DataSource = table;

                PdfGridCellStyle headerStyle = new()
                {
                    BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)),
                    TextBrush = PdfBrushes.White,
                    Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Bold)
                };
                pdfGrid.Headers[0].ApplyStyle(headerStyle);

                PdfGridCellStyle cellStyle = new()
                {
                    Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12),
                    TextBrush = new PdfSolidBrush(new PdfColor(0, 0, 0))
                };
                foreach (var row in pdfGrid.Rows)
                {
                    row.ApplyStyle(cellStyle);
                }

                pdfGrid.Draw(page, new PointF(10, yPosition));

                // Payment status
                yPosition += 60;
                graphics.DrawString($"Payment Status: {(bill.IsPaid ? "Paid" : "Unpaid")}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));

                // Save and return PDF
                MemoryStream stream = new();
                document.Save(stream);
                stream.Position = 0;

                _logger.LogInformation("PDF generated successfully for bill ID: {BillId}", bill.Id);
                return File(stream.ToArray(), "application/pdf", $"Invoice_{bill.Id}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF for appointmentId: {AppointmentId}", appointmentId);
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}