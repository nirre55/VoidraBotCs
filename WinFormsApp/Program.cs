using Implementation.Utils.Interfaces;
using Implementation.Utils;
using Implementation.Wrappers;
using Implementation.Wrappers.Interfaces;
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
            services.AddTransient<ICcxtWrapper>(sp =>
            {
                var factory = sp.GetRequiredService<IExchangeFactory>();

                // Configuration parameters (could be loaded from config file)
                const string exchangeId = "binanceusdm";
                const string apiKey = "71c0d61b99b727d02a7399fd9d05aefdeafd3f9c984a51fad08314518fe9ad6b";
                const string secretKey = "06b6bef900e4f9700039716b08d24ba8306d58c0055019856d9336ac8791d0c6";
                const bool sandbox = true;

                var exchangeOperations = factory.Create(exchangeId, apiKey, secretKey, sandbox);
                return new CcxtWrapper(exchangeOperations);
            });

            // Enregistrer le ConfigTabControl
            services.AddTransient<ConfigTabControl>();

            // Register the main form
            services.AddTransient<Form1>();
        }
    }
}