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
  
        public string TransactionID { get; set; }
        public DateTime Timestamp { get; set; }
        public string TransactionType { get; set; }
        public string InOut { get; set; }
        public double AmountFiat {get; set; }
        public string Fiat { get; set; }  
        public double AmountAsset { get; set; } 
        public string Asset { get; set; }
        public double AssetMarketPrice { get; set; }
        public string AssetMarketCurrency { get; set; }
        public string AssetClass { get;  set;}
        public string ProductId { get; set; }
        public double Fee { get; set; }
        public string FeeAsset { get; set; }
        public string Spread { get; set; }
        public string SpreadCurrency { get; set; }
        public Enums.ActivityType InternalActivityType { get; set; }
        public bool IsStaking { get; set; }



        public Activity(DateTime timestamp, string transactionType, double amountAsset, string asset, double assetMarketPrice, string assetMarketCurrency, double fee, Enums.ActivityType internalActivityType)
        {
            this.Timestamp = timestamp;
            this.TransactionType = transactionType;
            this.AmountAsset = amountAsset;
            this.Asset = asset;
            this.AssetMarketPrice = assetMarketPrice;
            this.AssetMarketCurrency = assetMarketCurrency;
            this.Fee = fee;
            IsStaking = false;
            InternalActivityType = internalActivityType;
        }

        public Activity(string transactionID, DateTime timestamp, string transactionType, string inOut, double amountFiat, string fiat, double amountAsset, string asset, double assetMarketPrice, string assetMarketCurrency, string assetClass, string productId, double fee, string feeAsset, string spread, string spreadCurrency)
        {
            this.TransactionID = transactionID;
            this.Timestamp = timestamp;
            this.TransactionType = transactionType;
            this.InOut = inOut;
            this.AmountFiat = amountFiat;
            this.Fiat = fiat;
            this.AmountAsset = amountAsset;
            this.Asset = asset;
            this.AssetMarketPrice = assetMarketPrice;
            this.AssetMarketCurrency = assetMarketCurrency;
            this.AssetClass = assetClass;
            this.ProductId = productId;
            this.Fee = fee;
            this.FeeAsset = feeAsset;
            this.Spread = spread;
            this.SpreadCurrency = spreadCurrency;
            IsStaking = false;
        }

        public string ToParquetCsvString()
        {
            return Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") + ";" + AssetMarketPrice.ToString() + ";" + AmountAsset.ToString() + ";" + "" + ";" + "0" +
                ";" + Fee + ";" + TransactionType + ";" + "Crypto" + ";" + Asset + ";" + AssetMarketCurrency;
        }

        public string ToPortfolioPerformanceCsvString()
        {
            return Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") + ";" + AssetMarketPrice.ToString();
        }


        public string ToParqetApiString(string parqetAcc)
        {
            string dataString = string.Format("[{{\"type\":\"{0}\",\"holding\":\"\",\"datetime\":\"{1}\",\"description\":\"\",\"currency\":\"EUR\",\"price\":{2},\"shares\":{3},\"fee\":0,\"tax\":0,\"allowDuplicate\":false,\"asset\":{{\"identifier\":\"{4},\"assetType\":\"Crypto\"}},\"portfolio\":\"{5}\"}}]",
                char.ToUpper(TransactionType[0]) + TransactionType.Substring(1),
                Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"),
                AssetMarketPrice.ToString().Replace(',','.'),
                AmountAsset.ToString().Replace(',', '.'),
                Asset,
                parqetAcc
                );
            return dataString;
        }

        public static Activity ParseBuyOrSell(JToken obj)
        {
            return new Activity((string)obj["trade"]["id"],
                           DateTime.ParseExact((string)obj["trade"]["attributes"]["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                           (string)obj["trade"]["attributes"]["type"],
                           "",
                           (double)obj["trade"]["attributes"]["amount_fiat"],
                           "EUR",
                           (double)obj["trade"]["attributes"]["amount_cryptocoin"],
                           (string)obj["trade"]["attributes"]["cryptocoin_symbol"],
                           (double)obj["trade"]["attributes"]["price"],
                           "EUR",
                           "Cryptocurrency",
                           (string)obj["trade"]["attributes"]["cryptocoin_id"],
                           0,
                           "EUR",
                           "",
                           "EUR"
                           );
        }

        public static Activity ParseStake(JToken obj)
        {
            string type = "";
            double price = 0.001;      // yet no way to add dividends or set price to 0
            if ((string)obj["in_or_out"] == "incoming") type = "buy";
            else type = "sell";

            return new Activity((string)obj["id"],
                                       DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                                       type,
                                       (string)obj["in_or_out"],
                                       (double)obj["amount_eur"],
                                       "EUR",
                                       (double)obj["amount"],
                                       (string)obj["cryptocoin_symbol"],
                                       price,                             
                                       "EUR",
                                       "Cryptocurrency",
                                       (string)obj["cryptocoin_id"],
                                       0,
                                       "EUR",
                                       "",
                                       "EUR"
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
            double price = 0.001;      // yet no way to add dividends or set price to 0
            if ((string)obj["in_or_out"] == "incoming") type = "buy";
            else type = "sell";

            return new Activity((string)obj["id"],
                                       DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
                                       type,
                                       (string)obj["in_or_out"],
                                       (double)obj["amount_eur"],
                                       "EUR",
                                       (double)obj["fee"],
                                       (string)obj["cryptocoin_symbol"],
                                       price,
                                       "EUR",
                                       "Cryptocurrency",
                                       (string)obj["cryptocoin_id"],
                                       0,
                                       "EUR",
                                       "",
                                       "EUR"
                                       );
        }
    }


}
