using MediatR;
using Records.Application.Borrowing;
using Records.Domain.Borrowing;

namespace Records.Client.Borrowing
{
    public class BorrowingRecordClient : IBorrowingRecordClient
    {
        private readonly IMediator mediator;

        public BorrowingRecordClient(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Add(int bookId, int patronId, BorrowingRecordTypeEnum recordTypeId)
        {
            await mediator.Send(new AddBorrowingRecordCommand
            {
                BookId = bookId,
                PatronId = patronId,
                RecordTypeId = recordTypeId
            });
        }

        public async Task<IEnumerable<BorrowingRecord>> GetByBook(int bookId)
        {
            var result = await mediator.Send(new GetBorrowingRecordsByBookQuery { BookId = bookId });
            return result.Response;
        }

        public async Task<IEnumerable<BorrowingRecord>> GetByPatron(int patronId)
        {
            var result = await mediator.Send(new GetBorrowingRecordsByPatronQuery { PatronId = patronId });
            return result.Response;
        }
    }
}
