using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class MainViewSyncParameters
    {
        public string API { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ParqetAcc { get; set; }
        public string ParqetToken { get; set; }

        public MainViewSyncParameters(string aPI, string filePath, string fileName, string parqetAcc, string parqetToken)
        {
            API = aPI;
            FilePath = filePath;
            FileName = fileName;
            ParqetAcc = parqetAcc;
            ParqetToken = parqetToken;
        }
    }
}
