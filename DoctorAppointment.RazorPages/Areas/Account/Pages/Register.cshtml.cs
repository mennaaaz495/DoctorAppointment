using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Account.Models;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Account.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public RegistrationModel Registration { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Registration is null)
            {
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Registration.Username,
                Email = Registration.Email,
                FirstName = Registration.FirstName,
                LastName = Registration.LastName
            };

            // Create user
            var result = await _userManager.CreateAsync(user, Registration.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelIdentityErrors(result);
                return Page();
            }

          
            await _userManager.AddToRoleAsync(user, "Patient");

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToPage("/Appointments/Index", new { area = "Patient" });
        }
    }
}