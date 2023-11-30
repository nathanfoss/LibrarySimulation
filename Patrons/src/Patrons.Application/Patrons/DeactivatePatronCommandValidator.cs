using FluentValidation;

namespace Patrons.Application.Patrons
{
    public class DeactivatePatronCommandValidator : AbstractValidator<DeactivatePatronCommand>
    {
        public DeactivatePatronCommandValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
