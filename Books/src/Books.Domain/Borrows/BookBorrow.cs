using Books.Domain.Books;

namespace Books.Domain.Borrows
{
    public class BookBorrow
    {
        public int Id { get; set; }

        public int PatronId { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsClosed { get; set; }

        public int RenewalCount { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
