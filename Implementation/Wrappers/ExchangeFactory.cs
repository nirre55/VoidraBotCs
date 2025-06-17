using ccxt;
using Implementation.Wrappers.Interfaces;
using Implementation.Wrappers.TestableCcxtWrapper;
using System.Reflection;

namespace Implementation.Wrappers
{
    public class ExchangeFactory : IExchangeFactory
    {
        private static readonly Dictionary<string, Type> _exchangeTypeCache = new();

        public ExchangeFactory()
        {
        }

        public IExchangeOperationsWrapper Create(string exchangeId, string apiKey, string secret, bool useSandbox)
        {
            if (string.IsNullOrWhiteSpace(exchangeId))
                throw new ArgumentException("exchangeId ne peut pas être vide.", nameof(exchangeId));

            var exchangeType = ResolveExchangeType(exchangeId);
            var config = BuildConfig(apiKey, secret);
            var exchangeInstance = InstantiateExchange(exchangeType, config);

            if (useSandbox)
            {
                exchangeInstance.setSandboxMode(true);
            }

            return new ExchangeOperationsWrapper(exchangeInstance);
        }

        private Type ResolveExchangeType(string exchangeId)
        {
            var key = exchangeId.ToLowerInvariant();
            if (_exchangeTypeCache.TryGetValue(key, out var cached))
            {
                return cached;
            }

            var assembly = Assembly.GetAssembly(typeof(Exchange));
            var possibleNames = new[]
            {
                key,
                char.ToUpperInvariant(key[0]) + key.Substring(1),
                exchangeId.ToUpperInvariant(),
                exchangeId
            };

            foreach (var name in possibleNames)
            {
                var type = assembly.GetTypes()
                    .FirstOrDefault(t =>
                        typeof(Exchange).IsAssignableFrom(t) && !t.IsAbstract &&
                        t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                    );

                if (type != null)
                {
                    _exchangeTypeCache[key] = type;
                    return type;
                }
            }

            var available = assembly.GetTypes()
                .Where(t => typeof(Exchange).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => t.Name)
                .Distinct()
                .OrderBy(n => n);

            throw new ArgumentException(
                $"L'échange '{exchangeId}' n'est pas supporté par CCXT.\n" +
                $"Échanges disponibles : {string.Join(", ", available)}"
            );
        }

        private Dictionary<string, object> BuildConfig(string apiKey, string secret)
        {
            var config = new Dictionary<string, object>
            {
                ["enableRateLimit"] = true
            };

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(secret))
            {
                config["apiKey"] = apiKey;
                config["secret"] = secret;
            }

            return config;
        }

        private Exchange InstantiateExchange(Type exchangeType, Dictionary<string, object> config)
        {
            var instance = Activator.CreateInstance(exchangeType, config);
            if (instance is not Exchange exchange)
            {
                throw new InvalidOperationException($"Impossible de créer une instance du type {exchangeType.FullName}.");
            }

            return exchange;
        }
    }
}
