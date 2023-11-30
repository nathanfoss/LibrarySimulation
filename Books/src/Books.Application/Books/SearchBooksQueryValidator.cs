using FluentValidation;

namespace Books.Application.Books
{
    public class SearchBooksQueryValidator : AbstractValidator<SearchBooksQuery>
    {
        public SearchBooksQueryValidator()
        {
            RuleFor(x => x.SearchText).NotEmpty();
        }
    }
}
