using Books.Client.BookBorrows;
using Books.Client.Books;
using Books.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Client.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBooksModule(this IServiceCollection services)
        {
            services.RegisterDependentServices()
                .RegisterClients();

            return services;
        }

        private static IServiceCollection RegisterClients(this IServiceCollection services)
        {
            services.AddTransient<IBookClient, BookClient>()
                .AddTransient<IBookBorrowClient, BookBorrowClient>();


            return services;
        }
    }
}
