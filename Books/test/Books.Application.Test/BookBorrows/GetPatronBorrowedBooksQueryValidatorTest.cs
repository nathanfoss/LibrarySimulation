using Books.Application.BookBorrows;
using Books.Domain.Borrows;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.BookBorrows
{
    public class GetPatronBorrowedBooksQueryValidatorTest : RequestValidatorTestBase<GetPatronBorrowedBooksQueryValidator, GetPatronBorrowedBooksQuery, Result<IEnumerable<BookBorrow>>>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInvalidInput(int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetPatronBorrowedBooksQuery
                {
                    PatronId = patronId
                }, () => null, CancellationToken.None));
        }
    }
}
