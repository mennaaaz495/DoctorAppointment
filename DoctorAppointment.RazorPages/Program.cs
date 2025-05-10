using DoctorAppointment.Application;
using DoctorAppointment.Application.Features.GeneratePdf;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Repositories.DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence;
using DoctorAppointment.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static DoctorAppointment.Domain.Constants.Database;
using static DoctorAppointment.Domain.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDatabase(builder.Configuration.GetConnectionString(ConnectionStringName) ??
    throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found."));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
}).AddIdentityStorageProvider();

// Repositories and Services
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddServices();

// Fixed MediatR registration (single call with all assemblies)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<DoctorAppointment.Application.Features.Bills.Commands.Create.CreateBillCommand>();
    cfg.RegisterServicesFromAssembly(typeof(GeneratePdfInvoiceQueryHandler).Assembly);
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
});

// Configure Razor Pages area-based authorization
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Patient", "/", "PatientOnly");
    options.Conventions.AuthorizeAreaFolder("Doctor", "/", "DoctorOnly");
   // options.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminOnly");
    options.Conventions.AuthorizeAreaFolder("Staff", "/", "StaffOnly");

    options.Conventions.AllowAnonymousToAreaPage("Account", "/Login");
    options.Conventions.AllowAnonymousToAreaPage("Account", "/Register");
});

builder.Services.AddServerSideBlazor();

var app = builder.Build();

// HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();