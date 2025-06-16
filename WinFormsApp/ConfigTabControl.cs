
namespace WinFormsApp
{
    public partial class ConfigTabControl : UserControl
    {

        public ConfigTabControl()
        {
            InitializeComponent();
            LoadLabels();
        }

        private void LoadLabels()
        {
            lblApiKey.Text = UiStrings.Label_ApiKey;
            lblApiSecret.Text = UiStrings.Label_ApiSecret;
            lblPlatform.Text = UiStrings.Label_Platform;
            chkSandbox.Text = UiStrings.Label_Sandbox;
        }
    }
}
