namespace Records.Domain.Borrowing
{
    public class BorrowingRecord
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int PatronId { get; set; }

        public BorrowingRecordTypeEnum RecordTypeId { get; set; }

        public BorrowingRecordType RecordType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
