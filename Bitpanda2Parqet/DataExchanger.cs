using CsvHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitpanda2Parqet
{
    public class DataExchanger
    {
        // This class is used to to interact with the local filesystem and the Api's of Bitpanda and Parqet
        // To Do: Improve parsing of Bitpanda data!! -> BitpandaJsonParse + ParseBitpandaTrade etc.

        public static List<Activity> DownloadDataFromBitpandaAPI(string aPIKey, out BitpandaApiResults result)
        {
            var records = new List<Activity>();
            try
            {
                JObject bitPandaTrades = JObject.Parse(MakeBitPandaTrasactionsCall(aPIKey));

                records = ParseActivitiesFromJson(bitPandaTrades, out result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nBitpanda API kontrollieren!");
            }
            return records;
        }

        private static List<Activity> ParseActivitiesFromJson(JObject jsonData, out BitpandaApiResults result)
        {
            string typeOfActivity = "";
            result = new BitpandaApiResults(0, 0, 0, 0, 0, 0);
            var records = new List<Activity>();
            for (int i = 0; i < jsonData["data"].Count(); i++)
            {
                typeOfActivity = IdentifySortOfActivity(jsonData["data"][i]);
                result.NumberOfDataSets++;

                if (typeOfActivity == "buy")
                {
                    records.Add(Activity.ParseBuyOrSell(jsonData["data"][i]["attributes"]));
                    result.NumberOfBuys++;
                }
                else if (typeOfActivity == "sell")
                {
                    records.Add(Activity.ParseBuyOrSell(jsonData["data"][i]["attributes"]));
                    result.NumberOfSells++;
                }
                else if (typeOfActivity == "outgoingStake" || typeOfActivity == "incomingStake")
                {
                    //records.Add(Activity.ParseStake(jsonData["data"][i]["attributes"])); //ignore stakes
                    result.NumberOfStakes++;
                }
                else if (typeOfActivity == "instantTradeBonus")
                {
                    records.Add(Activity.ParseInstantTradeBonus(jsonData["data"][i]["attributes"]));
                    result.NumberOfBestBonuses++;
                }
                else if (typeOfActivity == "incomingReward")
                {
                    records.Add(Activity.ParseReward(jsonData["data"][i]["attributes"]));
                    result.NumberOfBestBonuses++;
                }
                else if (typeOfActivity == "bestFeeReduction")
                {
                    records.Add(Activity.ParseBestFeeReduction(jsonData["data"][i]["attributes"]));
                    result.NumberOfBestBonuses++;
                }
                else
                {
                    result.NumberOfUnknownCalls++;  // add errorlog 
                }               
            }
            return records;
        }

        public static void ExportParquetCSV(List<Activity> activities, string filePath)
        {
            try
            {
                if (!filePath.EndsWith(".csv")) filePath += ".csv";

                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("datetime;price;shares;amount;tax;fee;type;assettype;identifier;currency");
                for (int i = 0; i < activities.Count; i++)
                {
                    writer.WriteLine(activities[i].ToParquetCsvString());
                }
                writer.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Writing process failed");
            }
        }

        public static async Task UploadDataToParqetAPI(List<Activity> activities, string parqetAcc, string parqetToken, BackgroundWorker worker, ParqetApiResults results)
        {
            try
            {
                for (int i = 0; i < activities.Count; i++)
                {
                    var clientHandler = new HttpClientHandler       // Code from Insomnia
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    };
                    var client = new HttpClient(clientHandler);
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://api.parqet.com/v1/portfolios/" + parqetAcc + "/activities?useNewResponseFormat=true"),
                        Headers =
                    {
                        { "Accept", "application/json, text/plain, */*" },
                        { "Origin", "https://app.parqet.com" },
                        { "Authorization", "Bearer " + parqetToken},
                        { "Referer", "https://app.parqet.com/" },
                        { "Connection", "keep-alive" },
                    },
                        Content = new StringContent("[{\"type\":\"" + char.ToUpper(activities[i].transactionType[0]) + activities[i].transactionType.Substring(1) + "\",\"holding\":\"\",\"datetime\":\"" + activities[i].timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'") + "\",\"description\":\"\",\"currency\":\"EUR\",\"price\":" + activities[i].assetMarketPrice.ToString().Replace(',', '.') + ",\"shares\":" + activities[i].amountAsset.ToString().Replace(',', '.') + ",\"fee\":0,\"tax\":0,\"allowDuplicate\":false,\"asset\":{\"identifier\":\"" + activities[i].asset + "\",\"assetType\":\"Crypto\"},\"portfolio\":\"" + parqetAcc + "\"}]")
                        {
                            Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                        }
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        results.GetResultFromParqetResponse(body);
                        worker.ReportProgress(Convert.ToInt32((100 * i) / activities.Count));
                    }
                }
                worker.ReportProgress(100);
            }
            catch (HttpRequestException httpex)
            {
                throw new Exception(httpex.Message + "\n\n Check Parqet Account/Token!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string MakeBitPandaTrasactionsCall(string API_KEY)       // max page size: 500, add page parameter for more datasets
        {
            var client = new WebClient();
            client.Headers.Add("X-API-KEY", API_KEY);
            return client.DownloadString("https://api.bitpanda.com/v1/wallets/transactions?page=1&page_size=500");
        }

        private static string IdentifySortOfActivity(JToken obj)
        {
            string sortOfActivity = "undefined";

            if (obj["attributes"]["type"].ToString() == "withdrawal" &&
                obj["attributes"]["is_bfc"].ToString() == "True") sortOfActivity = "bestFeeReduction";

            else if (obj["attributes"]["type"].ToString() == "buy") sortOfActivity = "buy";

            else if (obj["attributes"]["type"].ToString() == "sell") sortOfActivity = "sell";

            if (obj["attributes"]["tags"].Count() > 0)
            {
                if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Stake" &&
                         obj["attributes"]["in_or_out"].ToString() == "outgoing") sortOfActivity = "outgoingStake";

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Stake" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = "incomingStake";

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Instant Trade Bonus" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = "instantTradeBonus";

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Reward" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = "incomingReward";
            }



            return sortOfActivity;
        }



        // old Parse methods

        //private static List<Activity> BitpandaJsonParse(JObject jsonData, out BitpandaApiResults result)
        //{
        //    result = new BitpandaApiResults(0,0,0,0,0,0);
        //    var records = new List<Activity>();
        //    for (int i = 0; i < jsonData["data"].Count(); i++)
        //    {
        //        if ((jsonData["data"][i]["attributes"]["type"].ToString() == "buy") ||
        //            (jsonData["data"][i]["attributes"]["type"].ToString() == "sell"))
        //        {
        //            records.Add(ParseBitpandaTrade(jsonData["data"][i]["attributes"]["trade"]));
        //        }
        //        else if ((jsonData["data"][i]["attributes"]["type"].ToString() == "transfer") &&
        //                 (jsonData["data"][i]["attributes"]["tags"][0]["attributes"]["name"].ToString() != "Stake"))  // ignore outgoing staked coins, Best rewards should be ok
        //        {
        //            records.Add(ParseBitpandaTransaction(jsonData["data"][i]["attributes"]));
        //        }
        //        else if ((jsonData["data"][i]["attributes"]["type"].ToString() == "withdrawal"))
        //        {
        //            records.Add(ParseBitpandaWithdrawal(jsonData["data"][i]["attributes"]));
        //        }
        //        result.GetResultFromBitpandaDataResponse(jsonData["data"][i]);

        //    }

        //    return records;
        //}

        //private static Activity ParseBitpandaTrade(JToken obj)
        //{
        //    return new Activity((string)obj["id"],
        //                               DateTime.ParseExact((string)obj["attributes"]["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
        //                               (string)obj["attributes"]["type"],
        //                               "",
        //                               (double)obj["attributes"]["amount_fiat"],
        //                               "EUR",
        //                               (double)obj["attributes"]["amount_cryptocoin"],
        //                               (string)obj["attributes"]["cryptocoin_symbol"],
        //                               (double)obj["attributes"]["price"],
        //                               "EUR",
        //                               "Cryptocurrency",
        //                               (string)obj["attributes"]["cryptocoin_id"],
        //                               0,
        //                               "EUR",
        //                               "",
        //                               "EUR"
        //                               );
        //}

        //private static Activity ParseBitpandaTransaction(JToken obj)
        //{
        //    string type = "";
        //    if ((string)obj["in_or_out"] == "incoming") type = "buy";
        //    else type = "sell";

        //    return new Activity((string)obj["id"],
        //                               DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
        //                               type,         
        //                               (string)obj["in_or_out"],
        //                               (double)obj["amount_eur"],
        //                               "EUR",
        //                               (double)obj["amount"],
        //                               (string)obj["cryptocoin_symbol"],
        //                               0.0001,                             //price crypto (example: Best Rewards, haven't yet found a way to add "crypto dividends" or price 0), needs to be edited though
        //                               "EUR",
        //                               "Cryptocurrency",
        //                               (string)obj["cryptocoin_id"],
        //                               0,
        //                               "EUR",
        //                               "",
        //                               "EUR"
        //                               );
        //}

        //private static Activity ParseBitpandaWithdrawal(JToken obj)
        //{
        //    string type = "";
        //    if ((string)obj["in_or_out"] == "incoming") type = "buy";
        //    else type = "sell";

        //    return new Activity((string)obj["id"],
        //                               DateTime.ParseExact((string)obj["time"]["date_iso8601"], "MM/dd/yyyy HH:mm:ss", null),
        //                               type,
        //                               (string)obj["in_or_out"],
        //                               (double)obj["amount_eur"],
        //                               "EUR",
        //                               (double)obj["amount"],
        //                               (string)obj["cryptocoin_symbol"],
        //                               0.0001,                             //price crypto
        //                               "EUR",
        //                               "Cryptocurrency",
        //                               (string)obj["cryptocoin_id"],
        //                               0,
        //                               "EUR",
        //                               "",
        //                               "EUR"
        //                               );
        //}



    } 
}
