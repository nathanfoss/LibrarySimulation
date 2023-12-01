using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Fines;

namespace Records.Application.Test.Fines
{
    public class AddFineCommandValidatorTest : RequestValidatorTestBase<AddFineCommandValidator, AddFineCommand, Result>
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int bookId, int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new AddFineCommand
                {
                    BookId = bookId,
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
