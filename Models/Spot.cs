using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class Spot
    {
        public string SpotID { get; set; }
        public int OneTimOrderId { get; set; }
        public string Description { get; set; }
        public string Advertiser { get; set; }
        public List<SpotLog> SpotDetails { get; set; }
    }

    public class SpotLog
    {
        public string Zone { get; set; }
        public string Network { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime AiredAt { get; set; }
        public string ErrorStatus { get; set; }
        public bool AiredSuccessful { get; set; }
    }
}
