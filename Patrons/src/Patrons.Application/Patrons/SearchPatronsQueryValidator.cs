using FluentValidation;

namespace Patrons.Application.Patrons
{
    public class SearchPatronsQueryValidator : AbstractValidator<SearchPatronsQuery>
    {
        public SearchPatronsQueryValidator()
        {
            RuleFor(x => x.SearchText).NotEmpty();
        }
    }
}
