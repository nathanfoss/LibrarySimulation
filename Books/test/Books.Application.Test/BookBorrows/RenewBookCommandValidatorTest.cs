using Books.Application.BookBorrows;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.BookBorrows
{
    public class RenewBookCommandValidatorTest : RequestValidatorTestBase<RenewBookCommandValidator, RenewBookCommand, Result>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInvalidInput(int bookBorrowId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new RenewBookCommand
                {
                    BookBorrowId = bookBorrowId
                }, () => null, CancellationToken.None));
        }
    }
}
