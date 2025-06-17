
using Implementation.Wrappers.Interfaces;

namespace WinFormsApp
{
    public partial class SimulationTabControl : UserControl
    {
        private ICcxtWrapper _ccxt;

        public SimulationTabControl(ICcxtWrapper ccxt)
        {
            _ccxt = ccxt ?? throw new ArgumentNullException(nameof(ccxt));
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
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
        }
    }
}