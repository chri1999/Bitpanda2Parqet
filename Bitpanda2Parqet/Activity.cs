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
  
        public string transactionID { get; set; }
        public DateTime timestamp { get; set; }
        public string transactionType { get; set; }
        public string inOut { get; set; }
        public double amountFiat {get; set; }
        public string fiat { get; set; }  
        public double amountAsset { get; set; } 
        public string asset { get; set; }
        public double assetMarketPrice { get; set; }
        public string assetMarketCurrency { get; set; }
        public string assetClass { get;  set;}
        public string productId { get; set; }
        public double fee { get; set; }
        public string feeAsset { get; set; }
        public string spread { get; set; }
        public string spreadCurrency { get; set; }
        public bool isStaking { get; set; }

        public Activity(string transactionID, DateTime timestamp, string transactionType, string inOut, double amountFiat, string fiat, double amountAsset, string asset, double assetMarketPrice, string assetMarketCurrency, string assetClass, string productId, double fee, string feeAsset, string spread, string spreadCurrency)
        {
            this.transactionID = transactionID;
            this.timestamp = timestamp;
            this.transactionType = transactionType;
            this.inOut = inOut;
            this.amountFiat = amountFiat;
            this.fiat = fiat;
            this.amountAsset = amountAsset;
            this.asset = asset;
            this.assetMarketPrice = assetMarketPrice;
            this.assetMarketCurrency = assetMarketCurrency;
            this.assetClass = assetClass;
            this.productId = productId;
            this.fee = fee;
            this.feeAsset = feeAsset;
            this.spread = spread;
            this.spreadCurrency = spreadCurrency;
            isStaking = false;
        }

        public string ToParquetCsvString()
        {
            return timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") + ";" + assetMarketPrice.ToString() + ";" + amountAsset.ToString() + ";" + "" + ";" + "0" +
                ";" + fee + ";" + transactionType + ";" + "Crypto" + ";" + asset + ";" + assetMarketCurrency;
        }

        public string ToParqetApiString(string parqetAcc)
        {
            string dataString = string.Format("[{{\"type\":\"{0}\",\"holding\":\"\",\"datetime\":\"{1}\",\"description\":\"\",\"currency\":\"EUR\",\"price\":{2},\"shares\":{3},\"fee\":0,\"tax\":0,\"allowDuplicate\":false,\"asset\":{{\"identifier\":\"{4},\"assetType\":\"Crypto\"}},\"portfolio\":\"{5}\"}}]",
                char.ToUpper(transactionType[0]) + transactionType.Substring(1),
                timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"),
                assetMarketPrice.ToString().Replace(',','.'),
                amountAsset.ToString().Replace(',', '.'),
                asset,
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
