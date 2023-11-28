using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class GetOverdueBooksQuery : IRequest<Result<IEnumerable<BookBorrow>>>
    {
    }

    public class GetOverdueBooksQueryHandler : IRequestHandler<GetOverdueBooksQuery, Result<IEnumerable<BookBorrow>>>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly ILogger<GetOverdueBooksQueryHandler> logger;

        public GetOverdueBooksQueryHandler(IBookBorrowService bookBorrowService, ILogger<GetOverdueBooksQueryHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BookBorrow>>> Handle(GetOverdueBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var borrows = await bookBorrowService.GetOverdue();
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
