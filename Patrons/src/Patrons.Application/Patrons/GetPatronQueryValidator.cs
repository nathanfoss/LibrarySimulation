using FluentValidation;

namespace Patrons.Application.Patrons
{
    public class GetPatronQueryValidator : AbstractValidator<GetPatronQuery>
    {
        public GetPatronQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
