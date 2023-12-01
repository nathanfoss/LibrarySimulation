using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class GetUnpaidFinesByPatronQueryTest : RequestTestBase<GetUnpaidFinesByPatronQueryHandler>
    {
        private readonly Mock<IFineService> mockFineService = new();

        public GetUnpaidFinesByPatronQueryTest()
        {
            handler = new GetUnpaidFinesByPatronQueryHandler(mockFineService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockFineService.Setup(x => x.GetUnpaidByPatron(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new GetUnpaidFinesByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldGetUnpaidFines()
        {
            // Given
            mockFineService.Setup(x => x.GetUnpaidByPatron(It.IsAny<int>())).ReturnsAsync(new List<Fine>());

            // When
            var result = await handler.Handle(new GetUnpaidFinesByPatronQuery { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
        }
    }
}
