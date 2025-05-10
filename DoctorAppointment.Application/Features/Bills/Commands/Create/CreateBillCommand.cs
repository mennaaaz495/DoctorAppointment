using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands.Create
{
    public sealed record CreateBillCommand(BillDTO Bill) : IRequest<Result<int>>;
}