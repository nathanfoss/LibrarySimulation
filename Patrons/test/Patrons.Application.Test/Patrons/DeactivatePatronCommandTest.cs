using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Patrons.Application.Patrons;
using Patrons.Domain.Patrons;

namespace Patrons.Application.Test.Patrons
{
    public class DeactivatePatronCommandTest : RequestTestBase<DeactivatePatronCommandHandler>
    {
        private readonly Mock<IPatronService> mockPatronService = new();

        public DeactivatePatronCommandTest()
        {
            handler = new DeactivatePatronCommandHandler(mockPatronService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockPatronService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new DeactivatePatronCommand { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldReturnSuccess((Patron ExistingPatron, int TimesCalled) testData)
        {
            // Given
            mockPatronService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.ExistingPatron);

            // When
            var result = await handler.Handle(new DeactivatePatronCommand { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockPatronService.Verify(x => x.Update(It.Is<Patron>(p => !p.IsActive && p.DeactivatedDate.HasValue && p.DeactivatedDate.Value.Date == DateTime.UtcNow.Date)), Times.Exactly(testData.TimesCalled));
        }

        public static TheoryData<(Patron, int)> GenerateTestData =>
            new TheoryData<(Patron, int)>
            {
                (default(Patron), 0),
                (new Patron { IsActive = false }, 0),
                (new Patron { IsActive = true }, 1),
            };
    }
}
