using Books.Application.BookBorrows;
using Books.Domain.Borrows;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.BookBorrows
{
    public class GetPatronBorrowedBooksQueryTest : RequestTestBase<GetPatronBorrowedBooksQueryHandler>
    {
        private readonly Mock<IBookBorrowService> mockBookBorrowService = new();

        public GetPatronBorrowedBooksQueryTest()
        {
            handler = new GetPatronBorrowedBooksQueryHandler(mockBookBorrowService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookBorrowService.Setup(x => x.GetByPatron(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetPatronBorrowedBooksQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error occurred");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookBorrowService.Setup(x => x.GetByPatron(It.IsAny<int>())).ReturnsAsync(new List<BookBorrow>());

            // When
            var result = await handler.Handle(new GetPatronBorrowedBooksQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
