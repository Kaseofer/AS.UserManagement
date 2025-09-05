using AgendaSaludApp.Application.Services;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaSaludApp.Application.IOC
{
    public static class AddApplicationServices
    {
        public static  IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Servicios específicos
            services.AddScoped<IPacientesService, PacientesService>();
            services.AddScoped<ITurnosService, TurnosService>();
            services.AddScoped<IProfesionalesService, ProfesionalesService>();

            return services;
        }
    }
}
