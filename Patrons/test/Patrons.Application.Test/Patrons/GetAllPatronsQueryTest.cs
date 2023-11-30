using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class GetAllPatronsQueryTest : RequestTestBase<GetAllPatronsQueryHandler>
    {
        private readonly Mock<IPatronService> mockPatronService = new();

        public GetAllPatronsQueryTest()
        {
            handler = new GetAllPatronsQueryHandler(mockPatronService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockPatronService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetAllPatronsQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockPatronService.Setup(x => x.GetAll()).ReturnsAsync(new List<Patron>());

            // When
            var result = await handler.Handle(new GetAllPatronsQuery(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
