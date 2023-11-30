using FluentValidation;

namespace Patrons.Application.Patrons
{
    public class AddPatronCommandValidator : AbstractValidator<AddPatronCommand>
    {
        public AddPatronCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
