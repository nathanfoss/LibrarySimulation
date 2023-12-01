using FluentValidation;

namespace Records.Application.Fines
{
    public class GetAllFinesByPatronQueryValidator : AbstractValidator<GetAllFinesByPatronQuery>
    {
        public GetAllFinesByPatronQueryValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
