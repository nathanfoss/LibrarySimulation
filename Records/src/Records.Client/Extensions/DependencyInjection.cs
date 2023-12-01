using Microsoft.Extensions.DependencyInjection;
using Records.Client.Borrowing;
using Records.Client.Fines;
using Records.Infrastructure.Extensions;

namespace Records.Client.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBorrowingRecordModule(this IServiceCollection services)
        {
            services.RegisterServices()
                .AddTransient<IBorrowingRecordClient, BorrowingRecordClient>()
                .AddTransient<IFineClient, FineClient>();

            return services;
        }
    }
}
