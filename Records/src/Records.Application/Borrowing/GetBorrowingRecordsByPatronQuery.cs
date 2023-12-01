using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Borrowing;

namespace Records.Application.Borrowing
{
    public class GetBorrowingRecordsByPatronQuery : IRequest<Result<IEnumerable<BorrowingRecord>>>
    {
        public int PatronId { get; set; }
    }

    public class GetBorrowingRecordsByPatronQueryHandler : IRequestHandler<GetBorrowingRecordsByPatronQuery, Result<IEnumerable<BorrowingRecord>>>
    {
        private readonly IBorrowingRecordService borrowingRecordService;

        private readonly ILogger<GetBorrowingRecordsByPatronQueryHandler> logger;

        public GetBorrowingRecordsByPatronQueryHandler(IBorrowingRecordService borrowingRecordService, ILogger<GetBorrowingRecordsByPatronQueryHandler> logger)
        {
            this.borrowingRecordService = borrowingRecordService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BorrowingRecord>>> Handle(GetBorrowingRecordsByPatronQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var records = await borrowingRecordService.GetByPatron(request.PatronId);
                return Result<IEnumerable<BorrowingRecord>>.Success(records);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error getting borrowing records for patron {Patron}", request.PatronId);
                return Result<IEnumerable<BorrowingRecord>>.Failure(ex);
            }
        }
    }
}
