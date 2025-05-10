using DoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Features.Bills.Dtos
{
    // In DoctorAppointment.Application/Features/Bills/DTOs/BillDTO.cs

    public record BillDTO
    {
        public int Id { get; init; }
        public int AppointmentId { get; init; }
        public decimal Amount { get; init; }
        public string Description { get; init; }
        public DateTime GeneratedDate { get; init; }
        public bool IsPaid { get; init; }
        public int GeneratedById { get; init; }

        public static implicit operator BillDTO(Bill bill) => new()
        {
            Id = bill.Id,
            AppointmentId = bill.AppointmentId,
            Amount = bill.Amount,
            Description = bill.Description,
            GeneratedDate = bill.GeneratedDate,
            IsPaid = bill.IsPaid,
            GeneratedById = bill.GeneratedById
        };
    }
}

