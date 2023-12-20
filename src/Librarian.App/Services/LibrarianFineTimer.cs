using Books.Client.BookBorrows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Records.Client.Fines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.App.Services
{
    public class LibrarianFineTimer : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly TimeSpan period = TimeSpan.FromSeconds(60);

        public LibrarianFineTimer(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Checking for overdue books");
            using var scope = serviceScopeFactory.CreateScope();
            var bookBorrowClient = scope.ServiceProvider.GetRequiredService<IBookBorrowClient>();
            var fineClient = scope.ServiceProvider.GetRequiredService<IFineClient>();

            using var timer = new PeriodicTimer(period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var overdueBooks = await bookBorrowClient.GetOverdueBooks();

                if (!overdueBooks.Any())
                {
                    return;
                }

                Console.WriteLine($"Adding fines for {overdueBooks.Count()} overdue books");
                foreach (var overdueBook in overdueBooks)
                {
                    await fineClient.Add(overdueBook.BookId, overdueBook.PatronId);
                }
            }
        }
    }
}
