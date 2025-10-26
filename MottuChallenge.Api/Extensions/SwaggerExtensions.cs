using Microsoft.OpenApi.Models;
using MottuChallenge.Application.Configurations;

namespace MottuChallenge.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettings settings)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = settings.Title,
                Version = settings.Version,
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name = settings.Contact.Name,
                    Email = settings.Contact.Email,
                    Url = settings.Contact.Url
                }
            });
            
            swagger.EnableAnnotations();
        });
        return services;
    }
}