
using DomainLayer.Contracts;
using E_Commerce.Web.CustomeExceptionMiddleWare;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Service.AssemblyReferences).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory=ApiResponseFactory.GenerateApiResponseValidationError;

            });

            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjextOfDataSeeding= Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjextOfDataSeeding.DataSeedAsync();


            app.UseMiddleware<CustomeExceptionHandlerMiddleWare>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
