using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class AddPatronCommandValidatorTest : RequestValidatorTestBase<AddPatronCommandValidator, AddPatronCommand, Result<Patron>>
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowExceptionForInvalidInput(string name)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new AddPatronCommand
                {
                    Name = name
                }, () => null, CancellationToken.None));
        }
    }
}
