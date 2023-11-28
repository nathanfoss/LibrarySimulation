using Books.Domain.Books;

namespace Books.Client.Books
{
    public interface IBookClient
    {
        Task<Book> Get(int id);

        Task<IEnumerable<Book>> GetAll();

        Task<IEnumerable<Book>> GetByAuthor(int authorId);

        Task<IEnumerable<Book>> Search(string searchText);

        Task Add(Book book, string authorName);

        Task Delete(int bookId);
    }
}
