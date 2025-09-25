using AS.UserManegement.Application.Services;
using AS.UserManegement.Application.Services.Intefaces;
using Microsoft.Extensions.DependencyInjection;

namespace AS.UserManegement.Application.IOC
{
    public static class AddApplicationServices
    {
        public static  IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Pacientes

            services.AddScoped<IEspecialidadService, EspecialidadService>();

            services.AddScoped<IObraSocialService, ObraSocialService>();
            services.AddScoped<IPacientesService, PacientesService>();
            services.AddScoped<IProfesionalesService, ProfesionalesService>();
            services.AddScoped<IProfesionalHorariosService, ProfesionalHorariosService>();



            return services;
        }
    }
}
