using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Fines;

namespace Records.Application.Test.Fines
{
    public class PayFineCommandValidatorTest : RequestValidatorTestBase<PayFineCommandValidator, PayFineCommand, Result>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int fineId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new PayFineCommand
                {
                    FineId = fineId
                }, () => null, CancellationToken.None));
        }
    }
}
