namespace Books.Domain.Borrows
{
    public interface IBookBorrowService
    {
        Task<BookBorrow> Get(int id);

        Task<BookBorrow> Add(BookBorrow bookBorrow);

        Task<BookBorrow> Update(BookBorrow bookBorrow);

        Task<IEnumerable<BookBorrow>> GetOverdue();

        Task<IEnumerable<BookBorrow>> GetOverdue(int patronId);

        Task<IEnumerable<BookBorrow>> GetByPatron(int patronId);

        Task<IEnumerable<BookBorrow>> GetAllByPatron(int patronId);

        Task<IEnumerable<BookBorrow>> GetByBook(int bookId);
    }
}
