using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class InvoiceSummary
    {
        public string Division { get; set; }
        public string Region { get; set; }        
        public string InvoiceId { get; set; }
        public decimal? InvoiceActualBalance { get; set; }
        public decimal? InvoiceCurrentBalance { get; set; }
    }
}
