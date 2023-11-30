using Microsoft.Extensions.DependencyInjection;
using Patrons.Client.Patrons;
using Patrons.Infrastructure.DependencyInjection;

namespace Patrons.Client.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPatronModule(this IServiceCollection services)
        {
            services.RegisterDependencies().AddTransient<IPatronClient, PatronClient>();

            return services;
        }
    }
}
