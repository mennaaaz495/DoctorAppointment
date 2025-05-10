// In DoctorAppointment.Persistence/UnitOfWork.cs
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Repositories.DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;
using System;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DoctorAppointmentContext _context;

        public IAppointmentRepository AppointmentRepository { get; }
        public IBillRepository BillRepository { get; }

        public UnitOfWork(
            DoctorAppointmentContext context,
            IAppointmentRepository appointmentRepository,
            IBillRepository billRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            AppointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            BillRepository = billRepository ?? throw new ArgumentNullException(nameof(billRepository));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}