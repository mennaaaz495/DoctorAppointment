using DoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Interfaces.Services
{
    public interface IBillService
    {
        Task<Bill?> GetBillByIdAsync(int id);
        Task<IReadOnlyList<Bill>> GetBillsByAppointmentAsync(int appointmentId);
        Task<IReadOnlyList<Bill>> GetBillsByPatientAsync(int patientId);
        Task<int> CreateBillAsync(Bill bill);
        Task UpdateBillAsync(Bill bill);
        Task<bool> MarkBillAsPaidAsync(int id);
    }

}