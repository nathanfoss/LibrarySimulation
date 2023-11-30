using FluentValidation;

namespace Books.Application.BookBorrows
{
    public class GetPatronOverdueBooksQueryValidator : AbstractValidator<GetPatronOverdueBooksQuery>
    {
        public GetPatronOverdueBooksQueryValidator()
        {
            RuleFor(x => x.PatronId).GreaterThan(0);
        }
    }
}
