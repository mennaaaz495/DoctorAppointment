using DoctorAppointment.Application.Features.Bills.Commands;
using DoctorAppointment.Application.Features.Bills.Commands.Create;
using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Application.Features.Bills.Queries;
using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.IO;
using static DoctorAppointment.Domain.Constants.Roles;

namespace DoctorAppointment.API.Endpoints
{
    public static class BillEndpoints
    {
        public static RouteGroupBuilder MapBillsApi(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateBill)
                .WithName(nameof(CreateBill))
                .RequireAuthorization(Staff)
                .WithSummary("Create a new bill for an approved appointment");

            group.MapGet("/{id:int}", GetBillById)
                .WithName(nameof(GetBillById))
                .WithSummary("Get a bill by ID");

            group.MapGet("/appointment/{appointmentId:int}", GetBillsByAppointment)
                .WithName(nameof(GetBillsByAppointment))
                .WithSummary("Get all bills for an appointment");

            group.MapGet("/{id:int}/pdf", GeneratePdfInvoice)
                .WithName(nameof(GeneratePdfInvoice))
                .RequireAuthorization(Staff)
                .WithSummary("Generate PDF invoice for a bill");

            group.MapPatch("/{id:int}/pay", MarkBillAsPaid)
                .WithName(nameof(MarkBillAsPaid))
                .RequireAuthorization(Patient)
                .WithSummary("Mark a bill as paid");

            return group;
        }

        private static async Task<Results<CreatedAtRoute<int>, ProblemHttpResult>> CreateBill(
            HttpContext httpContext,
            ISender mediatr,
            [FromBody] BillDTO bill)
        {
            var result = await mediatr.Send(new CreateBillCommand(bill));

            if (result.IsSuccess)
            {
                return TypedResults.CreatedAtRoute(
                    result.Value,
                    nameof(GetBillById),
                    new { id = result.Value });
            }

            int statusCode = result.Error.Code switch
            {
                AppointmentErrors.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status400BadRequest
            };

            return TypedResults.Problem(
                result.Error.Description,
                httpContext.Request.Path,
                statusCode);
        }

        private static async Task<Results<Ok<BillDTO>, NotFound>> GetBillById(
            ISender mediatr,
            [FromRoute] int id)
        {
            var result = await mediatr.Send(new GetBillQuery(id));
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound();
        }

        private static async Task<Results<Ok<List<BillDTO>>, NotFound>> GetBillsByAppointment(
            ISender mediatr,
            [FromRoute] int appointmentId)
        {
            var result = await mediatr.Send(new GetBillsByAppointmentQuery(appointmentId));
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound();
        }

        private static async Task<Results<NoContent, NotFound>> MarkBillAsPaid(
            ISender mediatr,
            [FromRoute] int id)
        {
            var result = await mediatr.Send(new MarkBillAsPaidCommand(id));
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        }

        private static async Task<Results<FileContentHttpResult, NotFound, ProblemHttpResult>> GeneratePdfInvoice(
            HttpContext httpContext,
            [FromServices] IBillService billService,
            [FromServices] IAppointmentService appointmentService,
            [FromRoute] int id)
        {
            // Fetch the bill
            var bill = await billService.GetBillByIdAsync(id);
            if (bill == null)
            {
                return TypedResults.NotFound();
            }

            // Fetch the associated appointment
            var appointment = await appointmentService.GetAppointmentByIdAsync(bill.AppointmentId);
            if (appointment == null)
            {
                return TypedResults.Problem("Associated appointment not found.", httpContext.Request.Path, StatusCodes.Status404NotFound);
            }

            // Create a new PDF document
            using PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 40;

            // Add a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            // Add a header (e.g., Invoice Title)
            PdfFont headingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16, PdfFontStyle.Bold);
            graphics.DrawString($"Invoice #{bill.Id}", headingFont, PdfBrushes.Black, new PointF(10, 10));

            // Add invoice details (e.g., Date, Generated By)
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            float yPosition = 40;
            graphics.DrawString($"Date: {bill.GeneratedDate:MM/dd/yyyy}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
            graphics.DrawString($"Generated By: User #{bill.GeneratedById}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 20));

            // Add appointment details
            yPosition += 50;
            graphics.DrawString("Appointment Details", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
            yPosition += 20;
            graphics.DrawString($"Appointment ID: {appointment.Id}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));
            graphics.DrawString($"Date: {appointment.Date:MM/dd/yyyy}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 20));
            graphics.DrawString($"Patient ID: {appointment.PatientId}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 40));
            graphics.DrawString($"Doctor ID: {appointment.DoctorId}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition + 60));

            // Add billing details as a table
            yPosition += 100;
            PdfGrid pdfGrid = new PdfGrid();
            DataTable table = new DataTable();
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            table.Rows.Add(bill.Description, bill.Amount.ToString("C"));
            pdfGrid.DataSource = table;

            // Style the table
            PdfGridCellStyle headerStyle = new PdfGridCellStyle
            {
                BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173)),
                TextBrush = PdfBrushes.White,
                Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Bold)
            };
            pdfGrid.Headers[0].ApplyStyle(headerStyle);

            PdfGridCellStyle cellStyle = new PdfGridCellStyle
            {
                Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12),
                TextBrush = new PdfSolidBrush(new PdfColor(0, 0, 0))
            };
            foreach (var row in pdfGrid.Rows)
            {
                row.ApplyStyle(cellStyle);
            }

            pdfGrid.Draw(page, new PointF(10, yPosition));

            // Add payment status
            yPosition += 60;
            graphics.DrawString($"Payment Status: {(bill.IsPaid ? "Paid" : "Unpaid")}", subHeadingFont, PdfBrushes.Black, new PointF(10, yPosition));

            // Save the PDF to a MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            // Return the PDF as a file
            return TypedResults.File(
                stream.ToArray(),
                contentType: "application/pdf",
                fileDownloadName: $"Invoice_{bill.Id}.pdf");
        }
    }
}