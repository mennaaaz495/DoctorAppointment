using DoctorAppointment.Application.Features.Bills.Commands.Create;
using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Result<int>>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IBillService _billService;
    private readonly IUserPolicy _userPolicy;

    public CreateBillCommandHandler(IAppointmentService appointmentService, IBillService billService, IUserPolicy userPolicy)
    {
        _appointmentService = appointmentService;
        _billService = billService;
        _userPolicy = userPolicy;
    }

    public async Task<Result<int>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(request.Bill.AppointmentId);
        if (appointment == null || appointment.Status != AppointmentStatus.Approved)
        {
            return Result.Failure<int>(AppointmentErrors.AppointmentNotFound(request.Bill.AppointmentId));
        }

        var staffResult = await _userPolicy.CheckIfUserInRoleExists(request.Bill.GeneratedById, Roles.Staff);
        if (staffResult.IsFailure)
        {
            return Result.Failure<int>(staffResult.Error);
        }

        var bill = new Bill
        {
            AppointmentId = request.Bill.AppointmentId,
            Amount = request.Bill.Amount,
            Description = request.Bill.Description,
            GeneratedDate = DateTime.Now,
            IsPaid = false,
            GeneratedById = request.Bill.GeneratedById
        };

        int createdId = await _billService.CreateBillAsync(bill);
        return Result.Success(createdId);
    }
}