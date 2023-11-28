namespace Books.Domain.Books
{
    public interface IBookService
    {
        Task<Book> Get(int id);

        Task<IEnumerable<Book>> GetAll();

        Task<IEnumerable<Book>> GetByAuthor(int authorId);

        Task<IEnumerable<Book>> Search(string searchText);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);
    }
}
