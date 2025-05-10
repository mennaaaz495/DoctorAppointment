using System.Text.Json.Serialization;
using DoctorAppointment.API.Endpoints;
using DoctorAppointment.API.ExceptionHandlers;
using DoctorAppointment.Application;
using DoctorAppointment.Domain.Constants;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Persistence;
using Microsoft.OpenApi.Models;
using Syncfusion.Licensing;
using static DoctorAppointment.Domain.Constants.Database;
using static DoctorAppointment.Domain.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Register Syncfusion License
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCe0x3Q3xbf1x1ZFBMZFxbRn5PMyBoS35Rc0VmWXZedXBVRGhVU0d/VEBU");

// DB Context
builder.Services.AddDatabase(builder.Configuration.GetConnectionString(ConnectionStringName) ??
    throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found."));

// Identity, Authentication
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddIdentityStorageProvider();

// Authorization
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Roles.Admin, policy => policy.RequireRole(Roles.Admin))
    .AddPolicy(Roles.Patient, policy => policy.RequireRole(Roles.Patient))
    .AddPolicy(Roles.Doctor, policy => policy.RequireRole(Roles.Doctor))
    .AddPolicy(Roles.Staff, policy => policy.RequireRole(Roles.Staff));

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddUseCases();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("appointments", new OpenApiInfo
    {
        Version = "v1",
        Title = "Doctor Appointment API",
        Description = "Online registry for patients who want to make an appointment with their doctor."
    });
    options.SwaggerDoc("identity", new OpenApiInfo
    {
        Version = "v1",
        Title = "Doctor Appointment API - Identity",
        Description = "Online registry for patients who want to make an appointment with their doctor."
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Access Token must be provided.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/appointments/swagger.json", "appointments");
        options.SwaggerEndpoint("/swagger/identity/swagger.json", "identity");
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

// Minimal APIs
app.MapGroup("/identity")
   .MapIdentityApi<ApplicationUser>()
   .WithTags("Identity")
   .WithGroupName("identity")
   .WithOpenApi();

app.MapGroup("/appointments")
   .MapAppointmentsApi()
   .WithTags("Appointments")
   .WithGroupName("appointments")
   .RequireAuthorization()
   .WithOpenApi();

app.MapGroup("/bills")
   .MapBillsApi()
   .WithTags("Bills")
   .WithGroupName("bills")
   .RequireAuthorization()
   .WithOpenApi();

app.MapControllers();
app.MapRazorPages();

app.Run();