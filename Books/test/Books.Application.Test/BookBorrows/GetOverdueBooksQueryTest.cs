using Books.Application.BookBorrows;
using Books.Domain.Borrows;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.BookBorrows
{
    public class GetOverdueBooksQueryTest : RequestTestBase<GetOverdueBooksQueryHandler>
    {
        private readonly Mock<IBookBorrowService> mockBookBorrowService = new();

        public GetOverdueBooksQueryTest()
        {
            handler = new GetOverdueBooksQueryHandler(mockBookBorrowService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookBorrowService.Setup(x => x.GetOverdue()).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetOverdueBooksQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error occurred");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockBookBorrowService.Setup(x => x.GetOverdue()).ReturnsAsync(new List<BookBorrow>());

            // When
            var result = await handler.Handle(new GetOverdueBooksQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
