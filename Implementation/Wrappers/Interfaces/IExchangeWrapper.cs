using ccxt;

namespace Implementation.Wrappers.Interfaces
{
    public interface IExchangeWrapper
    {
        Task<Balances> FetchBalance();
    }
}
