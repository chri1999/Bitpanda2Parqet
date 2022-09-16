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

        public string BitpandaApi { get; set; }
        public string ParqetAcc { get; set; }
        public string ParqetToken { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public FormInitializer(string bitpandaApi, string parqetAcc, string parqetToken, string filePath, string fileName)
        {
            BitpandaApi = bitpandaApi;
            ParqetAcc = parqetAcc;
            ParqetToken = parqetToken;
            FilePath = filePath;
            FileName = fileName;
        }

        public static FormInitializer GetMainViewInitValues()
        {
            if (!File.Exists(@"..\..\init.txt"))
            {
                return new FormInitializer("", "", "", "", "");
            }

            string[] text = File.ReadAllLines(@"..\..\init.txt");
            string filePath = "";
            string fileName = "";
            string bitpandaApi = "";
            string parqetAcc = "";
            string parqetToken = "";

            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
                string[] splittedText = text[i].Split('=');
                if (splittedText[0] == "Filename") fileName = splittedText[1];
                if (splittedText[0] == "Filepath") filePath = splittedText[1];
                if (splittedText[0] == "Api") bitpandaApi = splittedText[1];
                if (splittedText[0] == "Acc") parqetAcc = splittedText[1];
                if (splittedText[0] == "Token") parqetToken = splittedText[1];
            }
            return new FormInitializer(bitpandaApi, parqetAcc, parqetToken, filePath, fileName);
        }

        public static void SaveInitValues(FormInitializer init)
        {
            string[] content = new string[5];
            content[0] = "Filename=" + init.FileName;
            content[1] = "Filepath=" + init.FilePath;
            content[2] = "Api=" + init.BitpandaApi;
            content[3] = "Acc=" + init.ParqetAcc;
            content[4] = "Token=" + init.ParqetToken;

            File.WriteAllLines(@"..\..\init.txt", content);
        }

    }
}
