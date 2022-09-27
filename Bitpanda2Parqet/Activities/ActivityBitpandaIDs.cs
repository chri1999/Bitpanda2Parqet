using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class ActivityBitpandaIDs
    {

        public string TransactionID { get; set; }
        public string ProductId { get; set; }

        public ActivityBitpandaIDs(string transactionID, string productId)
        {
            TransactionID = transactionID;
            ProductId = productId;
        }
    }
}
