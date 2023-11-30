using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class SearchPatronsQueryValidatorTest : RequestValidatorTestBase<SearchPatronsQueryValidator, SearchPatronsQuery, Result<IEnumerable<Patron>>>
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowExceptionForInvalidInput(string searchTest)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new SearchPatronsQuery
                {
                    SearchText = searchTest
                }, () => null, CancellationToken.None));
        }
    }
}
