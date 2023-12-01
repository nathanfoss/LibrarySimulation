using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Borrowing;
using Records.Domain.Borrowing;

namespace Records.Application.Test.BorrowingRecords
{
    public class AddBorrowingRecordCommandTest : RequestTestBase<AddBorrowingRecordCommandHandler>
    {
        private readonly Mock<IBorrowingRecordService> mockBorrowingRecordService = new();

        public AddBorrowingRecordCommandTest()
        {
            handler = new AddBorrowingRecordCommandHandler(mockBorrowingRecordService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBorrowingRecordService.Setup(x => x.Add(It.IsAny<BorrowingRecord>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new AddBorrowingRecordCommand { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldAddBorrowingRecord()
        {
            // Given

            // When
            var result = await handler.Handle(new AddBorrowingRecordCommand
            {
                PatronId = 1,
                BookId = 1,
                RecordTypeId = BorrowingRecordTypeEnum.CheckedOut
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockBorrowingRecordService.Verify(x => x.Add(It.Is<BorrowingRecord>(br => br.CreatedDate.Date == DateTime.UtcNow.Date)));
        }
    }
}
