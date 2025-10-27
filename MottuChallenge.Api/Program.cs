using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MottuChallenge.Api.Extensions;
using MottuChallenge.Application;
using MottuChallenge.Application.Configurations;
using MottuChallenge.Infrastructure;

namespace MottuChallenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configs = builder.Configuration.Get<Settings>();
            
            builder.Services.AddInfrastructure(configs);    
            builder.Services.AddUseCases();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(configs.Swagger);
            builder.Services.AddHealthServices(configs.ConnectionStrings);

            var app = builder.Build();
            
            app.UseAuthentication(); // ✅ Primeiro autentica
            app.UseAuthorization();  // ✅ Depois verifica permissão

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();
            
            app.MapHealthChecks("/api/health-check", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse
            });

            app.Run();
        }
    }
}
