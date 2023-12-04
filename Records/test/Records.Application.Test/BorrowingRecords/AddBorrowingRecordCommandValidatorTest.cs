using FluentValidation;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Shared.Kernel;
using LibrarySimulation.Shared.Kernel.Enums;
using Records.Application.Borrowing;
using Records.Application.Fines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records.Application.Test.BorrowingRecords
{
    public class AddBorrowingRecordCommandValidatorTest : RequestValidatorTestBase<AddBorrowingRecordCommandValidator, AddBorrowingRecordCommand, Result>
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        public async Task ShouldThrowExceptionWhenInputInvalid(int bookId, int patronId)
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await ValidationBehavior.Handle(
                new AddBorrowingRecordCommand
                {
                    BookId = bookId,
                    PatronId = patronId,
                    RecordTypeId = BorrowingRecordTypeEnum.CheckedOut
                }, () => null, CancellationToken.None));
        }
    }
}
