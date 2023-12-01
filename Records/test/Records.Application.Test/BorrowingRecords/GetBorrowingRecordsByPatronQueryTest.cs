using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Borrowing;
using Records.Domain.Borrowing;

namespace Records.Application.Test.BorrowingRecords
{
    public class GetBorrowingRecordsByPatronQueryTest : RequestTestBase<GetBorrowingRecordsByPatronQueryHandler>
    {
        private readonly Mock<IBorrowingRecordService> mockBorrowingRecordService = new();

        public GetBorrowingRecordsByPatronQueryTest()
        {
            handler = new GetBorrowingRecordsByPatronQueryHandler(mockBorrowingRecordService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBorrowingRecordService.Setup(x => x.GetByPatron(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetBorrowingRecordsByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldGetAllBorrowingRecords()
        {
            // Given
            mockBorrowingRecordService.Setup(x => x.GetByPatron(It.IsAny<int>())).ReturnsAsync(new List<BorrowingRecord>());

            // When
            var result = await handler.Handle(new GetBorrowingRecordsByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
