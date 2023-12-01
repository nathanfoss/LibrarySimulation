using FluentValidation;

namespace Records.Application.Fines
{
    public class AddFineCommandValidator : AbstractValidator<AddFineCommand>
    {
        public AddFineCommandValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
