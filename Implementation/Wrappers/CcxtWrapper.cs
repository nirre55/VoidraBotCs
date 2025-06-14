using Implementation.Wrappers.Interfaces;
using Microsoft.Extensions.Logging;

namespace Implementation.Wrappers
{
    public class CcxtWrapper : ICcxtWrapper
    {
        private readonly IExchangeWrapper _exchange;
        private readonly ILogger<CcxtWrapper> _logger;
        private readonly bool _isAuthenticated;

        public CcxtWrapper(
            IExchangeWrapper exchange,
            ILogger<CcxtWrapper> logger,
            string apiKey = null,
            string secret = null
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
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