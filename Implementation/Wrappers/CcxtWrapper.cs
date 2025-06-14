using System.Reflection;
using ccxt;
using Implementation.Utils;
using Microsoft.Extensions.Logging;

namespace Implementation.Wrappers
{
    public class CcxtWrapper : ICcxtWrapper
    {
        private readonly Exchange _exchange;
        private readonly ILogger<CcxtWrapper> _logger;
        private readonly bool _isAuthenticated;

        public CcxtWrapper(
            IExchangeFactory factory,
            ILogger<CcxtWrapper> logger,
            string exchangeId,
            string apiKey = null,
            string secret = null,
            bool useSandbox = false
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _exchange = factory?.Create(exchangeId, apiKey, secret, useSandbox)
                        ?? throw new ArgumentNullException(nameof(factory));
            _isAuthenticated = !string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(secret);
        }

        public async Task<double> GetBalanceAsync(string asset)
        {
            if (!_isAuthenticated)
            {
                _logger.LogError("Clés API requises pour accéder au solde.");
                throw new InvalidOperationException("Clés API requises pour accéder au solde.");
            }

            var balances = await _exchange.FetchBalance();

            if (balances.total.TryGetValue(asset, out var total))
                return total;

            _logger.LogWarning("Actif '{Asset}' non trouvé dans les soldes.", asset);
            return 0.0;
        }
    }
}