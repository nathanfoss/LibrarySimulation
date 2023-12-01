using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Fines;

namespace Records.Application.Fines
{
    public class GetUnpaidFinesByPatronQuery : IRequest<Result<IEnumerable<Fine>>>
    {
        public int PatronId { get; set; }
    }

    public class GetUnpaidFinesByPatronQueryHandler : IRequestHandler<GetUnpaidFinesByPatronQuery, Result<IEnumerable<Fine>>>
    {
        private readonly IFineService fineService;

        private readonly ILogger<GetUnpaidFinesByPatronQueryHandler> logger;

        public GetUnpaidFinesByPatronQueryHandler(IFineService fineService, ILogger<GetUnpaidFinesByPatronQueryHandler> logger)
        {
            this.fineService = fineService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Fine>>> Handle(GetUnpaidFinesByPatronQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var fines = await fineService.GetUnpaidByPatron(request.PatronId);
                return Result<IEnumerable<Fine>>.Success(fines);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error getting unpaid fines for patron {Patron}", request.PatronId);
                return Result<IEnumerable<Fine>>.Failure(ex);
            }
        }
    }
}
