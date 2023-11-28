using Books.Domain.Authors;
using Books.Domain.BookStatuses;
using Books.Domain.Genres;

namespace Books.Domain.Books
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public GenreEnum GenreId { get; set; }

        public Genre Genre { get; set; }

        public BookStatusEnum StatusId { get; set; }

        public BookStatus Status { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
