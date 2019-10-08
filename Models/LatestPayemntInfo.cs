using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class LatestPayemntInfo
    {
        public string AdvertiserCode { get; set; }
        public string AdvertiserName { get; set; }
        public string Division { get; set; }
        public string Region { get; set; }
        public string PaymentType { get; set; }
        public string Tender { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }       
        public string ConfirmationNumber { get; set; }
        public string UserPaid { get; set; }
        public List<InvoiceData> Invoices { get; set; }
    }

    public class InvoiceData
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceAmount { get; set; }
    }
}
