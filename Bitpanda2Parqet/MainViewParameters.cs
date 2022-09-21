using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class MainViewParameters
    {
        // An object of this class stores strings entered into the MainView textboxes

        public string API { get; set; }
        public string FilePath { get; set; }
        public string ParqetAcc { get; set; }
        public string ParqetToken { get; set; }
        public Enums.ExportFormat ExportFormat { get; set; }
        public DateTime DateOfOldestData { get; set; }
        public bool IgnoreStaking { get; set; }


        public MainViewParameters(string aPI, string filePath, string parqetAcc, string parqetToken, Enums.ExportFormat exportFormat, DateTime dateOfOldestData, bool ignoreStaking)
        {
            API = aPI;
            FilePath = filePath;
            ParqetAcc = parqetAcc;
            ParqetToken = parqetToken;
            ExportFormat = exportFormat;
            DateOfOldestData = dateOfOldestData;
            IgnoreStaking = ignoreStaking;
        }

    }
}
