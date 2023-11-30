using Books.Application.Books;
using Books.Domain.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class AddBookCommandValidatorTest : RequestValidatorTestBase<AddBookCommandValidator, AddBookCommand, Result<Book>>
    {
        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldThrowExceptionOnInvalidInput((Book Book, string AuthorName) testData)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new AddBookCommand
                {
                    Book = testData.Book,
                    AuthorName = testData.AuthorName
                }, () => null, CancellationToken.None));
        }

        public static TheoryData<(Book, string)> GenerateTestData =>
            new()
            {
                (null, "Test Author"),
                (new Book { Title = "" }, "Test Author"),
                (new Book { Title = "  " }, "Test Author"),
                (new Book { Title = null }, "Test Author"),
                (new Book { Title = "Title" }, null),
                (new Book { Title = "Title" }, ""),
                (new Book { Title = "Title" }, "   ")
            };
    }
}
