using Books.Domain.Books;
using Books.Domain.BookStatuses;
using Books.Domain.Events;
using LibrarySimulation.Shared.Kernel;
using LibrarySimulation.Shared.Kernel.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Books.Application.Books
{
    public class ReserveBookCommand : IRequest<Result>
    {
        public int BookId { get; set; }

        public int PatronId { get; set; }
    }

    public class ReserveBookCommandHandler : IRequestHandler<ReserveBookCommand, Result>
    {
        private readonly IBookService bookService;

        private readonly IBookBorrowEventService bookBorrowEventService;

        private readonly ILogger<ReserveBookCommandHandler> logger;

        public ReserveBookCommandHandler(IBookService bookService,
            IBookBorrowEventService bookBorrowEventService,
            ILogger<ReserveBookCommandHandler> logger)
        {
            this.bookService = bookService;
            this.bookBorrowEventService = bookBorrowEventService;
            this.logger = logger;
        }

        public async Task<Result> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await bookService.Get(request.BookId);
                if (book == null || book.StatusId != BookStatusEnum.Available)
                {
                    throw new Exception("Invalid book provided");
                }

                book.StatusId = BookStatusEnum.Reserved;
                await bookService.Update(book);

                await bookBorrowEventService.Add(new BookBorrowEvent
                {
                    BookId = request.BookId,
                    PatronId = request.PatronId,
                    EventType = BorrowingRecordTypeEnum.Reserved
                });

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error reserving book {Book} for patron {Patron}", request.BookId, request.PatronId);
                return Result.Failure(ex);
            }
        }
    }
}
