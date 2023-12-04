using Books.Application.BookBorrows;
using Books.Domain.Books;
using Books.Domain.BookStatuses;
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
    public class ReturnBookCommandTest : RequestTestBase<ReturnBookCommandHandler>
    {
        private readonly Mock<IBookBorrowService> mockBookBorrowService = new();

        private readonly Mock<IBookBorrowEventService> mockBookBorrowEventService = new();

        private readonly Mock<IBookService> mockBookService = new();

        public ReturnBookCommandTest()
        {
            handler = new ReturnBookCommandHandler(mockBookBorrowService.Object, mockBookService.Object, mockBookBorrowEventService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockBookBorrowService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new ReturnBookCommand { BookBorrowId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldReturnBook((BookBorrow Borrow, Book ExistingBook, int TimesCalled) testData)
        {
            // Given
            mockBookBorrowService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.Borrow);
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.ExistingBook);

            // When
            var result = await handler.Handle(new ReturnBookCommand
            {
                BookBorrowId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockBookBorrowService.Verify(x => x.Update(It.Is<BookBorrow>(bb => bb.IsClosed)), Times.Exactly(testData.TimesCalled));
            mockBookService.Verify(x => x.Update(It.Is<Book>(b => b.StatusId == BookStatusEnum.Available)), Times.Exactly(testData.TimesCalled));
            mockBookBorrowEventService.Verify(x => x.Add(It.Is<BookBorrowEvent>(e => e.EventType == BorrowingRecordTypeEnum.CheckedIn)), Times.Exactly(testData.TimesCalled));
        }

        public static TheoryData<(BookBorrow, Book, int)> GenerateTestData =>
            new TheoryData<(BookBorrow, Book, int)>
            {
                (default(BookBorrow), default(Book), 0),
                (new BookBorrow { IsClosed = true }, default(Book), 0),
                (new BookBorrow { IsClosed = false }, new Book { StatusId = BookStatusEnum.CheckedOut }, 1)
            };
    }
}
