using System.Reflection;
using Microsoft.OpenApi.Models;
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
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
