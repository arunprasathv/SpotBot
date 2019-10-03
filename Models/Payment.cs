using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class Payment
    {
        public bool SuccessfulPayment { get; set; }
        public int OrderNumber { get; set; }
        public string OrderGuid { get; set; }
        public string Decision { get; set; }
        public string Message { get; set; }
        public double Amount { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime DateTimeUtc { get; set; }
    }
}
