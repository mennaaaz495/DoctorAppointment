using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands.Create
{
    public class GenerateBillRequest
    {
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
