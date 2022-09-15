using CsvHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Bitpanda2Parqet
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            MainView mainView = new MainView();
            MainViewPresenter presenter = new MainViewPresenter(mainView);

            Application.Run(mainView);
        }

        

    }



}
