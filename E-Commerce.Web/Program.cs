
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;

namespace E_Commerce.Web
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
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            Options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection"))
            );
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjextOfDataSeeding= Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjextOfDataSeeding.DataSeed();

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
