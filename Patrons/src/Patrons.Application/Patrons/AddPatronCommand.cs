using LibrarySimulation.Shared.Kernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Patrons
{
    public class AddPatronCommand : IRequest<Result<Patron>>
    {
        public string Name { get; set; }
    }

    public class AddPatronCommandHandler : IRequestHandler<AddPatronCommand, Result<Patron>>
    {
        private readonly IPatronService patronService;

        private readonly ILogger<AddPatronCommandHandler> logger;

        public AddPatronCommandHandler(IPatronService patronService, ILogger<AddPatronCommandHandler> logger)
        {
            this.patronService = patronService;
            this.logger = logger;
        }

        public async Task<Result<Patron>> Handle(AddPatronCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var added = await patronService.Add(new Patron
                {
                    Name = request.Name,
                    CardNumber = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                });
                return Result<Patron>.Success(added);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred");
                return Result<Patron>.Failure(ex);
            }
        }
    }
}
