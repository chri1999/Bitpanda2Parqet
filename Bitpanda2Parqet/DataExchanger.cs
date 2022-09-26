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
            Enums.ActivityType typeOfActivity;
            result = new BitpandaApiResults(0, 0, 0, 0, 0, 0);
            var records = new List<Activity>();
            for (int i = 0; i < jsonData["data"].Count(); i++)
            {
                typeOfActivity = IdentifySortOfActivity(jsonData["data"][i]);
                result.NumberOfDataSets++;

                if (typeOfActivity == Enums.ActivityType.Buy)
                {
                    records.Add(Activity.ParseBuyOrSell(jsonData["data"][i]["attributes"]));
                    result.NumberOfBuys++;
                }
                else if (typeOfActivity == Enums.ActivityType.Sell)
                {
                    records.Add(Activity.ParseBuyOrSell(jsonData["data"][i]["attributes"]));
                    result.NumberOfSells++;
                }
                else if (typeOfActivity == Enums.ActivityType.OutgoingStake || typeOfActivity == Enums.ActivityType.IncomingStake)
                {
                    records.Add(Activity.ParseStake(jsonData["data"][i]["attributes"]));
                    records[i].IsStaking = true;
                    result.NumberOfStakes++;
                }
                else if (typeOfActivity == Enums.ActivityType.InstantTradeBonus)
                {
                    records.Add(Activity.ParseInstantTradeBonus(jsonData["data"][i]["attributes"]));
                    result.NumberOfBestBonuses++;
                }
                else if (typeOfActivity == Enums.ActivityType.incomingReward)
                {
                    records.Add(Activity.ParseReward(jsonData["data"][i]["attributes"]));
                    result.NumberOfBestBonuses++;
                }
                else if (typeOfActivity == Enums.ActivityType.BestFeeReduction)
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
                writer.WriteLine("Datum;Typ;Wert;Buchungswährung;Stück;Ticker-Symbol;Bruttobetrag;Währung Bruttobetrag");
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
                        Content = new StringContent("[{\"type\":\"" + char.ToUpper(activities[i].TransactionType[0]) + activities[i].TransactionType.Substring(1) + "\",\"holding\":\"\",\"datetime\":\"" + activities[i].Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'") + "\",\"description\":\"\",\"currency\":\"EUR\",\"price\":" + activities[i].AssetMarketPrice.ToString().Replace(',', '.') + ",\"shares\":" + activities[i].AmountAsset.ToString().Replace(',', '.') + ",\"fee\":0,\"tax\":0,\"allowDuplicate\":false,\"asset\":{\"identifier\":\"" + activities[i].Asset + "\",\"assetType\":\"Crypto\"},\"portfolio\":\"" + parqetAcc + "\"}]")
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

        private static Enums.ActivityType IdentifySortOfActivity(JToken obj)
        {
            Enums.ActivityType sortOfActivity = Enums.ActivityType.Undefined;

            if (obj["attributes"]["type"].ToString() == "withdrawal" &&
                obj["attributes"]["is_bfc"].ToString() == "True") sortOfActivity = Enums.ActivityType.BestFeeReduction;

            else if (obj["attributes"]["type"].ToString() == "buy") sortOfActivity = Enums.ActivityType.Buy;

            else if (obj["attributes"]["type"].ToString() == "sell") sortOfActivity = Enums.ActivityType.Sell;

            if (obj["attributes"]["tags"].Count() > 0)
            {
                if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Stake" &&
                         obj["attributes"]["in_or_out"].ToString() == "outgoing") sortOfActivity = Enums.ActivityType.OutgoingStake;

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Stake" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = Enums.ActivityType.IncomingStake;

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Instant Trade Bonus" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = Enums.ActivityType.InstantTradeBonus;

                else if (obj["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Reward" &&
                         obj["attributes"]["in_or_out"].ToString() == "incoming") sortOfActivity = Enums.ActivityType.IncomingStake;
            }



            return sortOfActivity;
        }

    } 
}
