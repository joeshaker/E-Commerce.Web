using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using StackExchange.Redis;

namespace Presistence
{
    public static class InfrastractureServicesApplication
    {
        public static IServiceCollection AddInfrastractureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(Options =>
               Options.UseSqlServer(Configuration.GetConnectionString("DeafultConnection"))
            );
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
               return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));

            });
            Services.AddDbContext<StoreIdentityDbContext>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"))
);
            return Services;
        }

    }
}
