using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class RenewBookCommandValidator : AbstractValidator<RenewBookCommand>
    {
        public RenewBookCommandValidator()
        {
            RuleFor(x => x.BookBorrowId).GreaterThan(0);
        }
    }
}
