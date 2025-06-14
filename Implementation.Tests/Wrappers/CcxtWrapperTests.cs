using Moq;
using AutoFixture;
using ccxt;
using Implementation.Wrappers;
using Implementation.Utils;
using Microsoft.Extensions.Logging;

namespace Implementation.Tests.Wrappers
{
    public class CcxtWrapperTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IExchangeFactory> _factoryMock;
        private readonly Mock<ILogger<CcxtWrapper>> _loggerMock;
        private readonly Mock<Exchange> _exchangeMock;

        public CcxtWrapperTests()
        {
            _fixture = new Fixture();
            _factoryMock = new Mock<IExchangeFactory>();
            _loggerMock = new Mock<ILogger<CcxtWrapper>>();

            // SOLUTION 1: Mock avec CallBase = true et Setup protégé
            _exchangeMock = new Mock<Exchange> { CallBase = true };
        }

        private CcxtWrapper CreateWrapper(bool authenticated = true)
        {
            var apiKey = authenticated ? _fixture.Create<string>() : null;
            var secret = authenticated ? _fixture.Create<string>() : null;

            _factoryMock.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                        .Returns(_exchangeMock.Object);

            return new CcxtWrapper(_factoryMock.Object, _loggerMock.Object, "binance", apiKey, secret);
        }

        [Fact]
        public void Constructor_WithNullLogger_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CcxtWrapper(_factoryMock.Object, null, "binance", "apiKey", "secret")
            );
        }

        [Fact]
        public void Constructor_WithNullFactory_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CcxtWrapper(null, _loggerMock.Object, "binance", "apiKey", "secret")
            );
        }

        [Fact]
        public async Task GetBalanceAsync_WithoutAuthentication_Throws()
        {
            // Arrange
            var wrapper = new CcxtWrapper(_factoryMock.Object, _loggerMock.Object, "binance");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => wrapper.GetBalanceAsync("BTC"));
            Assert.Equal("Clés API requises pour accéder au solde.", ex.Message);

            _loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Clés API requises")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WithAssetFound_ReturnsBalance()
        {
            // Arrange  
            var asset = "BTC";
            var expected = 1.2345;
            var balance = new Balances
            {
                total = new Dictionary<string, double>
                {
                    { asset, expected }
                }
            };

            // SOLUTION: Utilisation de Setup avec expression Lambda
            _exchangeMock.Setup(x => x.FetchBalance())
                        .Returns(Task.FromResult(balance));

            var wrapper = CreateWrapper();

            // Act  
            var result = await wrapper.GetBalanceAsync(asset);

            // Assert  
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetBalanceAsync_WithAssetNotFound_ReturnsZeroAndLogsWarning()
        {
            // Arrange
            var requestedAsset = "ETH";
            var balance = new Balances
            {
                total = new Dictionary<string, double>
                {
                    { "BTC", 1.2345 },
                    { "ADA", 2.5678 }
                }
            };

            _exchangeMock.Setup(x => x.FetchBalance())
                        .Returns(Task.FromResult(balance));

            var wrapper = CreateWrapper();

            // Act
            var result = await wrapper.GetBalanceAsync(requestedAsset);

            // Assert
            Assert.Equal(0.0, result);

            _loggerMock.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"Actif '{requestedAsset}' non trouvé")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WithEmptyBalances_ReturnsZeroAndLogsWarning()
        {
            // Arrange
            var asset = "BTC";
            var balance = new Balances
            {
                total = new Dictionary<string, double>()
            };

            _exchangeMock.Setup(x => x.FetchBalance())
                        .Returns(Task.FromResult(balance));
            var wrapper = CreateWrapper();

            // Act
            var result = await wrapper.GetBalanceAsync(asset);

            // Assert
            Assert.Equal(0.0, result);

            _loggerMock.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"Actif '{asset}' non trouvé")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Theory]
        [InlineData("", "valid-secret")]
        [InlineData(null, "valid-secret")]
        [InlineData("valid-api-key", "")]
        [InlineData("valid-api-key", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        public async Task GetBalanceAsync_WithInvalidCredentials_ThrowsInvalidOperationException(string apiKey, string secret)
        {
            // Arrange
            _factoryMock.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                        .Returns(_exchangeMock.Object);

            var wrapper = new CcxtWrapper(_factoryMock.Object, _loggerMock.Object, "binance", apiKey, secret);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => wrapper.GetBalanceAsync("BTC"));

            Assert.Equal("Clés API requises pour accéder au solde.", exception.Message);
        }

        [Fact]
        public async Task GetBalanceAsync_WithNullBalances_ThrowsNullReferenceException()
        {
            // Arrange
            _exchangeMock.Setup(x => x.FetchBalance())
                        .Returns(Task.FromResult<Balances>(null));
            var wrapper = CreateWrapper();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(
                () => wrapper.GetBalanceAsync("BTC"));
        }

        [Fact]
        public async Task GetBalanceAsync_WithZeroBalance_ReturnsZero()
        {
            // Arrange
            var asset = "BTC";
            var balance = new Balances
            {
                total = new Dictionary<string, double>
                {
                    { asset, 0.0 }
                }
            };

            _exchangeMock.Setup(x => x.FetchBalance())
                        .Returns(Task.FromResult(balance));
            var wrapper = CreateWrapper();

            // Act
            var result = await wrapper.GetBalanceAsync(asset);

            // Assert
            Assert.Equal(0.0, result);
        }
    }
}