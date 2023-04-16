using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Constants
{
    internal class Path_Constants
    {
        public const string LIST_COIN_PATH = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=50&page=1&sparkline=false&locale=en&image=small";
        public const string COIN_INFO_PATH = "https://api.coingecko.com/api/v3/coins/bitcoin?localization=false&market_data=false&community_data=false&developer_data=false&sparkline=false";
        public const string HISTORY_PATH1 = "https://api.coingecko.com/api/v3/coins/";
        public const string HISTORY_PATH2 = "/market_chart?vs_currency=usd&days=30&interval=daily";

        // "https://api.coincap.io/v2/assets?limit=15"//"https://api.coincap.io/v2/assets/";
    }

}
