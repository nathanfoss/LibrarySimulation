using FluentValidation;

namespace Books.Application.Books
{
    public class ReserveBookCommandValidator : AbstractValidator<ReserveBookCommand>
    {
        public ReserveBookCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
