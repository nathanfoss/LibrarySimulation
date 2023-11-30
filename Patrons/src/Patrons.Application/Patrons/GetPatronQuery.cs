using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Patrons
{
    public class GetPatronQuery : IRequest<Result<Patron>>
    {
        public int Id { get; set; }
    }

    public class GetPatronQueryHandler : IRequestHandler<GetPatronQuery, Result<Patron>>
    {
        private readonly IPatronService patronService;

        private readonly ILogger<GetPatronQueryHandler> logger;

        public GetPatronQueryHandler(IPatronService patronService, ILogger<GetPatronQueryHandler> logger)
        {
            this.patronService = patronService;
            this.logger = logger;
        }

        public async Task<Result<Patron>> Handle(GetPatronQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patron = await patronService.Get(request.Id);
                return Result<Patron>.Success(patron);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred");
                return Result<Patron>.Failure(ex);
            }
        }
    }
}
