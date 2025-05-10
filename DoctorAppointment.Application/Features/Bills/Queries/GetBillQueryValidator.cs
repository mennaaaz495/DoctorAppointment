using DoctorAppointment.Application.Features.Bills.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands.Queries
{
    namespace DoctorAppointment.Application.Features.Bills.Queries.Get
    {
        public class GetBillQueryValidator : AbstractValidator<GetBillQuery>
        {
            public GetBillQueryValidator()
            {
                RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            }
        }
    }
}
