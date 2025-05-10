using DoctorAppointment.Application.Features.Bills.Dtos;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Queries
{
    public class GetBillQueryHandler : IRequestHandler<GetBillQuery, Result<BillDTO>>
    {
        private readonly IBillService _billService;

        public GetBillQueryHandler(IBillService billService)
        {
            _billService = billService;
        }

        public async Task<Result<BillDTO>> Handle(GetBillQuery request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetBillByIdAsync(request.Id);
            return bill != null
                ? Result.Success((BillDTO)bill)
                : Result.Failure<BillDTO>(new Error("Bill", "NotFound", $"Bill with ID {request.Id} not found"));
        }
    }
}
