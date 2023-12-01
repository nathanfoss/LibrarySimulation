using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class GetAllFinesByPatronQueryTest : RequestTestBase<GetAllFinesByPatronQueryHandler>
    {
        private readonly Mock<IFineService> mockFineService = new();

        public GetAllFinesByPatronQueryTest()
        {
            handler = new GetAllFinesByPatronQueryHandler(mockFineService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockFineService.Setup(x => x.GetAllByPatron(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetAllFinesByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldGetAllFines()
        {
            // Given
            mockFineService.Setup(x => x.GetAllByPatron(It.IsAny<int>())).ReturnsAsync(new List<Fine>());

            // When
            var result = await handler.Handle(new GetAllFinesByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
