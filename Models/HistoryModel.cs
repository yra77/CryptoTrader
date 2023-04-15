

using System;

namespace CryptoTrader.Models
{
    internal class HistoryModel
    {
        public string priceUsd { get; set; }
        public string time { get; set; }
        public string date { get; set; }
    }

    public class ToolkPoint
    {

        public ToolkPoint(double value, long time)
        {
            Value = value;
            Time = (DateTimeOffset.FromUnixTimeSeconds(time/1000)).DateTime;
        }
        public double Value { get; set; }
        public DateTime Time { get; set; }

    }
}
