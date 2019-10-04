using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class NetworkPerformance
    {
        public string NetworkAbbreviation { get; set; }
        public double NetworkImpressionPercentage { get; set; }
    }

    public class ImpressionsDelivered
    {
        public DateTime AirDate { get; set; }
        public int DailyImpressionCount { get; set; }
        public int CumulativeImpressionCount { get; set; }
    }

    public class OrderPerformance
    {
        public string OrderId { get; set; }
        public double Reach { get; set; }
        public int Frequency { get; set; }
        public double AverageAdCompletionRate { get; set; }
        public int CumulativeImpressionCount { get; set; }
        public int SpotCount { get; set; }
        public int SpotCountAired { get; set; }
        public List<NetworkPerformance> NetworkPerformance { get; set; }
        public List<ImpressionsDelivered> ImpressionsDelivered { get; set; }
        public string SelfServiceId { get; set; }
    }
}
