using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Borrowing;
using Records.Domain.Borrowing;

namespace Records.Application.Test.BorrowingRecords
{
    public class GetBorrowingRecordsByBookQueryTest : RequestTestBase<GetBorrowingRecordsByBookQueryHandler>
    {
        private readonly Mock<IBorrowingRecordService> mockBorrowingRecordService = new();

        public GetBorrowingRecordsByBookQueryTest()
        {
            handler = new GetBorrowingRecordsByBookQueryHandler(mockBorrowingRecordService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBorrowingRecordService.Setup(x => x.GetByBook(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetBorrowingRecordsByBookQuery { BookId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldGetAllBorrowingRecords()
        {
            // Given
            mockBorrowingRecordService.Setup(x => x.GetByBook(It.IsAny<int>())).ReturnsAsync(new List<BorrowingRecord>());

            // When
            var result = await handler.Handle(new GetBorrowingRecordsByBookQuery { BookId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
