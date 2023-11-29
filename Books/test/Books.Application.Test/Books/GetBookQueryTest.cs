using Books.Application.Books;
using Books.Domain.Books;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class GetBookQueryTest : RequestTestBase<GetBookQueryHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        public GetBookQueryTest()
        {
            handler = new GetBookQueryHandler(mockBookService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetBookQuery
            {
                BookId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Error fetching book");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new Book());

            // When
            var result = await handler.Handle(new GetBookQuery
            {
                BookId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
