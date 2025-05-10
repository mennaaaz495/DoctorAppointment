using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.RazorPages.Attributes;

namespace DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments
{
    public class AppointmentWriteModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public int PatientId { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [Display(Name = "Staff")]
        public int? StaffId { get; set; }  // ✅ Add this line

        [Required]
        [MinDateToday]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public static implicit operator AppointmentWriteModel(Appointment appointment)
        {
            return new AppointmentWriteModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                StaffId = appointment.StaffId, // ✅ Add this line if StaffId exists in Appointment entity
                Date = appointment.Date,
                Status = appointment.Status
            };
        }

        public static implicit operator Appointment(AppointmentWriteModel model)
        {
            return new Appointment
            {
                Id = model.Id,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                StaffId = model.StaffId, // ✅ Add this line if needed
                Date = model.Date,
                Status = model.Status
            };
        }
    }
}
