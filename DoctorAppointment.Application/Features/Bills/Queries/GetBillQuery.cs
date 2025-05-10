using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Queries
{
    public sealed record GetBillQuery(int Id) : IRequest<Result<BillDTO>>;
}