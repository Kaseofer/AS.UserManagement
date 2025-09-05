using AgendaSaludApp.Application.Services;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AgendaSaludApp.Application.Validators;

namespace AgendaSaludApp.Application.IOC
{
    public static class AddApplicationServices
    {
        public static  IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Pacientes
            services.AddScoped<IPacientesService, PacientesService>();
            services.AddScoped<ICredencialService, CredencialService>();


            services.AddScoped<ITurnosService, TurnosService>();
            services.AddScoped<IProfesionalesService, ProfesionalesService>();

            

            return services;
        }
    }
}
