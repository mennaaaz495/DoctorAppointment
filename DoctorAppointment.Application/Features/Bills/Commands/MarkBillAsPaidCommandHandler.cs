using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Commands
{
    public class MarkBillAsPaidCommandHandler : IRequestHandler<MarkBillAsPaidCommand, Result>
    {
        private readonly IBillService _billService;

        public MarkBillAsPaidCommandHandler(IBillService billService)
        {
            _billService = billService;
        }

        public async Task<Result> Handle(MarkBillAsPaidCommand request, CancellationToken cancellationToken)
        {
            var success = await _billService.MarkBillAsPaidAsync(request.Id);
            return success ? Result.Success() : Result.Failure(new Error("Bill", "NotFound", "Bill not found"));
        }
    }
}