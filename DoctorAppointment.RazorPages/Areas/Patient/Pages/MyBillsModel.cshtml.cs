using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace DoctorAppointment.RazorPages.Areas.Patient.Pages
{
    // Areas/Patient/Pages/MyBills.cshtml.cs
    [Authorize(Roles = Roles.Patient)]
    public class MyBillsModel : PageModel
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;

        // Keep the type as List<Bill>
        public List<Bill> Bills { get; set; }

        public MyBillsModel(IBillService billService, UserManager<ApplicationUser> userManager)
        {
            _billService = billService;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            // Convert IReadOnlyList<Bill> to List<Bill>
            Bills = (await _billService.GetBillsByPatientAsync(user.Id)).ToList();
        }
    }

}
