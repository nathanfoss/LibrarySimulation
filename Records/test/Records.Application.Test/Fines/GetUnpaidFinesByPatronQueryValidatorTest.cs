using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class GetUnpaidFinesByPatronQueryValidatorTest : RequestValidatorTestBase<GetUnpaidFinesByPatronQueryValidator, GetUnpaidFinesByPatronQuery, Result<IEnumerable<Fine>>>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetUnpaidFinesByPatronQuery
                {
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
