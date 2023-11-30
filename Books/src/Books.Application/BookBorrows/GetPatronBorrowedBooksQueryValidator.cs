using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class GetPatronBorrowedBooksQueryValidator : AbstractValidator<GetPatronBorrowedBooksQuery>
    {
        public GetPatronBorrowedBooksQueryValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
