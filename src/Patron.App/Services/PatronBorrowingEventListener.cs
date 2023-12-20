using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Records.Client.Borrowing;

namespace Patron.App.Services
{
    public class PatronBorrowingEventListener : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly TimeSpan delay = TimeSpan.FromSeconds(5);

        public PatronBorrowingEventListener(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var borrowingRecordClient = scope.ServiceProvider.GetRequiredService<IBorrowingRecordClient>();
            var timer = new PeriodicTimer(delay);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                // TODO: Check for events (store in EF, add to client)
                // TODO: Add Records
                // TODO: Delete events
            }
        }
    }
}
