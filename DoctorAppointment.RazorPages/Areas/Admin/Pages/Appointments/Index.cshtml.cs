using System.Security.Claims;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Appointments
{
    [Authorize(Roles = "Admin,Staff,Doctor")]
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IList<AppointmentReadModel> Appointments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var allAppointments = await _appointmentService.ListAppointmentsAsync();

            // Filter if the user is a Doctor
            // Filter if the user is a Doctor
            if (userRole == "Doctor" && int.TryParse(userId, out int doctorId))
            {
                Appointments = allAppointments
                    .Where(a => a.DoctorId == doctorId)
                    .Select(a => new AppointmentReadModel { /* map fields */ })
                    .ToList();
            }
            else
            {
                Appointments = (await _appointmentService.ListAppointmentsAsync()).ToList().ConvertAll<AppointmentReadModel>(a => a);
            }
        }
    }
}
