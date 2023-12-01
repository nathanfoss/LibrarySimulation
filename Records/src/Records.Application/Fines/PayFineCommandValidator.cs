using FluentValidation;

namespace Records.Application.Fines
{
    public class PayFineCommandValidator : AbstractValidator<PayFineCommand>
    {
        public PayFineCommandValidator()
        {
            RuleFor(x => x.FineId).GreaterThan(0);
        }
    }
}
