using Books.Application.Books;
using Books.Domain.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Client.Books
{
    public class BookClient : IBookClient
    {
        private readonly IMediator mediator;

        public BookClient(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Add(Book book, string authorName)
        {
            await mediator.Send(new AddBookCommand
            {
                Book = book,
                AuthorName = authorName
            });
        }

        public async Task Delete(int bookId)
        {
            await mediator.Send(new DeleteBookCommand { BookId = bookId });
        }

        public async Task<Book> Get(int id)
        {
            var result = await mediator.Send(new GetBookQuery { BookId = id });
            return result.Response;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var result = await mediator.Send(new GetAllBooksQuery());
            return result.Response;
        }

        public async Task<IEnumerable<Book>> GetByAuthor(int authorId)
        {
            var result = await mediator.Send(new GetByAuthorQuery { AuthorId = authorId });
            return result.Response;
        }

        public async Task<IEnumerable<Book>> Search(string searchText)
        {
            var result = await mediator.Send(new SearchBooksQuery { SearchText = searchText });
            return result.Response;
        }
    }
}
