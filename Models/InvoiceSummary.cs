using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class InvoiceSummary
    {
        public string AdvertiserCode { get; set; }
        public string AdvertiserName { get; set; }
        public string BroadcastMonth { get; set; }
        public string Division { get; set; }
        public string Region { get; set; }
        public string InvoiceId { get; set; }
        public decimal? InvoiceActualBalance { get; set; }
        public decimal? InvoiceCurrentBalance { get; set; }
        public string FormattedBroadcaseMonth
        {
            get
            {
                return FormatBroadcastMonth(this.BroadcastMonth);
            }
        }

        public static string FormatBroadcastMonth(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 6)
            {
                return value;
            }
            var yearPart = value.Substring(0, 4);
            var monthPart = value.Substring(4);

            int year;
            int month;
            if (!int.TryParse(yearPart, out year) || !int.TryParse(monthPart, out month))
            {
                return value;
            }
            return new DateTime(year, month, 1).ToString("MMM-yy");
        }
    }
}
