using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class MainViewSettingsParameters
    {
        public Enums.ExportFormat ExportFormat { get; set; }
        public DateTime DateOfOldestData { get; set; }
        public bool IgnoreStaking { get; set; }

        public MainViewSettingsParameters(Enums.ExportFormat exportFormat, DateTime dateOfOldestData, bool ignoreStaking)
        {
            ExportFormat = exportFormat;
            DateOfOldestData = dateOfOldestData;
            IgnoreStaking = ignoreStaking;
        }
    }
}
