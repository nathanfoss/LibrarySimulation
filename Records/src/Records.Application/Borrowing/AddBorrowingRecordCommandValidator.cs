using FluentValidation;

namespace Records.Application.Borrowing
{
    public class AddBorrowingRecordCommandValidator : AbstractValidator<AddBorrowingRecordCommand>
    {
        public AddBorrowingRecordCommandValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
