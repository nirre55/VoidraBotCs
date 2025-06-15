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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            checkBox1 = new CheckBox();
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
            tableLayoutPanelConfig.Controls.Add(label1, 0, 0);
            tableLayoutPanelConfig.Controls.Add(textBox1, 1, 0);
            tableLayoutPanelConfig.Controls.Add(label2, 0, 1);
            tableLayoutPanelConfig.Controls.Add(textBox2, 1, 1);
            tableLayoutPanelConfig.Controls.Add(label3, 0, 2);
            tableLayoutPanelConfig.Controls.Add(comboBox1, 1, 2);
            tableLayoutPanelConfig.Controls.Add(checkBox1, 1, 3);
            tableLayoutPanelConfig.Dock = DockStyle.Fill;
            tableLayoutPanelConfig.Location = new Point(0, 0);
            tableLayoutPanelConfig.Name = "tableLayoutPanelConfig";
            tableLayoutPanelConfig.RowCount = 4;
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle());
            tableLayoutPanelConfig.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelConfig.Size = new Size(150, 150);
            tableLayoutPanelConfig.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Margin = new Padding(3, 6, 3, 6);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(47, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 35);
            label2.Margin = new Padding(3, 6, 3, 6);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 2;
            label2.Text = "label2";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(47, 32);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 64);
            label3.Margin = new Padding(3, 6, 3, 6);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 4;
            label3.Text = "label3";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(47, 61);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(100, 23);
            comboBox1.TabIndex = 5;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            tableLayoutPanelConfig.SetColumnSpan(checkBox1, 2);
            checkBox1.Location = new Point(3, 93);
            checkBox1.Margin = new Padding(3, 6, 3, 6);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
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
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private ComboBox comboBox1;
        private CheckBox checkBox1;
    }
}
