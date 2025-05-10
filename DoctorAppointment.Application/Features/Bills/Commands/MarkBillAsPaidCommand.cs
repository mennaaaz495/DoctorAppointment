using DoctorAppointment.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands
{
    public record MarkBillAsPaidCommand(int Id) : IRequest<Result>;
}
