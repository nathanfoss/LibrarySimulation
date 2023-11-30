namespace Patrons.Domain.Patrons
{
    public class Patron
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid CardNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedDate { get; set; }
    }
}
