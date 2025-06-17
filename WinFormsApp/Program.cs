using Implementation.Utils.Interfaces;
using Implementation.Utils;
using Implementation.Wrappers;
using Implementation.Wrappers.Interfaces;
using Implementation.Wrappers.TestableCcxtWrapper;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Créer le service provider
            var serviceProvider = CreateServiceProvider();

            // Récupérer Form1 depuis le container DI
            var form1 = serviceProvider.GetRequiredService<Form1>();

            Application.Run(form1);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureDependencies(services);
            return services.BuildServiceProvider();
        }

        private static void ConfigureDependencies(IServiceCollection services)
        {
            // Register factory and wrapper
            services.AddSingleton<IExchangeFactory, ExchangeFactory>();
            services.AddSingleton<IApiKeyStorage, ApiKeyStorage>();

            // Supprimer l'enregistrement de IExchangeOperationsWrapper car il sera créé dynamiquement

            // Modifier l'enregistrement de ICcxtWrapper pour qu'il soit créé à la demande
            services.AddTransient<ICcxtWrapper>(sp =>
            {
                var factory = sp.GetRequiredService<IExchangeFactory>();
                var apiKeyStorage = sp.GetRequiredService<IApiKeyStorage>();

                // Utiliser la plateforme par défaut
                const string defaultExchangeId = "binanceusdm";

                // Récupérer les informations depuis le stockage
                var apiKey = apiKeyStorage.LoadApiKey(defaultExchangeId);
                var secretKey = apiKeyStorage.LoadApiSecret(defaultExchangeId);
                var sandbox = apiKeyStorage.LoadSandMode(defaultExchangeId);

                // Vérifier si les clés sont configurées
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(secretKey))
                {
                    // Retourner une instance avec des valeurs par défaut ou lancer une exception
                    throw new InvalidOperationException("Les clés API n'ont pas été configurées. Veuillez les configurer dans l'onglet Configuration.");
                }

                var exchange = factory.Create(defaultExchangeId, apiKey, secretKey, sandbox);
                return new CcxtWrapper(exchange);
            });

            // Enregistrer le ConfigTabControl
            services.AddTransient<ConfigTabControl>();

            // Register the main form
            services.AddTransient<Form1>();
        }
    }
}