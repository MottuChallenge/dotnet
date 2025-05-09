
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MottuGrid_Dotnet.Infrastructure.Context;

namespace MottuGrid_Dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = builder.Configuration["Swagger:Description"] + DateTime.Now.Year,
                    Contact = new OpenApiContact()
                    {
                        Email = builder.Configuration["Swagger:Email"],
                        Name = builder.Configuration["Swagger:Name"]
                    }
                });
            });

            builder.Services.AddDbContext<MottuGridContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
            });


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
