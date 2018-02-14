using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoIndexFund
{
    public class CurrencyMarketCap
    {
        public decimal[][] market_cap_by_available_supply { get; set; }
        public decimal[][] price_btc { get; set; }
        public decimal[][] price_usd { get; set; }
        public decimal[][] volume_usd { get; set; }

        public decimal GetPriceAtDate(DateTime date)
        {
            long dateTicks = Util.DateTimeToEpochTicks(date);
            return price_usd.First(p => p[0] > dateTicks)[1];
        }

        public decimal GetMarketCapAtDate(DateTime date)
        {
            long dateTicks = Util.DateTimeToEpochTicks(date);
            return market_cap_by_available_supply.First(p => p[0] > dateTicks)[1];
        }
    }

    public class TotalMarketCap
    {
        public decimal[][] market_cap_by_available_supply { get; set; }
        public decimal[][] volume_usd { get; set; }

        public decimal GetMarketCapAtDate(DateTime date)
        {
            long dateTicks = Util.DateTimeToEpochTicks(date);
            return market_cap_by_available_supply.First(p => p[0] > dateTicks)[1];
        }
    }

}

