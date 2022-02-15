using Microsoft.EntityFrameworkCore;
using SSO.Infra.Context;
using SSO.Infra.DataSeed;

namespace SSO.Api.Extensions
{
    public static class DbContextService
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => 
                options.UseInMemoryDatabase("InMemoryDatabase")
            );

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context?.Seed();
        }
    }
}