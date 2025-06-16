namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainTabControl = new TabControl();
            tabPageConfig = new TabPage();
            tabPageSimulation = new TabPage();
            mainTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(tabPageConfig);
            mainTabControl.Controls.Add(tabPageSimulation);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Margin = new Padding(4);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(1029, 630);
            mainTabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            tabPageConfig.Location = new Point(4, 30);
            tabPageConfig.Margin = new Padding(4);
            tabPageConfig.Name = "tabPageConfig";
            tabPageConfig.Padding = new Padding(4);
            tabPageConfig.Size = new Size(1021, 596);
            tabPageConfig.TabIndex = 0;
            tabPageConfig.Text = "Config";
            tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageSimulation
            // 
            tabPageSimulation.Location = new Point(4, 24);
            tabPageSimulation.Margin = new Padding(4);
            tabPageSimulation.Name = "tabPageSimulation";
            tabPageSimulation.Padding = new Padding(4);
            tabPageSimulation.Size = new Size(1021, 602);
            tabPageSimulation.TabIndex = 1;
            tabPageSimulation.Text = "Simulation";
            tabPageSimulation.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 630);
            Controls.Add(mainTabControl);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            mainTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabPage tabPageConfig;
        private TabPage tabPageSimulation;
        public TabControl mainTabControl;
    }
}
