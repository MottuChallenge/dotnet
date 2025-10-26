using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Application.Configurations;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Infrastructure.Persistence;
using MottuChallenge.Infrastructure.Repositories;
using MottuChallenge.Infrastructure.Services;

namespace MottuChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddDbContext(this IServiceCollection services, ConnectionSettings connectionSettings)
        {
            var connectionString = connectionSettings.MysqlConnection;
            services.AddDbContext<MottuChallengeContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions =>
                { 
                    mysqlOptions.EnableRetryOnFailure();
                });
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IYardRepository, YardRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<ISectorTypeRepository, SectorTypeRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAddressProvider, FindAddressByApiViaCep>();
            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Settings settings)
        {
            services.AddDbContext(settings.ConnectionStrings);
            services.AddRepositories();
            services.AddServices();
            return services;
        }

    }
}
