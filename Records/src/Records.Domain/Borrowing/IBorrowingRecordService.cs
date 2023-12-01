namespace Records.Domain.Borrowing
{
    public interface IBorrowingRecordService
    {
        Task Add(BorrowingRecord record);

        Task<IEnumerable<BorrowingRecord>> GetByBook(int bookId);

        Task<IEnumerable<BorrowingRecord>> GetByPatron(int patronId);
    }
}
