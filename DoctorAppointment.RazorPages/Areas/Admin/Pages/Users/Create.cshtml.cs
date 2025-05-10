using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateModel(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public UserModel ApplicationUser { get; set; } = default!;

        public MultiSelectList Roles { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Roles")]
        public IEnumerable<string> UserRoles { get; set; } = default!;

        public IActionResult OnGet()
        {
            PopulateRoles();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || ApplicationUser is null || UserRoles is null)
            {
                PopulateRoles();
                return Page();
            }

            var user = (ApplicationUser)ApplicationUser;

            var createUserResult = await _userManager.CreateAsync(user, ApplicationUser.Password);
            if (!createUserResult.Succeeded)
            {
                ModelState.AddModelIdentityErrors(createUserResult);
                PopulateRoles();
                return Page();
            }

            if (UserRoles.Any())
            {
                var addRolesResult = await _userManager.AddToRolesAsync(user, UserRoles);
                if (!addRolesResult.Succeeded)
                {
                    ModelState.AddModelIdentityErrors(addRolesResult);
                    PopulateRoles();
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        private void PopulateRoles()
        {
            Roles = _roleManager.Roles.ToMultiSelectList();
        }
    }
}
