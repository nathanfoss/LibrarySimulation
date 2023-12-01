using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Records.Domain.Fines;

namespace Records.Application.Fines
{
    public class AddFineCommand : IRequest<Result>
    {
        public int BookId { get; set; }

        public int PatronId { get; set; }
    }

    public class AddFineCommandHandler : IRequestHandler<AddFineCommand, Result>
    {
        private readonly IFineService fineService;

        private readonly IConfiguration configuration;

        private readonly ILogger<AddFineCommandHandler> logger;

        public AddFineCommandHandler(IFineService fineService, IConfiguration configuration, ILogger<AddFineCommandHandler> logger)
        {
            this.fineService = fineService;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<Result> Handle(AddFineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var amount = configuration.GetValue<decimal>("DefaultFineAmount");
                await fineService.Add(new Fine
                {
                    Amount = amount,
                    BookId = request.BookId,
                    PatronId = request.PatronId,
                    CreatedDate = DateTime.UtcNow,
                    IsPaid = false
                });
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error adding fine for book {Book} and patron {Patron}", request.BookId, request.PatronId);
                return Result.Failure(ex);
            }
        }
    }
}
