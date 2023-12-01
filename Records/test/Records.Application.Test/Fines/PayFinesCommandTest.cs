using FluentAssertions;
using LibrarySimulation.Core.Test;
using LibrarySimulation.Core.Test.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using Records.Application.Fines;
using Records.Domain.Fines;

namespace Records.Application.Test.Fines
{
    public class PayFinesCommandTest : RequestTestBase<PayFinesCommandHandler>
    {
        private readonly Mock<IFineService> mockFineService = new();

        public PayFinesCommandTest()
        {
            handler = new PayFinesCommandHandler(mockFineService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task ShouldLogErrorWhenExceptionThrown()
        {
            // Given
            mockFineService.Setup(x => x.GetUnpaidByPatron(It.IsAny<int>())).ThrowsAsync(new Exception());

            // When
            var result = await handler.Handle(new PayFinesCommand { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeFalse();
            mockLogger.VerifyLog(LogLevel.Error, "Unexpected error");
        }

        [Theory]
        [MemberData(nameof(GenerateTestData))]
        public async Task ShouldPayFines((IEnumerable<Fine> Fines, int TimesCalled) testData)
        {
            // Given
            mockFineService.Setup(x => x.GetUnpaidByPatron(It.IsAny<int>())).ReturnsAsync(testData.Fines);

            // When
            var result = await handler.Handle(new PayFinesCommand { PatronId = 1 }, CancellationToken.None);

            // Then
            result.Succeeded.Should().BeTrue();
            mockFineService.Verify(x => x.Update(It.Is<IEnumerable<Fine>>(l => l.All(f => f.IsPaid && f.PaymentReceivedDate.Date == DateTime.UtcNow.Date))), Times.Exactly(testData.TimesCalled));
        }

        public static TheoryData<(IEnumerable<Fine>, int)> GenerateTestData =>
            new()
            {
                (new List<Fine>(), 0),
                (new List<Fine>{ new Fine { IsPaid = false, Amount = .5m } }, 1),
            };
    }
}
