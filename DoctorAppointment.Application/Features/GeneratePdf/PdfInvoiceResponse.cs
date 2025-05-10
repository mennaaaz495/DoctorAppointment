using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.GeneratePdf
{
    public record PdfInvoiceResponse(byte[] PdfBytes, string FileName);
}
