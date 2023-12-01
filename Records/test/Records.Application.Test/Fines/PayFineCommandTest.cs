using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class PayFineCommandTest : RequestTestBase<PayFineCommandHandler>
    {
        private readonly Mock<IFineService> mockFineService = new();

        public PayFineCommandTest()
        {
            handler = new PayFineCommandHandler(mockFineService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockFineService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new PayFineCommand { FineId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldPayFine((Fine Fine, int TimesCalled) testData)
        {
            // Given
            mockFineService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(testData.Fine);

            // When
            var result = await handler.Handle(new PayFineCommand { FineId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockFineService.Verify(x => x.Update(It.Is<Fine>(f => f.IsPaid && f.PaymentReceivedDate.Date == DateTime.UtcNow.Date)), Times.Exactly(testData.TimesCalled));
        }

        public static TheoryData<(Fine, int)> GenerateTestData =>
            new()
            {
                (null, 0),
                (new Fine{ IsPaid = true, PaymentReceivedDate = DateTime.UtcNow.AddDays(-5) }, 0),
                (new Fine{ IsPaid = false, Amount = .5m }, 1),
            };
    }
}
