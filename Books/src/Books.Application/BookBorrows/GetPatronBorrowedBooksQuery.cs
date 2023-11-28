using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class GetPatronBorrowedBooksQuery : IRequest<Result<IEnumerable<BookBorrow>>>
    {
        public int PatronId { get; set; }
    }

    public class GetPatronBorrowedBooksQueryHandler : IRequestHandler<GetPatronBorrowedBooksQuery, Result<IEnumerable<BookBorrow>>>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly ILogger<GetPatronBorrowedBooksQueryHandler> logger;

        public GetPatronBorrowedBooksQueryHandler(IBookBorrowService bookBorrowService, ILogger<GetPatronBorrowedBooksQueryHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BookBorrow>>> Handle(GetPatronBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var borrows = await bookBorrowService.GetByPatron(request.PatronId);
                return Result<IEnumerable<BookBorrow>>.Success(borrows);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred");
                return Result<IEnumerable<BookBorrow>>.Failure(ex);
            }
        }
    }
}
