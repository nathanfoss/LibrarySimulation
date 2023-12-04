using Books.Application.Books;
using Books.Domain.Authors;
using Books.Domain.Books;
using Books.Domain.BookStatuses;
using Books.Domain.Events;
using Books.Domain.Genres;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using LibrarySimulation.Shared.Kernel.Enums;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class ReserveBookCommandTest : RequestTestBase<ReserveBookCommandHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        private readonly Mock<IBookBorrowEventService> mockBookBorrowEventService = new();

        public ReserveBookCommandTest()
        {
            handler = new ReserveBookCommandHandler(mockBookService.Object, mockBookBorrowEventService.Object, mockLogger.Object);
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldReserveBook((Book Book, bool Succeeded) testData)
        {
            // Given
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.Book);

            // When
            var result = await handler.Handle(new ReserveBookCommand
            {
                BookId = 1,
                PatronId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().Be(testData.Succeeded);
            if (testData.Succeeded)
            {
                mockBookService.Verify(x => x.Update(It.Is<Book>(b => b.StatusId == BookStatusEnum.Reserved)));
                mockBookBorrowEventService.Verify(x => x.Add(It.Is<BookBorrowEvent>(e => e.EventType == BorrowingRecordTypeEnum.Reserved)));
            }
            else
            {
                mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
            }
        }

        public static TheoryData<(Book, bool)> GenerateTestData =>
            new()
            {
                (default(Book), false),
                (new Book { StatusId = BookStatusEnum.Reserved }, false),
                (new Book { StatusId = BookStatusEnum.CheckedOut }, false),
                (new Book { StatusId = BookStatusEnum.Available }, true)
            };
    }
}
