using Books.Domain.Books;
using Books.Domain.BookStatuses;
using Books.Domain.Borrows;
using Books.Domain.Events;
using LibrarySimulation.Shared.Kernel;
using LibrarySimulation.Shared.Kernel.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class ReturnBookCommand : IRequest<Result>
    {
        public int BookBorrowId { get; set; }
    }

    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly IBookService bookService;

        private readonly IBookBorrowEventService bookBorrowEventService;

        private readonly ILogger<ReturnBookCommandHandler> logger;

        public ReturnBookCommandHandler(IBookBorrowService bookBorrowService,
            IBookService bookService,
            IBookBorrowEventService bookBorrowEventService,
            ILogger<ReturnBookCommandHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.bookService = bookService;
            this.bookBorrowEventService = bookBorrowEventService;
            this.logger = logger;
        }

        public async Task<Result> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookBorrow = await bookBorrowService.Get(request.BookBorrowId);

                if (bookBorrow == null || bookBorrow.IsClosed)
                {
                    return Result.Success();
                }

                bookBorrow.IsClosed = true;
                bookBorrow.EndDate = DateTime.UtcNow;

                await bookBorrowService.Update(bookBorrow);

                var book = await bookService.Get(bookBorrow.BookId);
                book.StatusId = BookStatusEnum.Available;
                await bookService.Update(book);

                await bookBorrowEventService.Add(new BookBorrowEvent
                {
                    BookId = bookBorrow.BookId,
                    PatronId = bookBorrow.PatronId,
                    EventType = BorrowingRecordTypeEnum.CheckedIn
                });
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error closing book borrow {BookBorrow}", request.BookBorrowId);
                return Result.Failure(ex);
            }
        }
    }
}
