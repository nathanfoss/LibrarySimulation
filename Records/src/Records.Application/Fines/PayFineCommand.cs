using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Fines;

namespace Records.Application.Fines
{
    public class PayFineCommand : IRequest<Result>
    {
        public int FineId { get; set; }
    }

    public class PayFineCommandHandler : IRequestHandler<PayFineCommand, Result>
    {
        private readonly IFineService fineService;

        private readonly ILogger<PayFineCommandHandler> logger;

        public PayFineCommandHandler(IFineService fineService, ILogger<PayFineCommandHandler> logger)
        {
            this.fineService = fineService;
            this.logger = logger;
        }

        public async Task<Result> Handle(PayFineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fine = await fineService.Get(request.FineId);
                if (fine == null || fine.IsPaid)
                {
                    return Result.Success();
                }

                fine.IsPaid = true;
                fine.PaymentReceivedDate = DateTime.UtcNow;

                await fineService.Update(fine);

                logger.LogInformation("Paid fine for patron {Patron} for amount {Amount}", fine.PatronId, fine.Amount);
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error paying fine {Fine}", request.FineId);
                return Result.Failure(ex);
            }
        }
    }
}
