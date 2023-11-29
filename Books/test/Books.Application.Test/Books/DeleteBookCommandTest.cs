using Books.Application.Books;
using Books.Domain.Books;
using Books.Domain.BookStatuses;
using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.Application.Test.Books
{
    public class DeleteBookCommandTest : RequestTestBase<DeleteBookCommandHandler>
    {
        private readonly Mock<IBookService> mockBookService = new();

        public DeleteBookCommandTest()
        {
            handler = new DeleteBookCommandHandler(mockBookService.Object, mockLogger.Object);
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldDeleteBook((Book ExistingBook, bool Succeeded, int DeleteBookCalledTimes) testData)
        {
            // Given
            mockBookService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.ExistingBook);

            // When
            var result = await handler.Handle(new DeleteBookCommand { BookId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().Be(testData.Succeeded);
            mockBookService.Verify(x => x.Update(It.Is<Book>(b => b.IsDeleted && b.DeletedDate.HasValue && b.DeletedDate.Value.Date == DateTime.UtcNow.Date)), Times.Exactly(testData.DeleteBookCalledTimes));
            if (!testData.Succeeded)
            {
                mockLogger.VerifyLog(LogLevel.Error, "error occurred deleting book");
            }
        }

        public static TheoryData<(Book, bool, int)> GenerateTestData =>
            new TheoryData<(Book, bool, int)> {
                (default(Book), true, 0),
                (new Book { StatusId = BookStatusEnum.Available, IsDeleted = true }, true, 0),
                (new Book { StatusId = BookStatusEnum.CheckedOut }, false, 0),
                (new Book { StatusId = BookStatusEnum.Available, IsDeleted = false }, true, 1),
            };
    }
}
