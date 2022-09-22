using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    // An object of this class contains statistical information about the downloaded Transactions from Bitpanda
    // To Do: Check data more specifically

    public class BitpandaApiResults
    {
        public int NumberOfDataSets { get; set; }
        public int NumberOfBuys { get; set; }
        public int NumberOfSells { get; set; }
        public int NumberOfBestBonuses { get; set; }
        public int NumberOfStakes { get; set; }
        public int NumberOfUnknownCalls { get; set; }

        public BitpandaApiResults(int numberOfDataSets, int numberOfBuys, int numberOfSells, int numberOfBestBonuses, int numberOfStakes, int numberOfUnknownCalls)
        {
            NumberOfDataSets = numberOfDataSets;
            NumberOfBuys = numberOfBuys;
            NumberOfSells = numberOfSells;
            NumberOfBestBonuses = numberOfBestBonuses;
            NumberOfStakes = numberOfStakes;
            NumberOfUnknownCalls = numberOfUnknownCalls;
        }

        public void GetResultFromBitpandaDataResponse(JToken json)
        {
            NumberOfDataSets++;
            
            if (json["attributes"]["type"].ToString() == "buy")
            {
                NumberOfBuys++;
            }
            else if (json["attributes"]["type"].ToString() == "sell")
            {
                NumberOfSells++;
            }
            else if ((json["attributes"]["type"].ToString() == "transfer"))
            {
                if (json["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Stake")
                {
                    NumberOfStakes++;
                }
                else if (json["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Reward" ||
                         json["attributes"]["tags"][0]["attributes"]["name"].ToString() == "Instant Trade Bonus")
                {
                    NumberOfBestBonuses++;
                }
                else
                {
                    NumberOfUnknownCalls++;
                }
            }
            else if ((json["attributes"]["type"].ToString() == "withdrawal"))
            {
                if (json["attributes"]["is_bfc"].ToString() == "true")
                {
                    NumberOfBestBonuses++;
                }
            }
            else 
            {
                NumberOfUnknownCalls++;
            }
        }

        public void SetZero()
        {
            NumberOfDataSets = 0;
            NumberOfBuys = 0;
            NumberOfSells = 0;
            NumberOfBestBonuses = 0;
            NumberOfStakes = 0;
            NumberOfUnknownCalls = 0;
        }

        public override string ToString()
        {
            return "Number of Datasets: " + NumberOfDataSets + ", where:" +
                "\n\nNumber of Buys: " + NumberOfBuys +
                "\nNumber of Sells: " + NumberOfSells +
                "\nNumber of Stakes: " + NumberOfStakes +
                "\nNumber of Rewards or Best Bonuses: " + NumberOfBestBonuses +
                "\nNumber of Unknown Transactions: " + NumberOfUnknownCalls;
        }

    }
}
