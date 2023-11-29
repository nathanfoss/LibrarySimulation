using Books.Application.Books;
using Books.Domain.Authors;
using Books.Domain.Books;
using Books.Domain.Genres;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class AddBookCommandTest : RequestTestBase<AddBookCommandHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        private readonly Mock<IAuthorService> mockAuthorService = new();

        public AddBookCommandTest()
        {
            handler = new AddBookCommandHandler(mockBookService.Object, mockAuthorService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockAuthorService.Setup(x => x.Get(It.IsAny<string>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new AddBookCommand(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "error occurred adding book");
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldAddBook((Book Book, Author ExistingAuthor, int TimesAddingAuthor) testData)
        {
            // Given
            mockAuthorService.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(testData.ExistingAuthor);
            mockAuthorService.Setup(x => x.Add(It.IsAny<Author>())).ReturnsAsync((Author author) => new Author
            {
                FullName = author.FullName,
                Id = 1
            });

            // When
            var result = await handler.Handle(new AddBookCommand
            {
                AuthorName = "Test Author",
                Book = testData.Book
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockBookService.Verify(x => x.Add(It.Is<Book>(b => b.DateAdded.Date == DateTime.UtcNow.Date)));
            mockAuthorService.Verify(x => x.Add(It.IsAny<Author>()), Times.Exactly(testData.TimesAddingAuthor));
        }

        public static TheoryData<(Book, Author, int)> GenerateTestData =>
            new()
            {
                (new Book { Title = "Some Book", GenreId = GenreEnum.NonFiction }, default(Author), 1),
                (new Book { Title = "Some Book", GenreId = GenreEnum.NonFiction }, new Author { Id = 1, FullName = "Test Author" }, 0)
            };
    }
}
