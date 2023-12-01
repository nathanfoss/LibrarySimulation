using FluentValidation;

namespace Records.Application.Borrowing
{
    public class GetBorrowingRecordsByPatronQueryValidator : AbstractValidator<GetBorrowingRecordsByPatronQuery>
    {
        public GetBorrowingRecordsByPatronQueryValidator()
        {
            RuleFor(x => x.PatronId);
        }
    }
}
