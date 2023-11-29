using Books.Application.BookBorrows;
using Books.Domain.Books;
using Books.Domain.BookStatuses;
using Books.Domain.Borrows;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Test.BookBorrows
{
    public class BorrowBookCommandTest : RequestTestBase<BorrowBookCommandHandler>
    {
        private readonly Mock<IBookBorrowService> mockBookBorrowService = new();

        private readonly Mock<IBookService> mockBookService = new();

        public BorrowBookCommandTest()
        {
            handler = new BorrowBookCommandHandler(mockBookBorrowService.Object, mockBookService.Object, mockConfiguration.Object, mockLogger.Object);
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldReserveBook((Book ExistingBook, bool Succeeded) testData)
        {
            // Given
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.ExistingBook);
            SetupConfigurationGetValue("DefaultBorrowingPeriodDays", "7");

            // When
            var result = await handler.Handle(new BorrowBookCommand
            {
                BookId = 1,
                PatronId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().Be(testData.Succeeded);
            if (testData.Succeeded)
            {
                mockBookBorrowService.Verify(x => x.Add(It.Is<BookBorrow>(bb => bb.CreatedDate.Date == DateTime.UtcNow.Date)));
            }
            else
            {
                mockLogger.VerifyLog(LogLevel.Error, "Error borrowing book");
            }
        }

        public static TheoryData<(Book, bool)> GenerateTestData =>
            new TheoryData<(Book, bool)>
            {
                (default(Book), false),
                (new Book { StatusId = BookStatusEnum.CheckedOut}, false),
                (new Book { StatusId = BookStatusEnum.Available}, true)
            };
    }
}
