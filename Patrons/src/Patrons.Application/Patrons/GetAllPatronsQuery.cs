using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Patrons
{
    public class GetAllPatronsQuery : IRequest<Result<IEnumerable<Patron>>>
    {
    }

    public class GetAllPatronsQueryHandler : IRequestHandler<GetAllPatronsQuery, Result<IEnumerable<Patron>>>
    {
        private readonly IPatronService patronService;

        private readonly ILogger<GetAllPatronsQueryHandler> logger;

        public GetAllPatronsQueryHandler(IPatronService patronService, ILogger<GetAllPatronsQueryHandler> logger)
        {
            this.patronService = patronService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Patron>>> Handle(GetAllPatronsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patron = await patronService.GetAll();
                return Result<IEnumerable<Patron>>.Success(patron);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred");
                return Result<IEnumerable<Patron>>.Failure(ex);
            }
        }
    }
}
