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

        public Task<Balances> FetchBalance()
        {
            return _exchange.FetchBalance();
        }
    }
}
