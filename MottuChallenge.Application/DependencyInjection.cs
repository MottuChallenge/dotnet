using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Application.UseCases.Sectors;
using MottuChallenge.Application.UseCases.SectorTypes;
using MottuChallenge.Application.UseCases.Spots;
using MottuChallenge.Application.UseCases.Yards;

namespace MottuChallenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<FindAddressByCepUseCase>();
            services.AddScoped<FindAddressByIdUseCase>();
            services.AddScoped<CreateSectorUseCase>();
            services.AddScoped<GetAllSectorsUseCase>();
            services.AddScoped<GetSectorByIdUseCase>();
            services.AddScoped<CreateSectorTypeUseCase>();
            services.AddScoped<DeleteSectorTypeUseCase>();
            services.AddScoped<GetAllSectorTypesUseCase>();
            services.AddScoped<UpdateSectorTypeUseCase>();
            services.AddScoped<GenerateSpotsUseCase>();
            services.AddScoped<CreateYardUseCase>();
            services.AddScoped<GetAllYardsUseCase>();
            services.AddScoped<GetYardByIdUseCase>();
            services.AddScoped<GetYardEntityByIdUseCase>();
            services.AddScoped<GetSectorTypeByIdUseCase>();

            return services;
        }
    }
}
