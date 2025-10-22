
using AS.UserManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AS.UserManagement.Infrastructure.IOC
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<AgendaSaludDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            return services;
        }
    }
}
