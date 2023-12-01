using FluentValidation;

namespace Records.Application.Fines
{
    public class PayFinesCommandValidator : AbstractValidator<PayFinesCommand>
    {
        public PayFinesCommandValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
