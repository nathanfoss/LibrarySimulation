using FluentValidation;

namespace Records.Application.Fines
{
    public class GetUnpaidFinesByPatronQueryValidator : AbstractValidator<GetUnpaidFinesByPatronQuery>
    {
        public GetUnpaidFinesByPatronQueryValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
