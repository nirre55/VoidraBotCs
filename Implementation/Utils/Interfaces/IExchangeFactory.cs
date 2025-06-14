using ccxt;
using Implementation.Wrappers.Interfaces;


namespace Implementation.Utils.Interfaces
{
    // Factory interface to create CCXT Exchange instances
    public interface IExchangeFactory
    {
        IExchangeWrapper Create(string exchangeId, string apiKey, string secret, bool useSandbox = false);
    }
}
