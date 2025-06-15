using ccxt;
using Implementation.Wrappers.Interfaces;

namespace Implementation.Wrappers
{
    public class ExchangeAdapter : IExchangeWrapper
    {
        private readonly Exchange _exchange;

        public ExchangeAdapter(Exchange exchange)
        {
            _exchange = exchange;
        }

        public Task<Balances> FetchBalanceWrapped()
            => _exchange.FetchBalance();

        public async Task<Order> CreateOrderWrapped(string symbol, string type, string side, double amount, double? price = null)
        {
            var raw = await _exchange.CreateOrder(symbol, type, side, amount, price);
            return new Order(raw);
        }
    }
}
