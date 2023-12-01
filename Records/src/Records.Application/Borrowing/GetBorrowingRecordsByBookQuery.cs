using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Borrowing;

namespace Records.Application.Borrowing
{
    public class GetBorrowingRecordsByBookQuery : IRequest<Result<IEnumerable<BorrowingRecord>>>
    {
        public int BookId { get; set; }
    }

    public class GetBorrowingRecordsByBookQueryHandler : IRequestHandler<GetBorrowingRecordsByBookQuery, Result<IEnumerable<BorrowingRecord>>>
    {
        private readonly IBorrowingRecordService borrowingRecordService;

        private readonly ILogger<GetBorrowingRecordsByBookQueryHandler> logger;

        public GetBorrowingRecordsByBookQueryHandler(IBorrowingRecordService borrowingRecordService, ILogger<GetBorrowingRecordsByBookQueryHandler> logger)
        {
            this.borrowingRecordService = borrowingRecordService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BorrowingRecord>>> Handle(GetBorrowingRecordsByBookQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var records = await borrowingRecordService.GetByBook(request.BookId);
                return Result<IEnumerable<BorrowingRecord>>.Success(records);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error getting borrowing records for book {Book}", request.BookId);
                return Result<IEnumerable<BorrowingRecord>>.Failure(ex);
            }
        }
    }
}
