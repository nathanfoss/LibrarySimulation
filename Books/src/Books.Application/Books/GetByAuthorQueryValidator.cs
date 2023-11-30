using FluentValidation;

namespace Books.Application.Books
{
    public class GetByAuthorQueryValidator : AbstractValidator<GetByAuthorQuery>
    {
        public GetByAuthorQueryValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
