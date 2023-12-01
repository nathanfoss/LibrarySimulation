using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Fines;

namespace Records.Application.Test.Fines
{
    public class PayFinesCommandValidatorTest : RequestValidatorTestBase<PayFinesCommandValidator, PayFinesCommand, Result>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new PayFinesCommand
                {
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
