using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class ActivityAsset
    {
        public double AmountAsset { get; set; }
        public string Asset { get; set; }
        public double AssetMarketPrice { get; set; }
        public string AssetMarketCurrency { get; set; }
        public string AssetClass { get; set; }



        public ActivityAsset(double amountAsset, string asset, double assetMarketPrice, string assetMarketCurrency, string assetClass)  
        {
            AmountAsset = amountAsset;
            Asset = asset;
            AssetMarketPrice = assetMarketPrice;
            AssetMarketCurrency = assetMarketCurrency;
            AssetClass = assetClass;
        }
    }
}
