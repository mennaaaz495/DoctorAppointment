using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Repositories.DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// In DoctorAppointment.Application/Services/BillService.cs
namespace DoctorAppointment.Application.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _repository;

        public BillService(IBillRepository repository)
        {
            _repository = repository;
        }

        public async Task<Bill?> GetBillByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Bill>> GetBillsByAppointmentAsync(int appointmentId)
        {
            return await _repository.GetBillsByAppointmentAsync(appointmentId);
        }

        public async Task<int> CreateBillAsync(Bill bill)
        {
            return await _repository.AddAsync(bill);
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            await _repository.EditAsync(bill);
        }

        public async Task<bool> MarkBillAsPaidAsync(int id)
        {
            if (await _repository.GetByIdAsync(id) is Bill bill)
            {
                bill.IsPaid = true;
                await _repository.EditAsync(bill);
                return true;
            }
            return false;
        }
        public async Task<IReadOnlyList<Bill>> GetBillsByPatientAsync(int patientId)
        {
            return await _repository.GetBillsByPatientAsync(patientId);
        }


    }
}