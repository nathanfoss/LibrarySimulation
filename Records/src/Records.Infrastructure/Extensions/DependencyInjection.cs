using Microsoft.Extensions.DependencyInjection;
using Records.Application.Borrowing;
using Records.Domain;
using Records.Domain.Borrowing;
using Records.Domain.Fines;
using Records.Infrastructure.Borrowing;
using Records.Infrastructure.Fines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddLogging()
                .AddDbContext<RecordsDbContext>()
                .AddTransient<IBorrowingRecordService, BorrowingRecordService>()
                .AddTransient<IFineService, FineService>()
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(AddBorrowingRecordCommand).Assembly);
                });

            return services;
        }
    }
}
