using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands.Create
{
    namespace DoctorAppointment.Application.Features.Bills.Commands.Create
    {
        public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
        {

                public CreateBillCommandValidator()
                {
                    RuleFor(x => x.Bill.AppointmentId).NotEmpty().GreaterThan(0);
                    RuleFor(x => x.Bill.Amount).NotEmpty().GreaterThan(0);
                    RuleFor(x => x.Bill.Description).NotEmpty().MaximumLength(500);
                    RuleFor(x => x.Bill.GeneratedById).NotEmpty().GreaterThan(0);
                }
            }
        }
    
}
