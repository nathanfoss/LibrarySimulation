namespace Books.Domain.Events
{
    public interface IBookBorrowEventService
    {
        Task<IEnumerable<BookBorrowEvent>> GetAll();

        Task Add(BookBorrowEvent bookBorrowEvent);

        Task Add(IEnumerable<BookBorrowEvent> bookBorrowEvent);

        Task Delete(BookBorrowEvent bookBorrowEvent);

        Task Delete(IEnumerable<BookBorrowEvent> bookBorrowEvents);
    }
}
