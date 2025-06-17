using ccxt;

namespace Implementation.Wrappers.TestableCcxtWrapper
{
    public class ExchangeOperationsWrapper : IExchangeOperationsWrapper
    {
        private readonly Exchange _exchange;

        public ExchangeOperationsWrapper(Exchange exchange)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
        }

        public async Task<Balances> FetchBalance()
        {
            return await _exchange.FetchBalance();
        }

        public async Task<Order> CreateOrder(string symbol, string type, string side, double amount, double? price = null)
        {
            return await _exchange.CreateOrder(symbol, type, side, amount, price);
        }
    }
}
