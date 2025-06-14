using ccxt;


namespace Implementation.Utils
{
    // Factory interface to create CCXT Exchange instances
    public interface IExchangeFactory
    {
        Exchange Create(
            string exchangeId,
            string apiKey,
            string secret,
            bool useSandbox = false
        );
    }
}
