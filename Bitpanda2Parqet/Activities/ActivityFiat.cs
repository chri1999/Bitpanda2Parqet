using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class ActivityFiat
    {
        public double AmountFiat { get; set; }
        public string Fiat { get; set; }

        public ActivityFiat(double amountFiat, string fiat)
        {
            AmountFiat = amountFiat;
            Fiat = fiat;
        }
    }
}
