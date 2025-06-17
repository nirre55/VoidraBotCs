
using Implementation.Wrappers.Interfaces;

namespace WinFormsApp
{
    public partial class SimulationTabControl : UserControl
    {
        private ICcxtWrapper? _ccxt; // Allow null

        public SimulationTabControl(ICcxtWrapper? ccxt) // Allow null
        {
            _ccxt = ccxt; // Assign directly, can be null
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_ccxt == null)
            {
                label1.Text = "Veuillez configurer une plateforme.";
                button1.Enabled = false;
                return;
            }

            button1.Enabled = false;
            label1.Text = "Chargement...";

            try
            {
                double usdt = await _ccxt.GetBalanceAsync("USDT");
                label1.Text = $"{usdt:N4} USDT";
            }
            catch (Exception ex)
            {
                label1.Text = "Erreur";
                MessageBox.Show(
                    $"Impossible de récupérer le solde :\n{ex.Message}",
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

        public void UpdateCcxtWrapper(ICcxtWrapper newCcxt)
        {
            _ccxt = newCcxt ?? throw new ArgumentNullException(nameof(newCcxt));
            // Reset UI elements as a valid wrapper is provided
            label1.Text = "Click to load balance"; // Or simply ""
            button1.Enabled = true;
        }
    }
}