using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotBot.Models
{
    public class Commission
    {
        public double TotalMonthlyPayout { get; set; }
        public double YTDCommissionsPaid { get; set; }
        public double RevenueAdjustments { get; set; }
        public double ChargeBacks { get; set; }
    }
}
