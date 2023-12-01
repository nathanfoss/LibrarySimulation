using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Records.Domain.Borrowing;

namespace Records.Application.Borrowing
{
    public class AddBorrowingRecordCommand : IRequest<Result>
    {
        public int BookId { get; set; }

        public int PatronId { get; set; }

        public BorrowingRecordTypeEnum RecordTypeId { get; set; }
    }

    public class AddBorrowingRecordCommandHandler : IRequestHandler<AddBorrowingRecordCommand, Result>
    {
        private readonly IBorrowingRecordService borrowingRecordService;

        private readonly ILogger<AddBorrowingRecordCommandHandler> logger;

        public AddBorrowingRecordCommandHandler(IBorrowingRecordService borrowingRecordService, ILogger<AddBorrowingRecordCommandHandler> logger)
        {
            this.borrowingRecordService = borrowingRecordService;
            this.logger = logger;
        }

        public async Task<Result> Handle(AddBorrowingRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await borrowingRecordService.Add(new BorrowingRecord
                {
                    BookId = request.BookId,
                    PatronId = request.PatronId,
                    RecordTypeId = request.RecordTypeId,
                    CreatedDate = DateTime.UtcNow
                });
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error adding {RecordType} record for book {Book} and patron {Patron}",
                    request.RecordTypeId, request.BookId, request.PatronId);
                return Result.Failure(ex);
            }
        }
    }
}
