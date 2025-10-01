using AS.UserManagement.Application.Services;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AS.UserManagement.Application.IOC
{
    public static class AddApplicationServices
    {
        public static  IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Pacientes

            services.AddScoped<IMedicalSpecialtyService, MedicalSpecialtyService>();

            services.AddScoped<IHealthInsuranceService, HealthInsuranceService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IProfessionalService, ProfessionalService>();
            services.AddScoped<IProfessionalScheduleService, ProfessionalScheduleService>();



            return services;
        }
    }
}
