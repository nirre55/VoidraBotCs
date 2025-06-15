using Implementation.Wrappers.Interfaces;
using Microsoft.Extensions.Logging;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // Instanciation du wrapper en mode sandbox
        private readonly ICcxtWrapper _ccxt;
        private readonly ILogger<Form1> _logger;

        public Form1(ICcxtWrapper ccxt, ILogger<Form1> logger)
        {
            _ccxt = ccxt ?? throw new ArgumentNullException(nameof(ccxt));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InitializeComponent();

            InitializeTabs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeTabs()
        {
            // Récupérer le TabControl créé en Designer
            var tab = this.mainTabControl;

            // Instancier chaque UserControl
            var configCtrl = new ConfigTabControl { Dock = DockStyle.Fill };
            var simulationCtrl = new SimulationTabControl { Dock = DockStyle.Fill };

            // Ajouter aux TabPages (Name configuré dans Form1.Designer)
            tab.TabPages["tabPageConfig"].Controls.Add(configCtrl);
            tab.TabPages["tabPageSimulation"].Controls.Add(simulationCtrl);

        }

    }
}

//button1.Enabled = false;
//label1.Text = "Chargement…";

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