using ccxt;

namespace Implementation.Wrappers.TestableCcxtWrapper
{
    public interface IExchangeOperationsWrapper
    {
        Task<Balances> FetchBalance();
        Task<Order> CreateOrder(string symbol, string type, string side, double amount, double? price = null);
    }
}
