using Books.Domain.Books;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class GetBookQuery : IRequest<Result<Book>>
    {
        public int BookId { get; set; }
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<Book>>
    {
        private readonly IBookService bookService;

        private readonly ILogger<GetBookQueryHandler> logger;

        public GetBookQueryHandler(IBookService bookService, ILogger<GetBookQueryHandler> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public async Task<Result<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await bookService.Get(request.BookId) ??
                    throw new ArgumentOutOfRangeException(nameof(request.BookId));

                return Result<Book>.Success(book);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching book {Book}", request.BookId);
                return Result<Book>.Failure(ex);
            }
        }
    }
}
