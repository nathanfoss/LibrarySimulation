using FluentValidation;

namespace Books.Application.Books
{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
