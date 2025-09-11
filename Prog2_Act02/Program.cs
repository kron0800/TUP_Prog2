
using Microsoft.Data.SqlClient;
using Prog2_Act01.Data;
using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;
using Prog2_Act01.Services;

namespace Prog2_Act02
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
            
            // Dependency Injection
            builder.Services.AddScoped<IFacturaService, FacturaService>();
            builder.Services.AddScoped<IDetalleFacturaService, DetalleFacturaService>();
            builder.Services.AddScoped<IArticuloService, ArticuloService>();

            // Instance DataHelper Connection
            DataHelper.GetInstance(Properties.Resources.connectionString);

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
