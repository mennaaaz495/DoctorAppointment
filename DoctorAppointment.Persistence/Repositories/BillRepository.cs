using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Repositories.DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.Repositories
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(DoctorAppointmentContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Bill>> GetBillsByAppointmentAsync(int appointmentId)
        {
            return await dbSet.Where(b => b.AppointmentId == appointmentId).ToListAsync();
        }

        // Fix the method signature to accept int (not string)
        public async Task<IReadOnlyList<Bill>> GetBillsByPatientAsync(int patientId)
        {
            return await dbSet
                .Where(b => b.Appointment.PatientId == patientId)  // Assuming Appointment has a PatientId
                .ToListAsync();
        }
    }

}
