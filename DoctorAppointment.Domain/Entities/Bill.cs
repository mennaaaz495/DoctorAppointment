using DoctorAppointment.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// In DoctorAppointment.Domain/Entities/Bill.cs
namespace DoctorAppointment.Domain.Entities
{
   public class Bill : Entity
{
    public int AppointmentId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime GeneratedDate { get; set; }
    public bool IsPaid { get; set; }

    public int GeneratedById { get; set; }  // Likely a staff ID

    public virtual Appointment Appointment { get; set; }
    public virtual ApplicationUser GeneratedBy { get; set; }
}

}