

using System.Collections.Generic;

namespace CryptoTrader.Models
{
    internal class CoinInfoModel
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string market_cap_rank { get; set; }
        public Description description { get; set; }
        public List<Tickers> tickers { get; set; }
        public Image image { get; set; }
    }

    public class Description
    {
        public string en { get; set; }
    }

    public class Tickers
    {
        public string last { get; set; }
        public Market market { get; set; }
        public ConvertedLast converted_last { get; set; }
    }

    public class ConvertedLast
    {
        public string btc { get; set; }
        public string eth { get; set; }
        public string usd { get; set; }
    }

    public class Market
    {
        public string name { get; set; }
    }

    public class Image
    {
        public string small { get; set; }
    }
}
