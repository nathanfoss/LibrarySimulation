using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patron.App.Services
{
    public class PatronAppService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        private TimeSpan period = TimeSpan.FromSeconds(5);

        public PatronAppService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: Add a patron
            // TODO: View catalog
            // TODO: View books by author
            // TODO: Search for a book
            // TODO: Reserve an available book
            // TODO: Check out reserved book
            // TODO: Check out available book
            // TODO: View my borrowing history
            // TODO: View my fines
            // TODO: Pay a specific fine
            // TODO: Pay all fines
            // TODO: Return a book
            // TODO: Return the other book
            // TODO: Close my account
        }
    }
}
