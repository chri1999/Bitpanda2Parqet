using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitpanda2Parqet
{
    public partial class MainView : Form
    {
        public event EventHandler<MainViewParameters> ParquetExportRequested;
        public event EventHandler<MainViewParameters> ParquetSynchRequested;
        public event EventHandler LoadInitRequested;
        public event EventHandler<MainViewParameters> SaveInitRequested;

        public MainView()
        {
            InitializeComponent();
        }


        private void btnParqetExport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txbFileName.Text))
            {
                MessageBox.Show("Zuerst Dateiname eingeben!");
            }
            else if (String.IsNullOrWhiteSpace(txbBitpandaAPI.Text))
            {
                MessageBox.Show("Zuerst API eingeben!");
            }
            else if (String.IsNullOrWhiteSpace(txbFilePath.Text))
            {
                MessageBox.Show("Zuerst Pfad auswählen!");
            }
            else
            {
                ParquetExportRequested?.Invoke(this, GetMainViewParameters());
            }

        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txbFilePath.Text = fbd.SelectedPath; ;
                }
            }
        }

        public static void ShowTextMessage(string message)
        {
            MessageBox.Show(message, "Mitteilung", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnParqetSynch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txbBitpandaAPI.Text))
            {
                MessageBox.Show("Zuerst API eingeben!");
            }
            else if (String.IsNullOrWhiteSpace(txbParqetAcc.Text))
            {
                MessageBox.Show("Zuerst Parqet Accountnummer eingeben!");
            }
            else if (String.IsNullOrWhiteSpace(txbParqetToken.Text))
            {
                MessageBox.Show("Zuerst Parqet Token eingeben!");
            }
            else
            {
                ParquetSynchRequested?.Invoke(this, GetMainViewParameters());
            }
        }

        public void SetInitValues(MainViewParameters init)
        {
            txbFileName.Text = init.FileName;
            txbFilePath.Text = init.FilePath;
            txbBitpandaAPI.Text = init.API;
            txbParqetAcc.Text = init.ParqetAcc;
            txbParqetToken.Text = init.ParqetToken;
            cbxExportFormat.SelectedItem = init.ExportFormat;
            dtpDataFromDate.Value = init.DateOfOldestData;
            clbGenerellSettings.SetItemChecked(0, init.IgnoreStaking);
        }

        private MainViewParameters GetMainViewParameters()
        { 
        return new MainViewParameters(txbBitpandaAPI.Text,
            txbFilePath.Text,
            txbFileName.Text,
            txbParqetAcc.Text,
            txbParqetToken.Text,
            (Enums.ExportFormat)cbxExportFormat.SelectedItem,
            dtpDataFromDate.Value,
            clbGenerellSettings.GetItemCheckState(0) == CheckState.Checked);
        }

        private void btnSaveInitSettings_Click(object sender, EventArgs e)
        {
            SaveInitRequested?.Invoke(sender, GetMainViewParameters());
        }

        private void btnLoadInitSettings_Click(object sender, EventArgs e)
        {
            LoadInitRequested?.Invoke(sender, new EventArgs());
        }

        public void SetProgress(int progress)
        {
            pgbProgress.Value = progress;
        }
    }

}
