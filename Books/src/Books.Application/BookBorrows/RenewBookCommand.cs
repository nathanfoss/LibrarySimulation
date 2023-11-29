using Books.Domain.Borrows;
using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Books.Application.BookBorrows
{
    public class RenewBookCommand : IRequest<Result>
    {
        public int BookBorrowId { get; set; }
    }

    public class RenewBookCommandHandler : IRequestHandler<RenewBookCommand, Result>
    {
        private readonly IBookBorrowService bookBorrowService;

        private readonly IConfiguration configuration;

        private readonly ILogger<RenewBookCommandHandler> logger;

        public RenewBookCommandHandler(IBookBorrowService bookBorrowService, IConfiguration configuration, ILogger<RenewBookCommandHandler> logger)
        {
            this.bookBorrowService = bookBorrowService;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<Result> Handle(RenewBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookBorrow = await bookBorrowService.Get(request.BookBorrowId) ??
                    throw new ArgumentOutOfRangeException(nameof(request.BookBorrowId));

                var maxRenewalCount = configuration.GetValue<int>("MaxRenewalCount");
                if (bookBorrow.RenewalCount >= maxRenewalCount)
                {
                    throw new ArgumentException("Unable to renew book. Max Renewal count reached");
                }

                var renewalPeriodDays = configuration.GetValue<int>("RenewalPeriodDays");
                var expirationDate = bookBorrow.ExpirationDate;
                var renewalCount = bookBorrow.RenewalCount;

                bookBorrow.ExpirationDate = expirationDate.AddDays(renewalPeriodDays);
                bookBorrow.RenewalCount = renewalCount + 1;
                await bookBorrowService.Update(bookBorrow);
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error renewing book borrow {BookBorrow}", request.BookBorrowId);
                return Result.Failure(ex);
            }
        }
    }
}
