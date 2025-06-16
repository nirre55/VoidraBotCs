
namespace WinFormsApp
{
    public partial class ConfigTabControl : UserControl
    {

        public ConfigTabControl()
        {
            InitializeComponent();
            LoadLabels();
            LoadPlatforms();
        }

        private void LoadLabels()
        {
            lblApiKey.Text = UiStrings.Label_ApiKey;
            lblApiSecret.Text = UiStrings.Label_ApiSecret;
            lblPlatform.Text = UiStrings.Label_Platform;
            chkSandbox.Text = UiStrings.Label_Sandbox;
            buttonSave.Text = UiStrings.Button_Save;
        }

        private void LoadPlatforms()
        {
            cmbPlatform.Items.Clear();
            cmbPlatform.Items.Add("binanceusdm");
            // Ajouter d'autres plateformes si nécessaire
            cmbPlatform.SelectedIndex = 0; // Sélectionner la première plateforme par défaut
        }
    }
}
