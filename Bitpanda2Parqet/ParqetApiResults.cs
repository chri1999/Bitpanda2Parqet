using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class ParqetApiResults
    {
        public int NumberOfSuccesses { get; set; }
        public int NumberOfDuplications { get; set; }
        public int NumberOfUnknownCoins { get;set; }
        public int NumberOfFailures { get; set; }
        public int NumberOfDataSets { get; set; }

        public ParqetApiResults(int numberOfSuccesses, int numberOfDuplications, int numberOfUnknownCoins, int numberOfFailures, int numberOfDataSets)
        {
            NumberOfSuccesses = numberOfSuccesses;
            NumberOfDuplications = numberOfDuplications;
            NumberOfUnknownCoins = numberOfUnknownCoins;
            NumberOfFailures = numberOfFailures;
            NumberOfDataSets = numberOfDataSets;
        }

        public void GetResultFromParqetResponse(string response)
        {
            NumberOfDataSets++;
            JObject jsonResponse = JObject.Parse(response);

            if (jsonResponse["createdActivities"].Count() == 1)
            {
                NumberOfSuccesses++;
            }
            else if (jsonResponse["notCreatedActivities"][0]["error"]["code"].ToString() == "ACTIVITY_CREATION_EXISTING_DUPLICATE")
            {
                NumberOfDuplications++;
            }
            else if (jsonResponse["notCreatedActivities"][0]["error"]["code"].ToString() == "ACTIVITY_CREATION_NO_SECURITY_DATA")
            {
                NumberOfUnknownCoins++;
            }
            else if (jsonResponse["notCreatedActivities"].Count() == 1)
            {
                NumberOfFailures++;
            }
        }

        public void SetZero()
        {
            NumberOfSuccesses = 0;
            NumberOfDuplications = 0;
            NumberOfFailures = 0;
            NumberOfUnknownCoins = 0;
            NumberOfDataSets = 0;
        }

        public override string ToString()
        {
            return "Number of Datasets: " +  NumberOfDataSets +
                "\nNumber of Successes: " + NumberOfSuccesses +
                "\nNumber of Duplications: " + NumberOfDuplications +
                "\nNumber of Unknown Coins: " + NumberOfUnknownCoins +
                "\nNumber of Fails: " + NumberOfFailures;
        }

    }
}
