using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReferences).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }

    }
}
