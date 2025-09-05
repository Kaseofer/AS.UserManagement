using AgendaSaludApp.Infrastructure.Persistence.Repositories;
using AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Infrastructure.IOC
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            // Aquí puedes registrar tus servicios de infraestructura, como repositorios, contextos de base de datos, etc.
            // services.AddScoped<IYourRepository, YourRepositoryImplementation>();
            // Repositorios

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
            services.AddScoped<IPacienteRespository, PacienteRepository>();

            return services;
        }
    }
}
