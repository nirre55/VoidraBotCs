using Implementation.Wrappers.Interfaces;
using Implementation.Utils.Interfaces;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly ICcxtWrapper _ccxt;
        private readonly IApiKeyStorage _apiKeyStorage;

        public Form1(ICcxtWrapper ccxt, IApiKeyStorage apiKeyStorage)
        {
            _ccxt = ccxt ?? throw new ArgumentNullException(nameof(ccxt));
            _apiKeyStorage = apiKeyStorage ?? throw new ArgumentNullException(nameof(apiKeyStorage));
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
            var simulationCtrl = new SimulationTabControl { Dock = DockStyle.Fill };
            tab.TabPages["tabPageConfig"].Controls.Add(configCtrl);
            tab.TabPages["tabPageSimulation"].Controls.Add(simulationCtrl);
        }
    }
}

//button1.Enabled = false;
//label1.Text = "Chargementé";

//try
//{
//    double usdt = await _ccxt.GetBalanceAsync("USDT");
//    label1.Text = $"{usdt:N4} USDT";
//    _logger.LogInformation("Solde USDT récupéré : {Balance}", usdt);
//}
//catch (Exception ex)
//{
//    label1.Text = "Erreur";
//    _logger.LogError(ex, "Erreur lors de la récupération du solde");
//    MessageBox.Show(
//        $"Impossible de récupérer le solde :\n{ex.Message}",
//        "Erreur",
//        MessageBoxButtons.OK,
//        MessageBoxIcon.Error
//    );
//}
//finally
//{
//    button1.Enabled = true;
//}