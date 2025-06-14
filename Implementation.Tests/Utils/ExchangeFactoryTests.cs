//using AutoFixture;
//using AutoFixture.Xunit2;
//using ccxt;
//using Implementation.Utils;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Net.Sockets;


//namespace Implementation.Tests.Utils
//{
//    public class ExchangeFactoryTests
//    {
//        private readonly Fixture _fixture;
//        private readonly Mock<ILogger<ExchangeFactory>> _loggerMock;
//        private readonly ExchangeFactory _factory;

//        public ExchangeFactoryTests()
//        {
//            _fixture = new Fixture();
//            _loggerMock = new Mock<ILogger<ExchangeFactory>>();
//            _factory = new ExchangeFactory(_loggerMock.Object);
//            ClearExchangeTypeCache();
//        }

//        [Fact]
//        public void Create_WithValidExchangeId_ReturnsNotNull()
//        {
//            // Arrange
//            var exchangeId = "binance";

//            // Act
//            var exchange = _factory.Create(exchangeId);

//            // Assert
//            Assert.NotNull(exchange);
//        }

//        [Fact]
//        public void Create_WithApiKeys_SetsApiKeys()
//        {
//            // Arrange
//            var exchangeId = "binance";
//            var apiKey = _fixture.Create<string>();
//            var secretApiKey = _fixture.Create<string>();

//            // Act
//            var exchange = _factory.Create(exchangeId, apiKey: apiKey, secret: secretApiKey);

//            // Assert
//            Assert.Equal(apiKey, exchange.apiKey);
//            Assert.Equal(secretApiKey, exchange.secret);
//        }

//        [Fact]
//        public void Create_WithSandbox_SetsSandboxModeTrue()
//        {
//            // Arrange
//            var exchangeId = "binance";

//            // Act
//            var exchange = _factory.Create(exchangeId, useSandbox: true);

//            // Assert
//            Assert.True(exchange.isSandboxModeEnabled);
//        }

//        [Fact]
//        public void Create_WithNullExchangeId_ThrowsArgumentException()
//        {
//            // Act & Assert
//            var ex = Assert.Throws<ArgumentException>(() =>
//                _factory.Create(null)
//            );

//            Assert.Equal("exchangeId ne peut pas être vide. (Parameter 'exchangeId')", ex.Message);
//        }

//        [Fact]
//        public void Create_WithEmptyExchangeId_ThrowsArgumentException()
//        {
//            // Act & Assert
//            Assert.Throws<ArgumentException>(() => _factory.Create(""));
//        }

//        [Fact]
//        public void Create_WithWhitespaceExchangeId_ThrowsArgumentException()
//        {
//            // Act & Assert
//            Assert.Throws<ArgumentException>(() => _factory.Create("   "));
//        }

//        [Fact]
//        public void Create_WithUnknownExchangeId_Throws()
//        {
//            // Arrange
//            var invalidExchangeId = "doesNotExist";

//            // Act & Assert
//            var ex = Assert.Throws<ArgumentException>(() =>
//                _factory.Create(invalidExchangeId)
//            );

//            Assert.Contains("n'est pas supporté par CCXT", ex.Message);
//        }

//        [Fact]
//        public void Create_CachesExchangeType_AfterFirstCall()
//        {
//            // Arrange
//            var exchangeId = "binance";

//            // Act
//            _factory.Create(exchangeId); // 1re appel : découverte
//            _factory.Create(exchangeId); // 2e appel : cache

//            // Assert
//            _loggerMock.Verify(log =>
//                log.Log(
//                    LogLevel.Debug,
//                    It.IsAny<EventId>(),
//                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("retrieved from cache")),
//                    null,
//                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
//                Times.Once);
//        }

//        // Réflexion pour vider le cache statique
//        private void ClearExchangeTypeCache()
//        {
//            var cacheField = typeof(ExchangeFactory).GetField("_exchangeTypeCache", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
//            if (cacheField?.GetValue(null) is Dictionary<string, Type> cache)
//            {
//                cache.Clear();
//            }
//        }
//    }
//}