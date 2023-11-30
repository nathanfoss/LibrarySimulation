using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Patrons.Application.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class DeactivatePatronCommandValidatorTest : RequestValidatorTestBase<DeactivatePatronCommandValidator, DeactivatePatronCommand, Result>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowExceptionForInvalidInput(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new DeactivatePatronCommand
                {
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
