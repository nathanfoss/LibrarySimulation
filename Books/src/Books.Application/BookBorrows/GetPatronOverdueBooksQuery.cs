using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class GetPatronOverdueBooksQuery : IRequest<Result<IEnumerable<BookBorrow>>>
    {
        public int PatronId { get; set; }
    }

    public class GetPatronOverdueBooksQueryHandler : IRequestHandler<GetPatronOverdueBooksQuery, Result<IEnumerable<BookBorrow>>>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly ILogger<GetPatronOverdueBooksQueryHandler> logger;

        public GetPatronOverdueBooksQueryHandler(IBookBorrowService bookBorrowService, ILogger<GetPatronOverdueBooksQueryHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<BookBorrow>>> Handle(GetPatronOverdueBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var borrows = await bookBorrowService.GetOverdue(request.PatronId);
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
