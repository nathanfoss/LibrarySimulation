using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Patrons
{
    public class SearchPatronsQuery : IRequest<Result<IEnumerable<Patron>>>
    {
        public string SearchText { get; set; }
    }

    public class SearchPatronsQueryHandler : IRequestHandler<SearchPatronsQuery, Result<IEnumerable<Patron>>>
    {
        private readonly IPatronService patronService;

        private readonly ILogger<SearchPatronsQueryHandler> logger;

        public SearchPatronsQueryHandler(IPatronService patronService, ILogger<SearchPatronsQueryHandler> logger)
        {
            this.patronService = patronService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Patron>>> Handle(SearchPatronsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patron = await patronService.Search(request.SearchText);
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
