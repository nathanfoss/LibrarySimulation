using MediatR;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Client.Patrons
{
    public class PatronClient : IPatronClient
    {
        private readonly IMediator mediator;

        public PatronClient(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Patron> Add(string name)
        {
            var result = await mediator.Send(new AddPatronCommand { Name = name });
            return result.Response;
        }

        public async Task Deactivate(int id)
        {
            await mediator.Send(new DeactivatePatronCommand { PatronId = id });
        }

        public async Task<Patron> Get(int id)
        {
            var result = await mediator.Send(new GetPatronQuery { Id = id });
            return result.Response;
        }

        public async Task<IEnumerable<Patron>> GetAll()
        {
            var result = await mediator.Send(new GetAllPatronsQuery());
            return result.Response;
        }

        public async Task<IEnumerable<Patron>> Search(string searchText)
        {
            var result = await mediator.Send(new SearchPatronsQuery { SearchText = searchText });
            return result.Response;
        }
    }
}
