using DomainLayer.Contracts;
using E_Commerce.Web.CustomeExceptionMiddleWare;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomeExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomeExceptionHandlerMiddleWare>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
