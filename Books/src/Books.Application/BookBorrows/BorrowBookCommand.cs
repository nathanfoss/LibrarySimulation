using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class BorrowBookCommand : IRequest<Result>
    {
        public int BookId { get; set; }

        public int PatronId { get; set; }
    }

    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly IConfiguration configuration;

        private readonly ILogger<BorrowBookCommandHandler> logger;

        public BorrowBookCommandHandler(IBookBorrowService bookBorrowService, IConfiguration configuration, ILogger<BorrowBookCommandHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<Result> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookBorrow = new BookBorrow
                {
                    BookId = request.BookId,
                    PatronId = request.PatronId,
                    CreatedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddDays(configuration.GetValue<int>("DefaultBorrowingPeriodDays")),
                    IsClosed = false
                };
                await bookBorrowService.Add(bookBorrow);

                // TODO: Update book status
                // TODO: Add book record
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error borrowing book {Book} for patron {Patron}", request.BookId, request.PatronId);
                return Result.Failure(ex);
            }
        }
    }
}
