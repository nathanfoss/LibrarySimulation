using Books.Application.BookBorrows;
using Books.Domain;
using Books.Domain.Authors;
using Books.Domain.Books;
using Books.Domain.Borrows;
using Books.Domain.Events;
using Books.Infrastructure.Authors;
using Books.Infrastructure.BookBorrows;
using Books.Infrastructure.Books;
using Books.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDependentServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorService, AuthorService>()
                .AddTransient<IBookService, BookService>()
                .AddTransient<IBookBorrowService, BookBorrowService>()
                .AddTransient<IBookBorrowEventService, BookBorrowEventService>()
                .AddLogging()
                .AddDbContext<BookDbContext>()
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(GetOverdueBooksQuery).Assembly);
                });

            return services;
        }
    }
}
