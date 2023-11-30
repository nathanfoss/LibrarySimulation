using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Patrons
{
    public class DeactivatePatronCommand : IRequest<Result>
    {
        public int PatronId { get; set; }
    }

    public class DeactivatePatronCommandHandler : IRequestHandler<DeactivatePatronCommand, Result>
    {
        private readonly IPatronService patronService;

        private readonly ILogger<DeactivatePatronCommandHandler> logger;

        public DeactivatePatronCommandHandler(IPatronService patronService, ILogger<DeactivatePatronCommandHandler> logger)
        {
            this.patronService = patronService;
            this.logger = logger;
        }

        public async Task<Result> Handle(DeactivatePatronCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await patronService.Get(request.PatronId);

                if (existing is null || !existing.IsActive)
                {
                    return Result.Success();
                }

                existing.DeactivatedDate = DateTime.UtcNow;
                existing.IsActive = false;
                await patronService.Update(existing);

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred");
                return Result.Failure(ex);
            }
        }
    }
}
