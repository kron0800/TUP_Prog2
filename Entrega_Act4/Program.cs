
using Entrega_Act4.Data;
using Entrega_Act4.Models;
using Entrega_Act4.Services;
using Microsoft.EntityFrameworkCore;

namespace Entrega_Act4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // add db context
            builder.Services.AddDbContext<EnvioContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // dependency inyection
            builder.Services.AddScoped<IEnvioRepository, EnvioRepository>();
            builder.Services.AddScoped<IEnvioService, EnvioService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
