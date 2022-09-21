using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class FormInitializer
    {
        // This class is used to fill in/initialize the textboxes in the MainView with an object and has static methods to load/save the parameters
        // To Do: Exception Handling, init.txt filepath

        public MainViewParameters Parameters { get; set; }

        public FormInitializer(MainViewParameters parameters)
        {
            Parameters = parameters;
        }

        public static MainViewParameters ReadMainViewInitValues()
        {
            if (!File.Exists(@"..\..\init.txt"))
            {
                
                return new MainViewParameters("", "", "", "", "", Enums.ExportFormat.Parqet, DateTime.MinValue, true);
            }

            string[] text = File.ReadAllLines(@"..\..\init.txt");
            string filePath = "";
            string fileName = "";
            string bitpandaApi = "";
            string parqetAcc = "";
            string parqetToken = "";
            Enums.ExportFormat exportFormat = Enums.ExportFormat.Parqet;
            DateTime dateOfOldestData = DateTime.Now.AddYears(-2);
            bool ignoreStaking = true;
                 
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
                string[] splittedText = text[i].Split('=');
                if (splittedText[0] == "Filename") fileName = splittedText[1];
                if (splittedText[0] == "Filepath") filePath = splittedText[1];
                if (splittedText[0] == "Api") bitpandaApi = splittedText[1];
                if (splittedText[0] == "Acc") parqetAcc = splittedText[1];
                if (splittedText[0] == "Token") parqetToken = splittedText[1];
                if (splittedText[0] == "ExportFormat")
                {
                    if (splittedText[1] == Enums.ExportFormat.Parqet.ToString()) exportFormat = Enums.ExportFormat.Parqet;
                    else if (splittedText[1] == Enums.ExportFormat.PortfolioPerformance.ToString()) exportFormat = Enums.ExportFormat.PortfolioPerformance;
                }
                if (splittedText[0] == "MinDate") DateTime.TryParse(splittedText[1],out dateOfOldestData);
                if (splittedText[0] == "IgnoreStaking") bool.TryParse(splittedText[1], out ignoreStaking);
                
            }
            return new MainViewParameters(bitpandaApi, filePath, fileName, parqetAcc, parqetToken, exportFormat, dateOfOldestData, ignoreStaking);
        }

        public static void SaveInitValues(MainViewParameters init)
        {
            string[] content = new string[8];
            content[0] = "Filename=" + init.FileName;
            content[1] = "Filepath=" + init.FilePath;
            content[2] = "Api=" + init.API;
            content[3] = "Acc=" + init.ParqetAcc;
            content[4] = "Token=" + init.ParqetToken;
            content[5] = "ExportFormat=" + init.ExportFormat.ToString();
            content[6] = "MinDate=" + init.DateOfOldestData.ToShortDateString();
            content[7] = "IgnoreStaking=" + init.IgnoreStaking.ToString();

            File.WriteAllLines(@"..\..\init.txt", content);
        }

    }
}
