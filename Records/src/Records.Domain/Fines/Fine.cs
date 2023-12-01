namespace Records.Domain.Fines
{
    public class Fine
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int PatronId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsPaid { get; set; }

        public DateTime PaymentReceivedDate { get; set; }
    }
}
