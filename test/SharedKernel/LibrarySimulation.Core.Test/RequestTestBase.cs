using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibrarySimulation.Core.Test
{
    public class RequestTestBase<T>
    {
        public readonly Mock<ILogger<T>> mockLogger = new();

        public readonly Mock<IConfiguration> mockConfiguration = new();

        public T handler { get; set; }

        public void SetupConfigurationGetValue(string key, string value)
        {
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns(value);
            mockConfiguration.Setup(x => x.GetSection(key)).Returns(mockSection.Object);
        }
    }
}