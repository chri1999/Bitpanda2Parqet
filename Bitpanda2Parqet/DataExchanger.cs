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
                    records.Add(Activity.ParseStake(jsonData["data"][i]["attributes"]));
                    records[i].isStaking = true;
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

        public static void ExportParqetCSV(List<Activity> activities, string filePath, string fileName)
        {
            try
            {
                filePath = filePath + @"\" + fileName;
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

        public static void ExportPortfolioPerformanceCSV(List<Activity> activities, string filePath, string fileName)
        {
            try
            {
                filePath = filePath + @"\" + fileName;
                if (!filePath.EndsWith(".csv")) filePath += ".csv";

                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("datetime;price;shares;amount;tax;fee;type;assettype;identifier;currency");
                for (int i = 0; i < activities.Count; i++)
                {
                    writer.WriteLine(activities[i].ToPortfolioPerformanceCsvString());
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

    } 
}
