using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class GetAllPatronBorrowedBooksQuery : IRequest<Result<IEnumerable<BookBorrow>>>
    {
        public int PatronId { get; set; }
    }

    public class GetAllPatronBorrowedBooksQueryHandler : IRequestHandler<GetAllPatronBorrowedBooksQuery, Result<IEnumerable<BookBorrow>>>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly ILogger<GetAllPatronBorrowedBooksQueryHandler> logger;

        public GetAllPatronBorrowedBooksQueryHandler(IBookBorrowService bookBorrowService, ILogger<GetAllPatronBorrowedBooksQueryHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BookBorrow>>> Handle(GetAllPatronBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var borrows = await bookBorrowService.GetAllByPatron(request.PatronId);
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
