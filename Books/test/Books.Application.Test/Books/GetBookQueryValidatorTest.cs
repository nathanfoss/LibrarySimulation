using Books.Application.Books;
using Books.Domain.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class GetBookQueryValidatorTest : RequestValidatorTestBase<GetBookQueryValidator, GetBookQuery, Result<Book>>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowExceptionOnInvalidInput(int bookId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetBookQuery
                {
                    BookId = bookId
                }, () => null, CancellationToken.None));
        }
    }
}
