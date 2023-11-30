using FluentValidation;

namespace Books.Application.Books
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.Book).NotNull();
            RuleFor(x => x.AuthorName).NotEmpty();
            RuleFor(x => x.Book.Title).NotEmpty();
        }
    }
}
