using Books.Application.BookBorrows;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.BookBorrows
{
    public class BorrowBookCommandValidatorTest : RequestValidatorTestBase<BorrowBookCommandValidator, BorrowBookCommand, Result>
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        public async Task ShouldThrowExceptionWhenInvalidInput(int bookId, int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new BorrowBookCommand
                {
                    BookId = bookId,
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
