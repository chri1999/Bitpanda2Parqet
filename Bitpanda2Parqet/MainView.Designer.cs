namespace Bitpanda2Parqet
{
    partial class MainView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.btnCSVExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbBitpandaAPI = new System.Windows.Forms.TextBox();
            this.txbFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txbFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbParqetAcc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnParqetSynch = new System.Windows.Forms.Button();
            this.txbParqetToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveInitSettings = new System.Windows.Forms.Button();
            this.btnLoadInitSettings = new System.Windows.Forms.Button();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.tabTabs = new System.Windows.Forms.TabControl();
            this.tabPageSync = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.clbGenerellSettings = new System.Windows.Forms.CheckedListBox();
            this.cbxExportFormat = new System.Windows.Forms.ComboBox();
            this.dtpDataFromDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabTabs.SuspendLayout();
            this.tabPageSync.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCSVExport
            // 
            this.btnCSVExport.Location = new System.Drawing.Point(211, 225);
            this.btnCSVExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnCSVExport.Name = "btnCSVExport";
            this.btnCSVExport.Size = new System.Drawing.Size(110, 68);
            this.btnCSVExport.TabIndex = 2;
            this.btnCSVExport.Text = "CSV Export";
            this.btnCSVExport.UseVisualStyleBackColor = true;
            this.btnCSVExport.Click += new System.EventHandler(this.btnCSVExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bitpanda API:";
            // 
            // txbBitpandaAPI
            // 
            this.txbBitpandaAPI.Location = new System.Drawing.Point(60, 45);
            this.txbBitpandaAPI.Margin = new System.Windows.Forms.Padding(2);
            this.txbBitpandaAPI.Name = "txbBitpandaAPI";
            this.txbBitpandaAPI.Size = new System.Drawing.Size(536, 20);
            this.txbBitpandaAPI.TabIndex = 4;
            // 
            // txbFilePath
            // 
            this.txbFilePath.Location = new System.Drawing.Point(60, 184);
            this.txbFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txbFilePath.Name = "txbFilePath";
            this.txbFilePath.ReadOnly = true;
            this.txbFilePath.Size = new System.Drawing.Size(261, 20);
            this.txbFilePath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Speicherpfad:";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(61, 225);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(110, 68);
            this.btnSelectPath.TabIndex = 7;
            this.btnSelectPath.Text = "Speicherpfad auswählen";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txbFileName
            // 
            this.txbFileName.Location = new System.Drawing.Point(60, 113);
            this.txbFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txbFileName.Name = "txbFileName";
            this.txbFileName.Size = new System.Drawing.Size(261, 20);
            this.txbFileName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dateiname:";
            // 
            // txbParqetAcc
            // 
            this.txbParqetAcc.Location = new System.Drawing.Point(362, 113);
            this.txbParqetAcc.Margin = new System.Windows.Forms.Padding(2);
            this.txbParqetAcc.Name = "txbParqetAcc";
            this.txbParqetAcc.Size = new System.Drawing.Size(234, 20);
            this.txbParqetAcc.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(359, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Parqet Account Nummer";
            // 
            // btnParqetSynch
            // 
            this.btnParqetSynch.Location = new System.Drawing.Point(362, 225);
            this.btnParqetSynch.Margin = new System.Windows.Forms.Padding(2);
            this.btnParqetSynch.Name = "btnParqetSynch";
            this.btnParqetSynch.Size = new System.Drawing.Size(234, 68);
            this.btnParqetSynch.TabIndex = 12;
            this.btnParqetSynch.Text = "Parqet API Synchronisation";
            this.btnParqetSynch.UseVisualStyleBackColor = true;
            this.btnParqetSynch.Click += new System.EventHandler(this.btnParqetSync_Click);
            // 
            // txbParqetToken
            // 
            this.txbParqetToken.Location = new System.Drawing.Point(362, 184);
            this.txbParqetToken.Margin = new System.Windows.Forms.Padding(2);
            this.txbParqetToken.Name = "txbParqetToken";
            this.txbParqetToken.Size = new System.Drawing.Size(234, 20);
            this.txbParqetToken.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Parqet Accesstoken";
            // 
            // btnSaveInitSettings
            // 
            this.btnSaveInitSettings.Location = new System.Drawing.Point(61, 310);
            this.btnSaveInitSettings.Name = "btnSaveInitSettings";
            this.btnSaveInitSettings.Size = new System.Drawing.Size(535, 23);
            this.btnSaveInitSettings.TabIndex = 15;
            this.btnSaveInitSettings.Text = "Eingaben speichern";
            this.btnSaveInitSettings.UseVisualStyleBackColor = true;
            this.btnSaveInitSettings.Click += new System.EventHandler(this.btnSaveInitSettings_Click);
            // 
            // btnLoadInitSettings
            // 
            this.btnLoadInitSettings.Location = new System.Drawing.Point(61, 339);
            this.btnLoadInitSettings.Name = "btnLoadInitSettings";
            this.btnLoadInitSettings.Size = new System.Drawing.Size(535, 23);
            this.btnLoadInitSettings.TabIndex = 16;
            this.btnLoadInitSettings.Text = "gespeicherte Eingaben laden";
            this.btnLoadInitSettings.UseVisualStyleBackColor = true;
            this.btnLoadInitSettings.Click += new System.EventHandler(this.btnLoadInitSettings_Click);
            // 
            // pgbProgress
            // 
            this.pgbProgress.Location = new System.Drawing.Point(61, 368);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(535, 23);
            this.pgbProgress.TabIndex = 17;
            // 
            // tabTabs
            // 
            this.tabTabs.Controls.Add(this.tabPageSync);
            this.tabTabs.Controls.Add(this.tabPageSettings);
            this.tabTabs.Location = new System.Drawing.Point(0, 0);
            this.tabTabs.Name = "tabTabs";
            this.tabTabs.SelectedIndex = 0;
            this.tabTabs.Size = new System.Drawing.Size(657, 449);
            this.tabTabs.TabIndex = 18;
            // 
            // tabPageSync
            // 
            this.tabPageSync.Controls.Add(this.label4);
            this.tabPageSync.Controls.Add(this.pgbProgress);
            this.tabPageSync.Controls.Add(this.txbParqetAcc);
            this.tabPageSync.Controls.Add(this.txbBitpandaAPI);
            this.tabPageSync.Controls.Add(this.label5);
            this.tabPageSync.Controls.Add(this.label1);
            this.tabPageSync.Controls.Add(this.txbParqetToken);
            this.tabPageSync.Controls.Add(this.label3);
            this.tabPageSync.Controls.Add(this.btnParqetSynch);
            this.tabPageSync.Controls.Add(this.txbFileName);
            this.tabPageSync.Controls.Add(this.label2);
            this.tabPageSync.Controls.Add(this.btnLoadInitSettings);
            this.tabPageSync.Controls.Add(this.txbFilePath);
            this.tabPageSync.Controls.Add(this.btnSaveInitSettings);
            this.tabPageSync.Controls.Add(this.btnSelectPath);
            this.tabPageSync.Controls.Add(this.btnCSVExport);
            this.tabPageSync.Location = new System.Drawing.Point(4, 22);
            this.tabPageSync.Name = "tabPageSync";
            this.tabPageSync.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSync.Size = new System.Drawing.Size(649, 423);
            this.tabPageSync.TabIndex = 0;
            this.tabPageSync.Text = "Sync";
            this.tabPageSync.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.clbGenerellSettings);
            this.tabPageSettings.Controls.Add(this.cbxExportFormat);
            this.tabPageSettings.Controls.Add(this.dtpDataFromDate);
            this.tabPageSettings.Controls.Add(this.label8);
            this.tabPageSettings.Controls.Add(this.label7);
            this.tabPageSettings.Controls.Add(this.label6);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(649, 423);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // clbGenerellSettings
            // 
            this.clbGenerellSettings.FormattingEnabled = true;
            this.clbGenerellSettings.Items.AddRange(new object[] {
            "Staking ignorieren"});
            this.clbGenerellSettings.Location = new System.Drawing.Point(312, 74);
            this.clbGenerellSettings.Name = "clbGenerellSettings";
            this.clbGenerellSettings.Size = new System.Drawing.Size(297, 304);
            this.clbGenerellSettings.TabIndex = 6;
            // 
            // cbxExportFormat
            // 
            this.cbxExportFormat.FormattingEnabled = true;
            this.cbxExportFormat.Items.AddRange(new object[] {
            Bitpanda2Parqet.Enums.ExportFormat.Parqet,
            Bitpanda2Parqet.Enums.ExportFormat.PortfolioPerformance});
            this.cbxExportFormat.Location = new System.Drawing.Point(50, 66);
            this.cbxExportFormat.Name = "cbxExportFormat";
            this.cbxExportFormat.Size = new System.Drawing.Size(121, 21);
            this.cbxExportFormat.TabIndex = 5;
            // 
            // dtpDataFromDate
            // 
            this.dtpDataFromDate.Location = new System.Drawing.Point(50, 153);
            this.dtpDataFromDate.Name = "dtpDataFromDate";
            this.dtpDataFromDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDataFromDate.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(309, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "div. Einstellungen:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Daten laden ab:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Export Format:";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 447);
            this.Controls.Add(this.tabTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Bitpanda2Parqet";
            this.tabTabs.ResumeLayout(false);
            this.tabPageSync.ResumeLayout(false);
            this.tabPageSync.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCSVExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbBitpandaAPI;
        private System.Windows.Forms.TextBox txbFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txbFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbParqetAcc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnParqetSynch;
        private System.Windows.Forms.TextBox txbParqetToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveInitSettings;
        private System.Windows.Forms.Button btnLoadInitSettings;
        private System.Windows.Forms.ProgressBar pgbProgress;
        private System.Windows.Forms.TabControl tabTabs;
        private System.Windows.Forms.TabPage tabPageSync;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.ComboBox cbxExportFormat;
        private System.Windows.Forms.DateTimePicker dtpDataFromDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox clbGenerellSettings;
        private System.Windows.Forms.Label label8;
    }
}

