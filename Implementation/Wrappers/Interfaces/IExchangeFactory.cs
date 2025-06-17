using Implementation.Wrappers.TestableCcxtWrapper;

namespace Implementation.Wrappers.Interfaces
{
    // Factory interface to create CCXT Exchange instances
    public interface IExchangeFactory
    {
        IExchangeOperationsWrapper Create(string exchangeId, string apiKey, string secret, bool useSandbox = false);
    }
}
