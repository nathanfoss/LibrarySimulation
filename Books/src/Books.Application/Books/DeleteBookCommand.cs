using Books.Domain.Books;
using Books.Domain.BookStatuses;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public int BookId { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookService bookService;

        private readonly ILogger<DeleteBookCommandHandler> logger;

        public DeleteBookCommandHandler(IBookService bookService, ILogger<DeleteBookCommandHandler> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await bookService.Get(request.BookId) ?? 
                    throw new ArgumentOutOfRangeException(nameof(request.BookId));

                if (book.StatusId != BookStatusEnum.Available)
                {
                    throw new Exception($"Cannot delete this book because the status is {book.StatusId}");
                }

                book.IsDeleted = true;
                book.DeletedDate = DateTime.UtcNow;
                await bookService.Update(book);
                // TODO: Publish book deleted event

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred deleting book {Book}", request.BookId);
                return Result.Failure(ex);
            }
        }
    }
}
