using DoctorAppointment.Application.Features.Bills.Commands.Create;
using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.RazorPages.Areas.Staff.Pages.GenerateBill
{
    [Authorize(Roles = Roles.Staff)]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public GenerateBillRequest Request { get; set; } = new GenerateBillRequest();

        public void OnGet(int appointmentId)
        {
            Request.AppointmentId = appointmentId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int staffId))
            {
                ModelState.AddModelError("", "Unable to determine staff identity.");
                return Page();
            }

            var billDto = new BillDTO
            {
                AppointmentId = Request.AppointmentId,
                Amount = Request.Amount,
                Description = Request.Description,
                GeneratedById = staffId
            };

            var result = await _mediator.Send(new CreateBillCommand(billDto));

            if (result.IsSuccess)
            {
                return RedirectToPage("BillGenerated", new { id = result.Value });
            }

            ModelState.AddModelError(string.Empty, result.Error.Description);
            return Page();
        }
    }

    public class GenerateBillRequest
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must be under 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
