using Books.Application.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class ReserveBookCommandValidatorTest : RequestValidatorTestBase<ReserveBookCommandValidator, ReserveBookCommand, Result>
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        public async Task ShouldThrowExceptionOnInvalidInput(int bookId, int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new ReserveBookCommand
                {
                    BookId = bookId,
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
