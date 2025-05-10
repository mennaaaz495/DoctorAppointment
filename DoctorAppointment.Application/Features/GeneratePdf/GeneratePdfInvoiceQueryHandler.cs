using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.GeneratePdf
{
    public class GeneratePdfInvoiceQueryHandler : IRequestHandler<GeneratePdfInvoiceQuery, Result<PdfInvoiceResponse>>
    {
        private readonly IBillService _billService;
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<GeneratePdfInvoiceQueryHandler> _logger;

        public GeneratePdfInvoiceQueryHandler(
            IBillService billService,
            IAppointmentService appointmentService,
            ILogger<GeneratePdfInvoiceQueryHandler> logger)
        {
            _billService = billService;
            _appointmentService = appointmentService;
            _logger = logger;
        }

        public async Task<Result<PdfInvoiceResponse>> Handle(
            GeneratePdfInvoiceQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // 1. Get the bill
                var bill = await _billService.GetBillByIdAsync(request.BillId);
                if (bill == null)
                {
                    _logger.LogWarning("Bill with ID {BillId} not found", request.BillId);
                    return Result.Failure<PdfInvoiceResponse>(new Error(
                        "Bill",
                        "NotFound",
                        $"Bill with Id {request.BillId} cannot be found!"));
                }

                // 2. Get and validate the appointment
                var appointment = await _appointmentService.GetAppointmentByIdAsync(bill.AppointmentId);
                if (appointment == null)
                {
                    _logger.LogWarning("Appointment not found for bill {BillId}", request.BillId);
                    return Result.Failure<PdfInvoiceResponse>(AppointmentErrors.AppointmentNotFound(bill.AppointmentId));
                }

                if (appointment.Status != AppointmentStatus.Approved)
                {
                    _logger.LogWarning("Appointment {AppointmentId} not approved (Status: {Status})",
                        appointment.Id, appointment.Status);
                    return Result.Failure<PdfInvoiceResponse>(new Error(
                        "Appointment",
                        "NotApproved",
                        $"Appointment with Id {appointment.Id} is not approved!"));
                }

                // 3. Generate PDF
                QuestPDF.Settings.License = LicenseType.Community;
                var invoiceBytes = GeneratePdfContent(bill, appointment);

                return Result.Success(new PdfInvoiceResponse(
                    invoiceBytes,
                    $"Invoice_{bill.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF for bill {BillId}", request.BillId);
                return Result.Failure<PdfInvoiceResponse>(new Error(
                    "PDF",
                    "GenerationError",
                    $"Failed to generate PDF invoice: {ex.Message}"));
            }
        }

        private static byte[] GeneratePdfContent(BillDTO bill, Appointment appointment)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // Header
                    page.Header()
                        .Column(col =>
                        {
                            col.Item().AlignCenter().Text("MEDICAL INVOICE")
                                .Bold().FontSize(20).FontColor(Colors.Blue.Darken3);

                            col.Item().PaddingTop(10).Row(row =>
                            {
                                row.RelativeItem().Text($"Invoice #: INV-{bill.Id:D5}");
                                row.RelativeItem().AlignRight().Text($"Date: {DateTime.Now:d}");
                            });
                        });

                    // Content
                    page.Content()
                        .PaddingVertical(10)
                        .Column(col =>
                        {
                            // Patient/Doctor Info
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Text(text =>
                                {
                                    text.Span("Patient: ").Bold();
                                    text.Span(appointment.Patient?.FullName ?? "N/A");
                                });
                                row.RelativeItem().AlignRight().Text(text =>
                                {
                                    text.Span("Doctor: ").Bold();
                                    text.Span(appointment.Doctor?.FullName ?? "N/A");
                                });
                            });

                            // Appointment Date
                            col.Item().PaddingBottom(15).Text(text =>
                            {
                                text.Span("Appointment Date: ").Bold();
                                text.Span(appointment.Date.ToString("f"));
                            });

                            // Invoice Items
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.ConstantColumn(25);
                                    cols.RelativeColumn(3);
                                    cols.ConstantColumn(100);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Description").Bold();
                                    header.Cell().AlignRight().Text("Amount").Bold();
                                    header.Cell().ColumnSpan(3).PaddingTop(5).BorderBottom(1);
                                });

                                table.Cell().Text("1");
                                table.Cell().Text(bill.Description);
                                table.Cell().AlignRight().Text($"{bill.Amount:C}");

                                table.Footer(footer =>
                                {
                                    footer.Cell().ColumnSpan(2).AlignRight().Text("TOTAL:").Bold();
                                    footer.Cell().AlignRight().Text($"{bill.Amount:C}").Bold();
                                });
                            });
                        });

                    // Footer
                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Thank you for choosing our clinic! ");
                            text.Span("Generated: ").FontColor(Colors.Grey.Medium);
                            text.Span(DateTime.Now.ToString("g"));
                        });
                });
            }).GeneratePdf();
        }
    }
}