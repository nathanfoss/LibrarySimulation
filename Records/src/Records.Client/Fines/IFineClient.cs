using Records.Domain.Fines;

namespace Records.Client.Fines
{
    public interface IFineClient
    {
        Task Add(int bookId, int patronId);

        Task Pay(int fineId);

        Task PayAll(int patronId);

        Task<IEnumerable<Fine>> GetAll(int patronId);

        Task<IEnumerable<Fine>> GetUnpaid(int patronId);
    }
}
