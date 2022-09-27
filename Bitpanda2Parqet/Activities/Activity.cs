using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class Activity
    {

    // An object of this class represents a Bitpanda Transaction 
  
        public DateTime Timestamp { get; set; }
        public string TransactionType { get; set; }
        public string InOut { get; set; }
        public ActivityBitpandaIDs BitpandaIDs { get; set; }
        public ActivityAsset Asset { get; set; }
        public ActivityFiat Fiat { get; set; }
        public ActivityFees Fees { get; set; }
        public Enums.ActivityType InternalActivityType { get; set; }
        public bool IsStaking { get; set; }



        public Activity(DateTime timestamp, string transactionType, ActivityAsset asset, ActivityFees fees, Enums.ActivityType internalActivityType)
        {
            this.Timestamp = timestamp;
            this.TransactionType = transactionType;
            this.Asset = asset;
            this.Fees = fees;
            IsStaking = false;
            InternalActivityType = internalActivityType;
        }

        public Activity(ActivityBitpandaIDs bitpandaIDs, DateTime timestamp, string transactionType, string inOut, ActivityFiat fiat, ActivityAsset asset, ActivityFees fees)
        {
            this.BitpandaIDs = bitpandaIDs;
            this.Timestamp = timestamp;
            this.TransactionType = transactionType;
            this.InOut = inOut;
            this.Fiat = fiat;
            this.Asset = asset;
            this.Fees = fees;
            IsStaking = false;
        }

        public string ToParquetCsvString()
        {
            return Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") + ";" + Asset.AssetMarketPrice.ToString() + ";" + Asset.AmountAsset.ToString() + ";" + "" + ";" + "0" +
                ";" + Fees.Fee + ";" + TransactionType + ";" + "Crypto" + ";" + Asset.Asset + ";" + Asset.AssetMarketCurrency;
        }

        public string ToPortfolioPerformanceCsvString()
        {
            string ppType = "";
            if (InternalActivityType == Enums.ActivityType.Buy) ppType = "Kaufen";
            else if (InternalActivityType == Enums.ActivityType.Sell) ppType = "Verkaufen";
            else ppType = "Not implemented yet";
            

            return Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") + ";" + ppType + ";"+ (Asset.AssetMarketPrice * Asset.AmountAsset).ToString() + ";" + Fiat.Fiat + ";" + Asset.AmountAsset.ToString() + ";" + Asset.Asset + "-EUR" + ";" + Asset.AssetMarketPrice.ToString() + ";" + Asset.AssetMarketCurrency  ;
        }

        public string ToParqetApiString(string parqetAcc)
        {
            string dataString = string.Format("[{{\"type\":\"{0}\",\"holding\":\"\",\"datetime\":\"{1}\",\"description\":\"\",\"currency\":\"EUR\",\"price\":{2},\"shares\":{3},\"fee\":0,\"tax\":0,\"allowDuplicate\":false,\"asset\":{{\"identifier\":\"{4},\"assetType\":\"Crypto\"}},\"portfolio\":\"{5}\"}}]",
                char.ToUpper(TransactionType[0]) + TransactionType.Substring(1),
                Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"),
                Asset.AssetMarketPrice.ToString().Replace(',','.'),
                Asset.AmountAsset.ToString().Replace(',', '.'),
                Asset.Asset,
                parqetAcc
                );
            return dataString;
        }

        public static Activity ParseBuyOrSell(JToken obj)
        {
            ActivityAsset asset = new ActivityAsset((double)obj["trade"]["attributes"]["amount_cryptocoin"], (string)obj["trade"]["attributes"]["cryptocoin_symbol"], (double)obj["trade"]["attributes"]["price"], "EUR", "Cryptocurrency");
            ActivityFiat fiat = new ActivityFiat((double)obj["trade"]["attributes"]["amount_fiat"], "EUR");
            ActivityFees fees = new ActivityFees(0, "EUR", "", "EUR");
            ActivityBitpandaIDs id = new ActivityBitpandaIDs((string)obj["trade"]["id"], (string)obj["trade"]["attributes"]["cryptocoin_id"]);

            return new Activity(    id,
                                    DateTime.ParseExact((string)obj["trade"]["attributes"]["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                                    (string)obj["trade"]["attributes"]["type"],
                                    "",
                                    fiat,
                                    asset,
                                    fees                      
                                    );
        }

        public static Activity ParseStake(JToken obj)
        {
            string type = "";
            double price = 0.001;      // yet no way to add dividends or set price to 0 in parqet
            ActivityAsset asset = new ActivityAsset((double)obj["amount"], (string)obj["cryptocoin_symbol"], price, "EUR", "Cryptocurrency");
            ActivityFiat fiat = new ActivityFiat((double)obj["amount_eur"], "EUR");
            ActivityFees fees = new ActivityFees(0, "EUR", "", "EUR");
            ActivityBitpandaIDs id = new ActivityBitpandaIDs((string)obj["id"], (string)obj["cryptocoin_id"]);

            if ((string)obj["in_or_out"] == "incoming") type = "buy";
            else type = "sell";

            return new Activity(    id,
                                    DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                                    type,
                                    (string)obj["in_or_out"],                                     
                                    fiat,
                                    asset,
                                    fees
                                    );
        }

        public static Activity ParseReward(JToken obj)
        {
            return ParseStake(obj);
        }

        public static Activity ParseInstantTradeBonus(JToken obj)
        {
            return ParseStake(obj);
        }

        public static Activity ParseBestFeeReduction(JToken obj)
        {
            string type = "";
            double price = 0.001;      // yet no way to add dividends or set price to 0 in parqet
            ActivityAsset asset = new ActivityAsset((double)obj["fee"], (string)obj["cryptocoin_symbol"], price, "EUR", "Cryptocurrency");
            ActivityFiat fiat = new ActivityFiat((double)obj["amount_eur"], "EUR");
            ActivityFees fees = new ActivityFees(0,"EUR", "", "EUR");
            ActivityBitpandaIDs id = new ActivityBitpandaIDs((string)obj["id"], (string)obj["cryptocoin_id"]);

            if ((string)obj["in_or_out"] == "incoming") type = "buy";
            else type = "sell";

            return new Activity(       id,
                                       DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                                       type,
                                       (string)obj["in_or_out"],
                                       fiat,
                                       asset,
                                       fees                                      
                                       );
        }
    }


}
