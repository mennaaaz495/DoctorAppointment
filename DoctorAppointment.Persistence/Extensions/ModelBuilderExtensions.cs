﻿using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsers();
            modelBuilder.SeedUserRoles();
            modelBuilder.SeedAppointments();
        }

        private static void SeedRoles(this ModelBuilder modelBuilder)
        {
            ApplicationRole adminRole = new()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Administrator can access and manage all data.",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            ApplicationRole patientRole = new()
            {
                Id = 2,
                Name = "Patient",
                NormalizedName = "PATIENT",
                Description = "Patient can make appointment requests.",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            ApplicationRole doctorRole = new()
            {
                Id = 3,
                Name = "Doctor",
                NormalizedName = "DOCTOR",
                Description = "Doctor can approve or decline appointments.",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            ApplicationRole staffRole = new() // Add Staff role here
            {
                Id = 4,
                Name = "Staff",
                NormalizedName = "STAFF",
                Description = "Staff can manage appointments and generate bills after doctor approval.",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<ApplicationRole>().HasData(adminRole, patientRole, doctorRole, staffRole); // Include Staff role
        }

        private static void SeedUsers(this ModelBuilder modelBuilder)
        {
            ApplicationUser adminUser = new()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Test",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            ApplicationUser patientUser = new()
            {
                Id = 2,
                FirstName = "Alexandra",
                LastName = "Valkova",
                UserName = "alexandra.valkova",
                NormalizedUserName = "ALEXANDRA.VALKOVA",
                Email = "alexandra.valkova@email.com",
                NormalizedEmail = "ALEXANDRA.VALKOVA@EMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            ApplicationUser doctorUser = new()
            {
                Id = 3,
                FirstName = "Diana",
                LastName = "Yosifova",
                UserName = "diana.yosifova",
                NormalizedUserName = "DIANA.YOSIFOVA",
                Email = "diana.yosifova@email.com",
                NormalizedEmail = "DIANA.YOSIFOVA@EMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Add Staff user here
            ApplicationUser staffUser = new()
            {
                Id = 4,
                FirstName = "John",
                LastName = "Doe",
                UserName = "john.doe",
                NormalizedUserName = "JOHN.DOE",
                Email = "john.doe@email.com",
                NormalizedEmail = "JOHN.DOE@EMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<ApplicationUser> hasher = new();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin");
            patientUser.PasswordHash = hasher.HashPassword(patientUser, "avalkova");
            doctorUser.PasswordHash = hasher.HashPassword(doctorUser, "dyosifova");
            staffUser.PasswordHash = hasher.HashPassword(staffUser, "johndoe"); // Staff user password

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser, patientUser, doctorUser, staffUser);
        }

        private static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            ApplicationUserRole adminUserRole = new()
            {
                RoleId = 1,
                UserId = 1
            };

            ApplicationUserRole patientUserRole = new()
            {
                RoleId = 2,
                UserId = 2
            };

            ApplicationUserRole doctorUserRole = new()
            {
                RoleId = 3,
                UserId = 3
            };

            ApplicationUserRole staffUserRole = new() // Add Staff user role
            {
                RoleId = 4,  // Staff role ID is 4
                UserId = 4   // Staff user ID is 4
            };

            modelBuilder.Entity<ApplicationUserRole>().HasData(adminUserRole, patientUserRole, doctorUserRole, staffUserRole); // Include Staff user role
        }

        private static void SeedAppointments(this ModelBuilder modelBuilder)
        {
            Appointment pending = new()
            {
                Id = 1,
                PatientId = 2,
                DoctorId = 3,
                Date = DateTime.Now.AddDays(3),
                Status = AppointmentStatus.Pending
            };

            Appointment approved = new()
            {
                Id = 2,
                PatientId = 2,
                DoctorId = 3,
                Date = DateTime.Now.AddDays(2),
                Status = AppointmentStatus.Approved
            };

            Appointment declined = new()
            {
                Id = 3,
                PatientId = 2,
                DoctorId = 3,
                Date = DateTime.Now.AddDays(1),
                Status = AppointmentStatus.Declined
            };

            modelBuilder.Entity<Appointment>().HasData(pending, approved, declined);
        }
    }
}
