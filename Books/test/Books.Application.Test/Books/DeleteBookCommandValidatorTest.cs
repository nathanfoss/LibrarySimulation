using Books.Application.Books;
using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;

namespace Books.Application.Test.Books
{
    public class DeleteBookCommandValidatorTest : RequestValidatorTestBase<DeleteBookCommandValidator, DeleteBookCommand, Result>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowExceptionOnInvalidInput(int bookId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new DeleteBookCommand
                {
                    BookId = bookId
                }, () => null, CancellationToken.None));
        }
    }
}
