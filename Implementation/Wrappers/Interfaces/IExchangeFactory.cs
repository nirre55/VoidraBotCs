using ccxt;


namespace Implementation.Wrappers.Interfaces
{
    // Factory interface to create CCXT Exchange instances
    public interface IExchangeFactory
    {
        IExchangeWrapper Create(string exchangeId, string apiKey, string secret, bool useSandbox = false);
    }
}
