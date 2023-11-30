using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class GetPatronQueryValidatorTest : RequestValidatorTestBase<GetPatronQueryValidator, GetPatronQuery, Result<Patron>>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowExceptionForInvalidInput(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetPatronQuery
                {
                    Id = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
