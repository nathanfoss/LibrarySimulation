using Books.Application.Books;
using Books.Domain.Books;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class GetByAuthorQueryTest : RequestTestBase<GetByAuthorQueryHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        public GetByAuthorQueryTest()
        {
            handler = new GetByAuthorQueryHandler(mockBookService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookService.Setup(x => x.GetByAuthor(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetByAuthorQuery
            {
                AuthorId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Error getting books for author");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookService.Setup(x => x.GetByAuthor(It.IsAny<int>())).ReturnsAsync(new List<Book>());

            // When
            var result = await handler.Handle(new GetByAuthorQuery
            {
                AuthorId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
