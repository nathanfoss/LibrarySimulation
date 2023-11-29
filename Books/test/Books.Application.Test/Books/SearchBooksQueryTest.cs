using Books.Application.Books;
using Books.Domain.Books;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class SearchBooksQueryTest : RequestTestBase<SearchBooksQueryHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        public SearchBooksQueryTest()
        {
            handler = new SearchBooksQueryHandler(mockBookService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookService.Setup(x => x.Search(It.IsAny<string>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new SearchBooksQuery
            {
                SearchText = "Something"
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Error searching books");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookService.Setup(x => x.Search(It.IsAny<string>())).ReturnsAsync(new List<Book>());

            // When
            var result = await handler.Handle(new SearchBooksQuery
            {
                SearchText = "Something"
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
