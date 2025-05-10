using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Repositories.DoctorAppointment.Domain.Interfaces.Repositories;

using System;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository AppointmentRepository { get; }
        IBillRepository BillRepository { get; } 
        Task SaveAsync();
    }
}