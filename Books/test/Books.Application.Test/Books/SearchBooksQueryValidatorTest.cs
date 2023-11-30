using Books.Application.Books;
using Books.Domain.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class SearchBooksQueryValidatorTest : RequestValidatorTestBase<SearchBooksQueryValidator, SearchBooksQuery, Result<IEnumerable<Book>>>
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowExceptionOnInvalidInput(string searchText)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new SearchBooksQuery
                {
                    SearchText = searchText
                }, () => null, CancellationToken.None));
        }
    }
}
