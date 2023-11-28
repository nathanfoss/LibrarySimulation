using Books.Domain.Books;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class GetAllBooksQuery : IRequest<Result<IEnumerable<Book>>>
    {
    }

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<Book>>>
    {
        private readonly IBookService bookService;

        private readonly ILogger<GetAllBooksQueryHandler> logger;

        public GetAllBooksQueryHandler(IBookService bookService, ILogger<GetAllBooksQueryHandler> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await bookService.GetAll();
                return Result<IEnumerable<Book>>.Success(books);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching all books");
                return Result<IEnumerable<Book>>.Failure(ex);
            }
        }
    }
}
