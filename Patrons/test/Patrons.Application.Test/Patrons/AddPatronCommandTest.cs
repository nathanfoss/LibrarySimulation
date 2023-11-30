using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class AddPatronCommandTest : RequestTestBase<AddPatronCommandHandler>
    {
        private readonly Mock<IPatronService> mockPatronService = new();

        public AddPatronCommandTest()
        {
            handler = new AddPatronCommandHandler(mockPatronService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockPatronService.Setup(x => x.Add(It.IsAny<Patron>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new AddPatronCommand { Name = "Test Patron" }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            // Given
            mockPatronService.Setup(x => x.Add(It.IsAny<Patron>())).ReturnsAsync((Patron patron) => patron);

            // When
            var result = await handler.Handle(new AddPatronCommand { Name = "Test Patron" }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockPatronService.Verify(x => x.Add(It.Is<Patron>(p => p.IsActive && p.CreatedDate.Date == DateTime.UtcNow.Date)));
        }
    }
}
