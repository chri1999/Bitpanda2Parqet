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
            this.btnParqetExport = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // btnParqetExport
            // 
            this.btnParqetExport.Location = new System.Drawing.Point(203, 229);
            this.btnParqetExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnParqetExport.Name = "btnParqetExport";
            this.btnParqetExport.Size = new System.Drawing.Size(110, 68);
            this.btnParqetExport.TabIndex = 2;
            this.btnParqetExport.Text = "Parqet CSV Export";
            this.btnParqetExport.UseVisualStyleBackColor = true;
            this.btnParqetExport.Click += new System.EventHandler(this.btnParqetExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bitpanda API:";
            // 
            // txbBitpandaAPI
            // 
            this.txbBitpandaAPI.Location = new System.Drawing.Point(52, 49);
            this.txbBitpandaAPI.Margin = new System.Windows.Forms.Padding(2);
            this.txbBitpandaAPI.Name = "txbBitpandaAPI";
            this.txbBitpandaAPI.Size = new System.Drawing.Size(536, 20);
            this.txbBitpandaAPI.TabIndex = 4;
            // 
            // txbFilePath
            // 
            this.txbFilePath.Location = new System.Drawing.Point(52, 188);
            this.txbFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txbFilePath.Name = "txbFilePath";
            this.txbFilePath.ReadOnly = true;
            this.txbFilePath.Size = new System.Drawing.Size(261, 20);
            this.txbFilePath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Speicherpfad:";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(53, 229);
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
            this.txbFileName.Location = new System.Drawing.Point(52, 117);
            this.txbFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txbFileName.Name = "txbFileName";
            this.txbFileName.Size = new System.Drawing.Size(261, 20);
            this.txbFileName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dateiname:";
            // 
            // txbParqetAcc
            // 
            this.txbParqetAcc.Location = new System.Drawing.Point(354, 117);
            this.txbParqetAcc.Margin = new System.Windows.Forms.Padding(2);
            this.txbParqetAcc.Name = "txbParqetAcc";
            this.txbParqetAcc.Size = new System.Drawing.Size(234, 20);
            this.txbParqetAcc.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Parqet Account Nummer";
            // 
            // btnParqetSynch
            // 
            this.btnParqetSynch.Location = new System.Drawing.Point(354, 229);
            this.btnParqetSynch.Margin = new System.Windows.Forms.Padding(2);
            this.btnParqetSynch.Name = "btnParqetSynch";
            this.btnParqetSynch.Size = new System.Drawing.Size(234, 68);
            this.btnParqetSynch.TabIndex = 12;
            this.btnParqetSynch.Text = "Parqet Synchronisation";
            this.btnParqetSynch.UseVisualStyleBackColor = true;
            this.btnParqetSynch.Click += new System.EventHandler(this.btnParqetSynch_Click);
            // 
            // txbParqetToken
            // 
            this.txbParqetToken.Location = new System.Drawing.Point(354, 188);
            this.txbParqetToken.Margin = new System.Windows.Forms.Padding(2);
            this.txbParqetToken.Name = "txbParqetToken";
            this.txbParqetToken.Size = new System.Drawing.Size(234, 20);
            this.txbParqetToken.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Parqet Accesstoken";
            // 
            // btnSaveInitSettings
            // 
            this.btnSaveInitSettings.Location = new System.Drawing.Point(53, 314);
            this.btnSaveInitSettings.Name = "btnSaveInitSettings";
            this.btnSaveInitSettings.Size = new System.Drawing.Size(535, 23);
            this.btnSaveInitSettings.TabIndex = 15;
            this.btnSaveInitSettings.Text = "Eingaben speichern";
            this.btnSaveInitSettings.UseVisualStyleBackColor = true;
            this.btnSaveInitSettings.Click += new System.EventHandler(this.btnSaveInitSettings_Click);
            // 
            // btnLoadInitSettings
            // 
            this.btnLoadInitSettings.Location = new System.Drawing.Point(53, 343);
            this.btnLoadInitSettings.Name = "btnLoadInitSettings";
            this.btnLoadInitSettings.Size = new System.Drawing.Size(535, 23);
            this.btnLoadInitSettings.TabIndex = 16;
            this.btnLoadInitSettings.Text = "gespeicherte Eingaben laden";
            this.btnLoadInitSettings.UseVisualStyleBackColor = true;
            this.btnLoadInitSettings.Click += new System.EventHandler(this.btnLoadInitSettings_Click);
            // 
            // pgbProgress
            // 
            this.pgbProgress.Location = new System.Drawing.Point(53, 372);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(535, 23);
            this.pgbProgress.TabIndex = 17;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 409);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.btnLoadInitSettings);
            this.Controls.Add(this.btnSaveInitSettings);
            this.Controls.Add(this.txbParqetToken);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnParqetSynch);
            this.Controls.Add(this.txbParqetAcc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txbFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbBitpandaAPI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnParqetExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Bitpanda2Parqet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnParqetExport;
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
    }
}

