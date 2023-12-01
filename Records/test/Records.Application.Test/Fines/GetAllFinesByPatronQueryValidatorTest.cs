using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class GetAllFinesByPatronQueryValidatorTest : RequestValidatorTestBase<GetAllFinesByPatronQueryValidator, GetAllFinesByPatronQuery, Result<IEnumerable<Fine>>>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetAllFinesByPatronQuery
                {
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
