using MediatR;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Client.Fines
{
    public class FineClient : IFineClient
    {
        private readonly IMediator mediator;

        public FineClient(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Add(int bookId, int patronId)
        {
            await mediator.Send(new AddFineCommand { BookId = bookId, PatronId = patronId });
        }

        public async Task<IEnumerable<Fine>> GetAll(int patronId)
        {
            var result = await mediator.Send(new GetAllFinesByPatronQuery { PatronId = patronId });
            return result.Response;
        }

        public async Task<IEnumerable<Fine>> GetUnpaid(int patronId)
        {
            var result = await mediator.Send(new GetUnpaidFinesByPatronQuery { PatronId = patronId });
            return result.Response;
        }

        public async Task Pay(int fineId)
        {
            await mediator.Send(new PayFineCommand { FineId = fineId });
        }

        public async Task PayAll(int patronId)
        {
            await mediator.Send(new PayFinesCommand { PatronId = patronId });
        }
    }
}
