using Books.Application.Books;
using Books.Domain.Books;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class GetAllBooksQueryTest : RequestTestBase<GetAllBooksQueryHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        public GetAllBooksQueryTest()
        {
            handler = new GetAllBooksQueryHandler(mockBookService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Error fetching all books");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookService.Setup(x => x.GetAll()).ReturnsAsync(new List<Book>());

            // When
            var result = await handler.Handle(new GetAllBooksQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
