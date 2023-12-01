using FluentValidation;

namespace Records.Application.Borrowing
{
    public class GetBorrowingRecordsByBookQueryValidator : AbstractValidator<GetBorrowingRecordsByBookQuery>
    {
        public GetBorrowingRecordsByBookQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
