using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Fines;

namespace Records.Application.Fines
{
    public class GetAllFinesByPatronQuery : IRequest<Result<IEnumerable<Fine>>>
    {
        public int PatronId { get; set; }
    }

    public class GetAllFinesByPatronQueryHandler : IRequestHandler<GetAllFinesByPatronQuery, Result<IEnumerable<Fine>>>
    {
        private readonly IFineService fineService;

        private readonly ILogger<GetAllFinesByPatronQueryHandler> logger;

        public GetAllFinesByPatronQueryHandler(IFineService fineService, ILogger<GetAllFinesByPatronQueryHandler> logger)
        {
            this.fineService = fineService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Fine>>> Handle(GetAllFinesByPatronQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var fines = await fineService.GetAllByPatron(request.PatronId);
                return Result<IEnumerable<Fine>>.Success(fines);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error getting fines for patron {Patron}", request.PatronId);
                return Result<IEnumerable<Fine>>.Failure(ex);
            }
        }
    }
}
