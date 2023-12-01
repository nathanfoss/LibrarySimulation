using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Fines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records.Application.Fines
{
    public class PayFinesCommand : IRequest<Result>
    {
        public int PatronId { get; set; }
    }

    public class PayFinesCommandHandler : IRequestHandler<PayFinesCommand, Result>
    {
        private readonly IFineService fineService;

        private readonly ILogger<PayFinesCommandHandler> logger;

        public PayFinesCommandHandler(IFineService fineService, ILogger<PayFinesCommandHandler> logger)
        {
            this.fineService = fineService;
            this.logger = logger;
        }

        public async Task<Result> Handle(PayFinesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                decimal total = 0;
                var fines = (await fineService.GetUnpaidByPatron(request.PatronId)).ToList();

                if (!fines.Any())
                {
                    return Result.Success();
                }

                foreach (var fine in fines)
                {
                    fine.IsPaid = true;
                    fine.PaymentReceivedDate = DateTime.UtcNow;
                    total += fine.Amount;
                }

                await fineService.Update(fines);

                logger.LogInformation("Fines paid for patron {Patron} totalling amount {Amount}", request.PatronId, total);
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error paying fines for patron {Patron}", request.PatronId);
                return Result.Failure(ex);
            }
        }
    }
}
