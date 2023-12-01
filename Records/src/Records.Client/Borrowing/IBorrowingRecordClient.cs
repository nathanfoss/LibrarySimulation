using Records.Domain.Borrowing;

namespace Records.Client.Borrowing
{
    public interface IBorrowingRecordClient
    {
        Task Add(int bookId, int patronId, BorrowingRecordTypeEnum recordTypeId);

        Task<IEnumerable<BorrowingRecord>> GetByBook(int bookId);

        Task<IEnumerable<BorrowingRecord>> GetByPatron(int patronId);
    }
}
