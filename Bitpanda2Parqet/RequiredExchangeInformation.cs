using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class RequiredExchangeInformation
    {
        // An object of this class stores strings entered into the MainView textboxes

        public string API { get; set; }
        public string FilePath { get; set; }
        public string ParqetAcc { get; set; }
        public string ParqetToken { get; set; }

        public RequiredExchangeInformation(string aPI, string filePath, string parqetAcc, string parqetToken)
        {
            API = aPI;
            FilePath = filePath;
            ParqetAcc = parqetAcc;
            ParqetToken = parqetToken;
        }

    }
}
