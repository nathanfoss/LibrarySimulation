using Books.Application.Books;
using Books.Domain.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class GetByAuthorQueryValidatorTest : RequestValidatorTestBase<GetByAuthorQueryValidator, GetByAuthorQuery, Result<IEnumerable<Book>>>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowExceptionOnInvalidInput(int authorId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetByAuthorQuery
                {
                    AuthorId = authorId
                }, () => null, CancellationToken.None));
        }
    }
}
