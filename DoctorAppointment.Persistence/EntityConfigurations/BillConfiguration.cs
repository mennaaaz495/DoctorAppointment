using DoctorAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EntityConfigurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(b => b.Amount).HasColumnType("decimal(18,2)");
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.GeneratedDate).IsRequired();

            builder.HasOne(b => b.Appointment)
                   .WithMany()
                   .HasForeignKey(b => b.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.GeneratedBy)
                   .WithMany()
                   .HasForeignKey(b => b.GeneratedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}