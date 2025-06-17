
using Moq;
using ccxt;
using Implementation.Wrappers.TestableCcxtWrapper;
using Implementation.Wrappers;
using AutoFixture;

namespace Implementation.Tests.Wrappers
{
    public class CcxtWrapperTests
    {
        private readonly Mock<IExchangeOperationsWrapper> _mockExchangeOperations;
        private readonly CcxtWrapper _ccxtWrapper;
        private readonly Fixture _fixure;

        public CcxtWrapperTests()
        {
            _mockExchangeOperations = new Mock<IExchangeOperationsWrapper>();
            _ccxtWrapper = new CcxtWrapper(_mockExchangeOperations.Object);
            _fixure = new Fixture();
        }

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidExchangeOperations_ShouldCreateInstance()
        {
            // Assert
            Assert.NotNull(_ccxtWrapper);
        }

        #endregion

        #region GetBalanceAsync Tests

        [Fact]
        public async Task GetBalanceAsync_WithExistingAsset_ShouldReturnBalance()
        {
            // Arrange
            const string asset = "BTC";
            const double expectedBalance = 1.5;
            var balances = FakeBlanace(asset, expectedBalance);

            _mockExchangeOperations.Setup(ex => ex.FetchBalance()).ReturnsAsync(balances);
            // Act
            var result = await _ccxtWrapper.GetBalanceAsync(asset);

            // Assert
            Assert.Equal(expectedBalance, result);
            _mockExchangeOperations.Verify(x => x.FetchBalance(), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WithNonExistingAsset_ShouldReturnZero()
        {
            // Arrange
            const string asset = "ETH";
            const double expectedBalance = 1.5;
            var balances = FakeBlanace(asset, expectedBalance);
            const string nonExistingAsset = "NonExistingAsset";


            _mockExchangeOperations.Setup(ex => ex.FetchBalance()).ReturnsAsync(balances);

            // Act
            var result = await _ccxtWrapper.GetBalanceAsync(nonExistingAsset);

            // Assert
            Assert.Equal(0.0, result);
            _mockExchangeOperations.Verify(x => x.FetchBalance(), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WithEmptyBalances_ShouldReturnZero()
        {
            // Arrange
            const string asset = "BTC";
            var balances = new Balances();
            balances.total = new Dictionary<string, double>(); // Empty balances


            _mockExchangeOperations.Setup(x => x.FetchBalance())
                                 .ReturnsAsync(balances);

            // Act
            var result = await _ccxtWrapper.GetBalanceAsync(asset);

            // Assert
            Assert.Equal(0.0, result);
            _mockExchangeOperations.Verify(x => x.FetchBalance(), Times.Once);
        }

        [Fact]
        public async Task GetBalanceAsync_WhenExchangeThrowsException_ShouldRethrowException()
        {
            // Arrange
            const string asset = "BTC";
            var expectedException = new InvalidOperationException("Exchange error");

            _mockExchangeOperations.Setup(x => x.FetchBalance())
                                 .ThrowsAsync(expectedException);

            // Act & Assert
            var thrownException = await Assert.ThrowsAsync<InvalidOperationException>(() => _ccxtWrapper.GetBalanceAsync(asset));
            Assert.Equal(expectedException.Message, thrownException.Message);
            _mockExchangeOperations.Verify(x => x.FetchBalance(), Times.Once);
        }

    

        [Fact]
        public async Task GetBalanceAsync_WithCaseInsensitiveAsset_ShouldReturnZeroIfNotExactMatch()
        {
            // Arrange
            const string asset = "btc"; // lowercase
            var balances = FakeBlanace("BTC", 1.5);// uppercase

            _mockExchangeOperations.Setup(x => x.FetchBalance())
                                 .ReturnsAsync(balances);

            // Act
            var result = await _ccxtWrapper.GetBalanceAsync(asset);

            // Assert
            Assert.Equal(0.0, result); // Should return 0 because dictionary is case-sensitive
        }

        #endregion

        #region CreateOrderAsync Tests

        [Fact]
        public async Task CreateOrderAsync_WithValidParameters_ShouldReturnOrder()
        {
            // Arrange
            const string symbol = "BTC/USDT";
            const string type = "limit";
            const string side = "buy";
            const double amount = 0.01;
            const double price = 50000.0;

            var ordre = FakeOrder(symbol, type, side, amount, price);

            _mockExchangeOperations.Setup(x => x.CreateOrder(symbol, type, side, amount, price))
                                 .ReturnsAsync(ordre);

            // Act
            var result = await _ccxtWrapper.CreateOrderAsync(symbol, type, side, amount, price);

            // Assert
            Assert.NotNull(result);
            _mockExchangeOperations.Verify(x => x.CreateOrder(symbol, type, side, amount, price), Times.Once);
        }


        [Fact]
        public async Task CreateOrderAsync_WhenExchangeThrowsException_ShouldRethrowException()
        {
            // Arrange
            const string symbol = "BTC/USDT";
            const string type = "limit";
            const string side = "buy";
            const double amount = 0.01;
            const double price = 50000.0;

            var expectedException = new InvalidOperationException("Order creation failed");

            _mockExchangeOperations.Setup(x => x.CreateOrder(symbol, type, side, amount, price))
                                 .ThrowsAsync(expectedException);

            // Act & Assert
            var thrownException = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _ccxtWrapper.CreateOrderAsync(symbol, type, side, amount, price));

            Assert.Equal(expectedException.Message, thrownException.Message);
            _mockExchangeOperations.Verify(x => x.CreateOrder(symbol, type, side, amount, price), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_WithZeroAmount_ShouldPassToExchange()
        {
            // Arrange
            const string symbol = "BTC/USDT";
            const string type = "limit";
            const string side = "buy";
            const double amount = 0.0;
            const double price = 50000.0;

            var ordre = FakeOrder(symbol, type, side, amount, price);

            _mockExchangeOperations.Setup(x => x.CreateOrder(symbol, type, side, amount, price))
                                 .ReturnsAsync(ordre);

            // Act
            var result = await _ccxtWrapper.CreateOrderAsync(symbol, type, side, amount, price);

            // Assert
            Assert.NotNull(result);
            _mockExchangeOperations.Verify(x => x.CreateOrder(symbol, type, side, amount, price), Times.Once);
        }

        [Theory]
        [InlineData("", "limit", "buy", 0.01, 50000.0)]
        [InlineData("BTC/USDT", "", "buy", 0.01, 50000.0)]
        [InlineData("BTC/USDT", "limit", "", 0.01, 50000.0)]
        public async Task CreateOrderAsync_WithEmptyStringParameters_ShouldPassToExchange(
            string symbol, string type, string side, double amount, double price)
        {
            // Arrange
            var ordre = FakeOrder(symbol, type, side, amount, price);

            _mockExchangeOperations.Setup(x => x.CreateOrder(symbol, type, side, amount, price))
                                 .ReturnsAsync(ordre);

            // Act
            var result = await _ccxtWrapper.CreateOrderAsync(symbol, type, side, amount, price);

            // Assert
            Assert.NotNull(result);
            _mockExchangeOperations.Verify(x => x.CreateOrder(symbol, type, side, amount, price), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_WithNegativeAmount_ShouldPassToExchange()
        {
            // Arrange
            const string symbol = "BTC/USDT";
            const string type = "limit";
            const string side = "sell";
            const double amount = -0.01;
            const double price = 50000.0;

            var ordre = FakeOrder(symbol, type, side, amount, price);

            _mockExchangeOperations.Setup(x => x.CreateOrder(symbol, type, side, amount, price))
                                 .ReturnsAsync(ordre);

            // Act
            var result = await _ccxtWrapper.CreateOrderAsync(symbol, type, side, amount, price);

            // Assert
            Assert.NotNull(result);
            _mockExchangeOperations.Verify(x => x.CreateOrder(symbol, type, side, amount, price), Times.Once);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public async Task CcxtWrapper_SequentialOperations_ShouldWorkCorrectly()
        {
            // Arrange
            const string asset = "BTC";
            const double balance = 1.0;
            var balances = new Balances();
            balances.total = new Dictionary<string, double> { { asset, balance } };

            var rawOrder = new Order()
            {
                id = "order123",
                symbol = "BTC/USDT",
                type = "market",
                side = "buy",
                amount = 0.01,
                price = 100.0
            };

            _mockExchangeOperations.Setup(x => x.FetchBalance()).ReturnsAsync(balances);
            _mockExchangeOperations.Setup(x => x.CreateOrder(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double?>()))
                .ReturnsAsync(rawOrder);

            // Act
            var balanceResult = await _ccxtWrapper.GetBalanceAsync(asset);
            var orderResult = await _ccxtWrapper.CreateOrderAsync("BTC/USDT", "market", "buy", 0.01, 100);

            // Assert
            Assert.Equal(balance, balanceResult);
            Assert.NotNull(orderResult);
            _mockExchangeOperations.Verify(x => x.FetchBalance(), Times.Once);
            _mockExchangeOperations.Verify(x => x.CreateOrder("BTC/USDT", "market", "buy", 0.01, 100), Times.Once);
        }

        [Fact]
        public async Task CcxtWrapper_ConcurrentOperations_ShouldWorkCorrectly()
        {
            // Arrange
            const string asset = "BTC";
            const double balance = 1.0;
            var balances = new Balances();
            balances.total = new Dictionary<string, double> { { asset, balance } };

            var rawOrder = new Order()
            {
                id = "order123",
                symbol = "BTC/USDT",
                type = "market",
                side = "buy",
                amount = 0.01,
                price = 100.0
            };

            _mockExchangeOperations.Setup(x => x.FetchBalance()).ReturnsAsync(balances);
            _mockExchangeOperations.Setup(x => x.CreateOrder(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double?>()))
                .ReturnsAsync(rawOrder);

            // Act
            var balanceTask = _ccxtWrapper.GetBalanceAsync(asset);
            var orderTask = _ccxtWrapper.CreateOrderAsync("BTC/USDT", "market", "buy", 0.01, 100);

            await Task.WhenAll(balanceTask, orderTask);

            // Assert
            Assert.Equal(balance, balanceTask.Result);
            Assert.NotNull(orderTask.Result);
        }

        #endregion

        #region ExchangeOperationsWrapper Tests

        [Fact]
        public void ExchangeOperationsWrapper_Constructor_WithNullExchange_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ExchangeOperationsWrapper(null));
            Assert.Equal("exchange", exception.ParamName);
        }

        // Note: Pour tester ExchangeOperationsWrapper avec un vrai Exchange, 
        // vous auriez besoin d'un exchange réel ou d'une instance de test
        // Ces tests seraient plutôt des tests d'intégration

        #endregion

        private Balances FakeBlanace(string asset, double expectedBalance) {
            var balances = new Balances();
            balances.total = new Dictionary<string, double>
            {
                { asset, expectedBalance }
            };
            return balances; 
        }

        private Order FakeOrder(string symbol, string type, string side, double amount, double price)
        {
            var order = new Order();
            order.id = "order123"; // Example order ID
            order.symbol = symbol;
            order.type = type;
            order.side = side;
            order.amount = amount;
            order.price = price;

            return order;
        }
    }
}