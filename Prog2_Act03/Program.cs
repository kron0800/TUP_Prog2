
using Microsoft.EntityFrameworkCore;
using Prog2_Act03.Data;
using Prog2_Act03.Models;
using Prog2_Act03.Services;

namespace Prog2_Act03
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
            builder.Services.AddSwaggerGen();

            // Database context configuration
            builder.Services.AddDbContext<FacturacionContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
            );

            // Dependency inyection
            builder.Services.AddScoped<IGenericRepository<Factura>, FacturaRepositoryEF>();
            builder.Services.AddScoped<IFacturaService, FacturaService>();

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
