using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using Records.Application.Borrowing;
using Records.Domain.Borrowing;

namespace Records.Application.Test.BorrowingRecords
{
    public class GetBorrowingRecordsByBookQueryValidatorTest : RequestValidatorTestBase<GetBorrowingRecordsByBookQueryValidator, GetBorrowingRecordsByBookQuery, Result<IEnumerable<BorrowingRecord>>>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int bookId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new GetBorrowingRecordsByBookQuery
                {
                    BookId = bookId
                }, () => null, CancellationToken.None));
        }
    }
}
