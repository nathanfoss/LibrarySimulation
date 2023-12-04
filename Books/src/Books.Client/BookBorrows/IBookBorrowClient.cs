using Books.Domain.Borrows;

namespace Books.Client.BookBorrows
{
    public interface IBookBorrowClient
    {
        Task BorrowBook(int bookId, int patronId);

        Task RenewBook(int bookBorrowId);

        Task<IEnumerable<BookBorrow>> GetOverdueBooks();

        Task<IEnumerable<BookBorrow>> GetPatronOverdueBooks(int patronId);

        Task<IEnumerable<BookBorrow>> GetPatronBorrowedBooks(int patronId);

        Task<IEnumerable<BookBorrow>> GetAllPatronBorrowedBooks(int patronId);

        Task ReturnBook(int bookBorrowId);
    }
}
