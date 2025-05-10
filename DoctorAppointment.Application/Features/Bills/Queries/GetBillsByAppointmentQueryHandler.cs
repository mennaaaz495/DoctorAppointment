using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using System.Collections.Generic;

namespace DoctorAppointment.Application.Features.Bills.Queries
{
    public class GetBillsByAppointmentQueryHandler : IRequestHandler<GetBillsByAppointmentQuery, Result<List<BillDTO>>>
    {
        private readonly IBillService _billService;

        public GetBillsByAppointmentQueryHandler(IBillService billService)
        {
            _billService = billService;
        }

        public async Task<Result<List<BillDTO>>> Handle(GetBillsByAppointmentQuery request, CancellationToken cancellationToken)
        {
            var bills = await _billService.GetBillsByAppointmentAsync(request.AppointmentId);
            var billDTOs = bills.Select(b => (BillDTO)b).ToList();
            return Result.Success(billDTOs);

        }
    }
}
