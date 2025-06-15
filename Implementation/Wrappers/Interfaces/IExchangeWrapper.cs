using ccxt;

namespace Implementation.Wrappers.Interfaces
{
    public interface IExchangeWrapper
    {
        Task<Balances> FetchBalanceWrapped();
        Task<Order> CreateOrderWrapped(string symbol, string type, string side, double amount, double? price = null);

    }
}
