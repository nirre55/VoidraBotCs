using Implementation.Wrappers.Interfaces;
using Implementation.Utils.Interfaces;
using Implementation.Wrappers;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IExchangeFactory _exchangeFactory;
        private readonly IApiKeyStorage _apiKeyStorage;
        private ICcxtWrapper? _ccxt;

        public Form1(IExchangeFactory exchangeFactory, IApiKeyStorage apiKeyStorage, ICcxtWrapper? ccxtWrapper)
        {
            _exchangeFactory = exchangeFactory ?? throw new ArgumentNullException(nameof(exchangeFactory));
            _apiKeyStorage = apiKeyStorage ?? throw new ArgumentNullException(nameof(apiKeyStorage));
            _ccxt = ccxtWrapper; // Assign the ccxtWrapper which might be null
            InitializeComponent();
            InitializeTabs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void InitializeTabs()
        {
            var tab = this.mainTabControl;
            var configCtrl = new ConfigTabControl(_apiKeyStorage) { Dock = DockStyle.Fill };
            configCtrl.PlatformChanged += OnPlatformChanged;

            // Créer le SimulationTabControl avec l'instance de ICcxtWrapper
            var simulationCtrl = new SimulationTabControl(_ccxt) { Dock = DockStyle.Fill };

            tab.TabPages["tabPageConfig"].Controls.Add(configCtrl);
            tab.TabPages["tabPageSimulation"].Controls.Add(simulationCtrl);
        }

        private void OnPlatformChanged(object sender, string platform)
        {
            try
            {
                var apiKey = _apiKeyStorage.LoadApiKey(platform);
                var secretKey = _apiKeyStorage.LoadApiSecret(platform);
                var sandbox = _apiKeyStorage.LoadSandMode(platform);

                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(secretKey))
                {
                    MessageBox.Show(
                        "Veuillez configurer les clés API pour cette plateforme.",
                        "Configuration requise",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var exchange = _exchangeFactory.Create(platform, apiKey, secretKey, sandbox);
                _ccxt = new CcxtWrapper(exchange);

                // Mettre à jour le SimulationTabControl avec la nouvelle instance
                var simulationCtrl = mainTabControl.TabPages["tabPageSimulation"].Controls.OfType<SimulationTabControl>().FirstOrDefault();
                if (simulationCtrl != null)
                {
                    simulationCtrl.UpdateCcxtWrapper(_ccxt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors du changement de plateforme : {ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

