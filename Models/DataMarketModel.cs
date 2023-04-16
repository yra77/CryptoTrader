

namespace CryptoTrader.Models
{
    internal class DataMarketModel
    { 
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string market_cap { get; set; }
        public string current_price { get; set; }
        public string price_change_percentage_24h { get; set; }
        public string image { get; set; }
        public string colorPrice { get; set; } = "Green";
        public string colorPercentage { get; set; } = "Green";
    }
}
