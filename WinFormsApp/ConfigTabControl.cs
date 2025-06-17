
using Implementation.Utils.Interfaces;

namespace WinFormsApp
{
    public partial class ConfigTabControl : UserControl
    {
        private readonly IApiKeyStorage _apiKeyStorage;
        public ConfigTabControl(IApiKeyStorage apiKeyStorage)
        {
            _apiKeyStorage = apiKeyStorage;
            InitializeComponent();
            LoadLabels();
            LoadPlatforms();
        }

        private void cmbPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlatform.SelectedItem == null)
            {
                EnableControls(false);
                return;
            }

            EnableControls(true);
            LoadSavedSettings();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedPlatform = cmbPlatform.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedPlatform))
                {
                    MessageBox.Show(
                        "Veuillez sélectionner une plateforme avant de sauvegarder.",
                        "Avertissement",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                _apiKeyStorage.SaveAll(
                    textBox1.Text,
                    textBox2.Text,
                    chkSandbox.Checked,
                    selectedPlatform
                );

                MessageBox.Show(
                    "Paramètres sauvegardés avec succès",
                    "Succès",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de la sauvegarde des paramètres : {ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void LoadSavedSettings()
        {
            var selectedPlatform = cmbPlatform.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedPlatform))
            {
                MessageBox.Show(
                    "Veuillez sélectionner une plateforme avant de sauvegarder.",
                    "Avertissement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            textBox1.Text = _apiKeyStorage.LoadApiKey(selectedPlatform) ?? string.Empty;
            textBox2.Text = _apiKeyStorage.LoadApiSecret(selectedPlatform) ?? string.Empty;
            chkSandbox.Checked = _apiKeyStorage.LoadSandMode(selectedPlatform);
        }

        private void EnableControls(bool enable)
        {
            textBox1.Enabled = enable;
            textBox2.Enabled = enable;
            chkSandbox.Enabled = enable;
            buttonSave.Enabled = enable;
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
