using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
