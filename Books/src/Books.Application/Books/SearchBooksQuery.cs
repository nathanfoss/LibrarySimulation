using Books.Domain.Books;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class SearchBooksQuery : IRequest<Result<IEnumerable<Book>>>
    {
        public string SearchText { get; set; }
    }

    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, Result<IEnumerable<Book>>>
    {
        private readonly IBookService bookService;

        private readonly ILogger<SearchBooksQueryHandler> logger;

        public SearchBooksQueryHandler(IBookService bookService, ILogger<SearchBooksQueryHandler> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Book>>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await bookService.Search(request.SearchText.Trim());
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
