using ccxt;
using Implementation.Wrappers.Interfaces;
using Implementation.Wrappers.TestableCcxtWrapper;

namespace Implementation.Wrappers
{
    public class CcxtWrapper : ICcxtWrapper
    {
        private readonly IExchangeOperationsWrapper _exchange;

        public CcxtWrapper(IExchangeOperationsWrapper exchange)
        {
            _exchange = exchange;
        }

        public async Task<double> GetBalanceAsync(string asset)
        {
            try
            {
                var balances = await _exchange.FetchBalance();

                if (balances.total.TryGetValue(asset, out var total))
                    return total;

                return 0.0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> CreateOrderAsync(string symbol, string type, string side, double amount, double price)
        {
            try
            {
                var raw = await _exchange.CreateOrder(symbol, type, side, amount, price);
                return raw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}