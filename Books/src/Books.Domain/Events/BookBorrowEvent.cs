using LibrarySimulation.Shared.Kernel.Enums;

namespace Books.Domain.Events
{
    public class BookBorrowEvent
    {
        public int Id { get; set; }

        public BorrowingRecordTypeEnum EventType { get; set; }

        public int BookId { get; set; }

        public int PatronId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
