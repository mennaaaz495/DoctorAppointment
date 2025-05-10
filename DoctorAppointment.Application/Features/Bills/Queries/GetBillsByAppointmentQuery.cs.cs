using DoctorAppointment.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoctorAppointment.Application.Features.Bills.Dtos;

namespace DoctorAppointment.Application.Features.Bills.Queries
{
    public record GetBillsByAppointmentQuery(int AppointmentId) : IRequest<Result<List<BillDTO>>>;
}