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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label1.Text = "Chargement�";

            try
            {
                double usdt = await _ccxt.GetBalanceAsync("USDT");
                label1.Text = $"{usdt:N4} USDT";
                _logger.LogInformation("Solde USDT r�cup�r� : {Balance}", usdt);
            }
            catch (Exception ex)
            {
                label1.Text = "Erreur";
                _logger.LogError(ex, "Erreur lors de la r�cup�ration du solde");
                MessageBox.Show(
                    $"Impossible de r�cup�rer le solde :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                button1.Enabled = true;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
