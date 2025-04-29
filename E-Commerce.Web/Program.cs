
using DomainLayer.Contracts;
using E_Commerce.Web.CustomeExceptionMiddleWare;
using E_Commerce.Web.Extensions;
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

            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastractureServices(builder.Configuration);

            builder.Services.AddApplicationService();

            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);

            var app = builder.Build();

            await app.SeedDatabaseAsync();


            app.UseCustomeExceptionMiddleWare();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
