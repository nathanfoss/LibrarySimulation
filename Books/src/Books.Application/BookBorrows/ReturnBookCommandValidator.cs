using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.BookBorrowId).GreaterThan(0);
        }
    }
}
