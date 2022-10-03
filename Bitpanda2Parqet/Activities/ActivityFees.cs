using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class ActivityFees
    {
        public double Fee { get; set; }
        public string FeeAsset { get; set; }
        public string Spread { get; set; }
        public string SpreadCurrency { get; set; }

        public ActivityFees(double fee, string feeAsset, string spread, string spreadCurrency)
        {
            Fee = fee;
            FeeAsset = feeAsset;
            Spread = spread;
            SpreadCurrency = spreadCurrency;
        }
    }
}
