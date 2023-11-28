using Books.Domain.Books;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class GetByAuthorQuery : IRequest<Result<IEnumerable<Book>>>
    {
        public int AuthorId { get; set; }
    }

    public class GetByAuthorQueryHandler : IRequestHandler<GetByAuthorQuery, Result<IEnumerable<Book>>>
    {
        private readonly IBookService bookService;
        
        private readonly ILogger<GetByAuthorQueryHandler> logger;

        public GetByAuthorQueryHandler(IBookService bookService, ILogger<GetByAuthorQueryHandler> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public async Task<Result<IEnumerable<Book>>> Handle(GetByAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await bookService.GetByAuthor(request.AuthorId);
                return Result<IEnumerable<Book>>.Success(books);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error getting books for author {Author}", request.AuthorId);
                return Result<IEnumerable<Book>>.Failure(ex);
            }
        }
    }
}
