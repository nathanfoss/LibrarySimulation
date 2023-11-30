using Microsoft.Extensions.DependencyInjection;
using Patrons.Application.Patrons;
using Patrons.Domain;
using Patrons.Domain.Patrons;
using Patrons.Infrastructure.Patrons;

namespace Patrons.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services
                .AddLogging()
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(GetPatronQuery).Assembly);
                })
                .AddTransient<IPatronService, PatronService>()
                .AddDbContext<PatronDbContext>();

            return services;
        }
    }
}
