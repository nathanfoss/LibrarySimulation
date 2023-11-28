using Books.Domain.Authors;
using Books.Domain.Books;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class AddBookCommand : IRequest<Result>
    {
        public Book Book { get; set; }

        public string AuthorName { get; set; }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Result>
    {
        private readonly IBookService bookService;

        private readonly IAuthorService authorService;

        private readonly ILogger<AddBookCommandHandler> logger;

        public AddBookCommandHandler(IBookService bookService, IAuthorService authorService, ILogger<AddBookCommandHandler> logger)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.logger = logger;
        }

        public async Task<Result> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = request.Book;

                var existingAuthor = await authorService.Get(request.AuthorName);
                existingAuthor ??= await authorService.Add(new Author
                    {
                        FullName = request.AuthorName
                    });

                book.AuthorId = existingAuthor.Id;
                book.DateAdded = DateTime.UtcNow;
                await bookService.Add(book);

                // TODO: Publish book added event
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred adding book {@Book} for author {Author}", request.Book, request.AuthorName);
                return Result.Failure(ex);
            }
        }
    }
}
