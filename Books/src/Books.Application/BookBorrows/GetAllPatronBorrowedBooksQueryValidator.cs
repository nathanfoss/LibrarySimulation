using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class GetAllPatronBorrowedBooksQueryValidator : AbstractValidator<GetAllPatronBorrowedBooksQuery>
    {
        public GetAllPatronBorrowedBooksQueryValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
