namespace WinFormsApp
{
    partial class ConfigTabControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelConfig = new TableLayoutPanel();
            lblApiKey = new Label();
            textBox1 = new TextBox();
            lblApiSecret = new Label();
            textBox2 = new TextBox();
            lblPlatform = new Label();
            cmbPlatform = new ComboBox();
            chkSandbox = new CheckBox();
            buttonSave = new Button();
            tableLayoutPanelConfig.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelConfig
            // 
            tableLayoutPanelConfig.AutoScroll = true;
            tableLayoutPanelConfig.AutoSize = true;
            tableLayoutPanelConfig.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelConfig.ColumnCount = 2;
            tableLayoutPanelConfig.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelConfig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelConfig.Controls.Add(lblApiKey, 0, 0);
            tableLayoutPanelConfig.Controls.Add(textBox1, 1, 0);
            tableLayoutPanelConfig.Controls.Add(lblApiSecret, 0, 1);
            tableLayoutPanelConfig.Controls.Add(textBox2, 1, 1);
            tableLayoutPanelConfig.Controls.Add(lblPlatform, 0, 2);
            tableLayoutPanelConfig.Controls.Add(cmbPlatform, 1, 2);
            tableLayoutPanelConfig.Controls.Add(chkSandbox, 0, 3);
            tableLayoutPanelConfig.Controls.Add(buttonSave, 0, 4);
            tableLayoutPanelConfig.Dock = DockStyle.Fill;
            tableLayoutPanelConfig.Location = new Point(0, 0);
            tableLayoutPanelConfig.Name = "tableLayoutPanelConfig";
            tableLayoutPanelConfig.RowCount = 5;
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelConfig.Size = new Size(150, 150);
            tableLayoutPanelConfig.TabIndex = 0;
            // 
            // lblApiKey
            // 
            lblApiKey.AutoSize = true;
            lblApiKey.Location = new Point(3, 6);
            lblApiKey.Margin = new Padding(3, 6, 3, 6);
            lblApiKey.Name = "lblApiKey";
            lblApiKey.Size = new Size(38, 15);
            lblApiKey.TabIndex = 0;
            lblApiKey.Text = "label1";
            lblApiKey.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(47, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // lblApiSecret
            // 
            lblApiSecret.AutoSize = true;
            lblApiSecret.Location = new Point(3, 35);
            lblApiSecret.Margin = new Padding(3, 6, 3, 6);
            lblApiSecret.Name = "lblApiSecret";
            lblApiSecret.Size = new Size(38, 15);
            lblApiSecret.TabIndex = 2;
            lblApiSecret.Text = "label2";
            lblApiSecret.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(47, 32);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 3;
            // 
            // lblPlatform
            // 
            lblPlatform.AutoSize = true;
            lblPlatform.Location = new Point(3, 64);
            lblPlatform.Margin = new Padding(3, 6, 3, 6);
            lblPlatform.Name = "lblPlatform";
            lblPlatform.Size = new Size(38, 15);
            lblPlatform.TabIndex = 4;
            lblPlatform.Text = "label3";
            lblPlatform.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbPlatform
            // 
            cmbPlatform.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Location = new Point(47, 61);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(100, 23);
            cmbPlatform.TabIndex = 5;
            // 
            // chkSandbox
            // 
            chkSandbox.AutoSize = true;
            tableLayoutPanelConfig.SetColumnSpan(chkSandbox, 2);
            chkSandbox.Location = new Point(3, 93);
            chkSandbox.Margin = new Padding(3, 6, 3, 6);
            chkSandbox.Name = "chkSandbox";
            chkSandbox.Size = new Size(82, 19);
            chkSandbox.TabIndex = 6;
            chkSandbox.Text = "checkBox1";
            chkSandbox.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            buttonSave.AutoSize = true;
            buttonSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelConfig.SetColumnSpan(buttonSave, 2);
            buttonSave.Location = new Point(3, 124);
            buttonSave.Margin = new Padding(3, 6, 3, 6);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(59, 20);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "button1";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // ConfigTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelConfig);
            Name = "ConfigTabControl";
            tableLayoutPanelConfig.ResumeLayout(false);
            tableLayoutPanelConfig.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelConfig;
        private Label lblApiKey;
        private TextBox textBox1;
        private Label lblApiSecret;
        private TextBox textBox2;
        private Label lblPlatform;
        private ComboBox cmbPlatform;
        private CheckBox chkSandbox;
        private Button buttonSave;
    }
}
