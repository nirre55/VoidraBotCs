using Moq;
using AutoFixture;
using ccxt;
using Implementation.Wrappers;
using Microsoft.Extensions.Logging;
using Implementation.Wrappers.Interfaces;
#nullable enable
namespace Implementation.Tests.Wrappers
{

    public class CcxtWrapperTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IExchangeWrapper> _exchangeMock;
        private readonly Mock<ILogger<CcxtWrapper>> _loggerMock;

        public CcxtWrapperTests()
        {
            _fixture = new Fixture();
            _exchangeMock = new Mock<IExchangeWrapper>();
            _loggerMock = new Mock<ILogger<CcxtWrapper>>();
        }

        private CcxtWrapper CreateWrapper(bool authenticated = true)
        {
            var apiKey = authenticated ? _fixture.Create<string>() : null;
            var secret = authenticated ? _fixture.Create<string>() : null;

            return new CcxtWrapper(_exchangeMock.Object, _loggerMock.Object, apiKey, secret);
        }

        [Fact]
        public void Constructor_WithNullLogger_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CcxtWrapper(_exchangeMock.Object, null, "apiKey", "secret")
            );
        }

        [Fact]
        public void Constructor_WithNullExchange_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CcxtWrapper(null, _loggerMock.Object, "apiKey", "secret")
            );
        }

        [Fact]
        public async Task GetBalanceAsync_WithoutAuthentication_Throws()
        {
            // Arrange
            var wrapper = new CcxtWrapper(_exchangeMock.Object, _loggerMock.Object);

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
            var asset = "BTC";
            var expected = 1.2345;
            var balance = new Balances
            {
                total = new Dictionary<string, double> { { asset, expected } }
            };

            _exchangeMock.Setup(x => x.FetchBalance()).ReturnsAsync(balance);
            var wrapper = CreateWrapper();

            var result = await wrapper.GetBalanceAsync(asset);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetBalanceAsync_WithAssetNotFound_ReturnsZeroAndLogsWarning()
        {
            var balance = new Balances
            {
                total = new Dictionary<string, double>
                {
                    { "BTC", 1.0 }, { "ADA", 2.0 }
                }
            };

            _exchangeMock.Setup(x => x.FetchBalance()).ReturnsAsync(balance);
            var wrapper = CreateWrapper();

            var result = await wrapper.GetBalanceAsync("ETH");

            Assert.Equal(0.0, result);

            _loggerMock.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Actif 'ETH' non trouvé")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WithEmptyBalances_ReturnsZero()
        {
            var balance = new Balances { total = new Dictionary<string, double>() };
            _exchangeMock.Setup(x => x.FetchBalance()).ReturnsAsync(balance);

            var wrapper = CreateWrapper();
            var result = await wrapper.GetBalanceAsync("BTC");

            Assert.Equal(0.0, result);
        }

        [Theory]
        [InlineData("", "valid-secret")]
        [InlineData(null, "valid-secret")]
        [InlineData("valid-api-key", "")]
        [InlineData("valid-api-key", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        public async Task GetBalanceAsync_WithInvalidCredentials_Throws(string apiKey, string secret)
        {
            var wrapper = new CcxtWrapper(_exchangeMock.Object, _loggerMock.Object, apiKey, secret);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                wrapper.GetBalanceAsync("BTC"));

            Assert.Equal("Clés API requises pour accéder au solde.", ex.Message);
        }

        [Fact]
        public async Task GetBalanceAsync_WithNullBalances_Throws()
        {
            _exchangeMock.Setup(x => x.FetchBalance()).ReturnsAsync((Balances?)null);

            var wrapper = CreateWrapper();
            await Assert.ThrowsAsync<NullReferenceException>(() => wrapper.GetBalanceAsync("BTC"));
        }

        [Fact]
        public async Task GetBalanceAsync_WithZeroBalance_ReturnsZero()
        {
            var asset = "BTC";
            var balance = new Balances
            {
                total = new Dictionary<string, double> { { asset, 0.0 } }
            };

            _exchangeMock.Setup(x => x.FetchBalance()).ReturnsAsync(balance);
            var wrapper = CreateWrapper();

            var result = await wrapper.GetBalanceAsync(asset);
            Assert.Equal(0.0, result);
        }
    }
}
