using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CryptoIndexFund
{
    // URL for total market cap is https://graphs2.coinmarketcap.com/global/marketcap-total/1510685220000/1518634020000/
    // URL for bitcoin is https://graphs2.coinmarketcap.com/currencies/bitcoin/1510688968000/1518637768000/
    class Program
    {
        static void Main(string[] args)
        {

            var p = new Program();

            p.GetCurrencyMarketCap("ethereum", DateTime.Now.Subtract(TimeSpan.FromDays(60)), DateTime.Now);
        }

        public void GetCurrencyMarketCap(string currency, DateTime start, DateTime end)
        {
            string url = GetCurrencyMarketCapURL(currency, start, end);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string responseText;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                responseText = reader.ReadToEnd();
            }

            CurrencyMarketCap ethMC = JsonConvert.DeserializeObject<CurrencyMarketCap>(responseText);

            Debug.WriteLine($"The price of ethereum on Dec 25, 2017 was {ethMC.GetPriceAtDate(new DateTime(2017, 12, 25))}");
            //Console.WriteLine(responseText);
            //Debug.WriteLine(responseText);

        }

        public string GetCurrencyMarketCapURL(string currency, DateTime start, DateTime end)
        {
            string url = $"https://graphs2.coinmarketcap.com/currencies/{currency}/{Util.DateTimeToEpochTicks(start)}/{Util.DateTimeToEpochTicks(end)}/";
            return url;
        }

        DateTime EpochStart = new DateTime(1970, 1, 1);
        public long DateTimeToEpochTicks(DateTime dateTime)
        {
            return (long)Math.Round((dateTime - EpochStart).TotalSeconds) * 1000;
        }
    }

    public class Util
    {
        private static DateTime EpochStart = new DateTime(1970, 1, 1);
        public static long DateTimeToEpochTicks(DateTime dateTime)
        {
            return (long)Math.Round((dateTime - EpochStart).TotalSeconds) * 1000;
        }
    }
}
