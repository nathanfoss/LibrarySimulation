using Books.Application.BookBorrows;
using Books.Domain.Borrows;
using Books.Domain.Events;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using LibrarySimulation.Shared.Kernel.Enums;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.BookBorrows
{
    public class RenewBookCommandTest : RequestTestBase<RenewBookCommandHandler>
    {
        private readonly Mock<IBookBorrowService> mockBookBorrowService = new();

        private readonly Mock<IBookBorrowEventService> mockBookBorrowEventService = new();

        public RenewBookCommandTest()
        {
            handler = new RenewBookCommandHandler(mockBookBorrowService.Object, mockBookBorrowEventService.Object, mockConfiguration.Object, mockLogger.Object);
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldRenewBookIfAble((BookBorrow Existing, bool Succeeded) testData)
        {
            // Given
            mockBookBorrowService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.Existing);
            SetupConfigurationGetValue("MaxRenewalCount", "3");
            SetupConfigurationGetValue("RenewalPeriodDays", "7");

            // When
            var result = await handler.Handle(new RenewBookCommand
            {
                BookBorrowId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().Be(testData.Succeeded);
            if (testData.Succeeded)
            {
                var expectedRenewalCount = 1;
                var expectedExpirationDate = DateTime.UtcNow.AddDays(8);
                mockBookBorrowService.Verify(x => x.Update(It.Is<BookBorrow>(bb => bb.RenewalCount == expectedRenewalCount && bb.ExpirationDate.Date == expectedExpirationDate.Date)));
                mockBookBorrowEventService.Verify(x => x.Add(It.Is<BookBorrowEvent>(e => e.EventType == BorrowingRecordTypeEnum.Renewed)));
            }
            else
            {
                mockLogger.VerifyLog(LogLevel.Error, "Unexpected error renewing book borrow");
            }
        }

        public static TheoryData<(BookBorrow, bool)> GenerateTestData =>
            new TheoryData<(BookBorrow, bool)>
            {
                (default(BookBorrow), false),
                (new BookBorrow { RenewalCount = 5}, false),
                (new BookBorrow { RenewalCount = 0, ExpirationDate = DateTime.UtcNow.AddDays(1)}, true)
            };
    }
}
