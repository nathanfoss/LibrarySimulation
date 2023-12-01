using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class AddFineCommandTest : RequestTestBase<AddFineCommandHandler>
    {
        private readonly Mock<IFineService> mockFineService = new();

        public AddFineCommandTest()
        {
            handler = new AddFineCommandHandler(mockFineService.Object, mockConfiguration.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockFineService.Setup(x => x.Add(It.IsAny<Fine>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new AddFineCommand(), CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldAddFine()
        {
            // Given
            SetupConfigurationGetValue("DefaultFineAmount", ".50");

            // When
            var result = await handler.Handle(new AddFineCommand
            {
                BookId = 1,
                PatronId = 1
            }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockFineService.Verify(x => x.Add(It.Is<Fine>(f => f.Amount == .5m && f.CreatedDate.Date == DateTime.UtcNow.Date)));
        }
    }
}
