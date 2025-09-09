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
            services.AddScoped<IAgendaCitasService, AgendaCitasService>();
            services.AddScoped<IEspecialidadService, EspecialidadService>();
            services.AddScoped<IEstadoCitaService, EstadoCitaService>();
            services.AddScoped<IMotivoCitaService, MotivoCitaService>();
            services.AddScoped<IObraSocialService, ObraSocialService>();
            services.AddScoped<IPacientesService, PacientesService>();
            services.AddScoped<IProfesionalesService, ProfesionalesService>();
            services.AddScoped<IProfesionalHorariosService, ProfesionalHorariosService>();



            return services;
        }
    }
}
