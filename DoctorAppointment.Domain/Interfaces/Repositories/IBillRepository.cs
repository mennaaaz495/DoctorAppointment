using DoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Interfaces.Repositories
{
    namespace DoctorAppointment.Domain.Interfaces.Repositories
    {
        public interface IBillRepository : IRepository<Bill>
        {
            Task<IReadOnlyList<Bill>> GetBillsByAppointmentAsync(int appointmentId);

            Task<IReadOnlyList<Bill>> GetBillsByPatientAsync(int patientId);  // Change to int
        }

    }

}
