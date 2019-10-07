using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("Market")]
    public partial class Market
    {
        public Market()
        {
            PaymentRequestCustomers = new HashSet<PaymentRequestCustomer>();
            PaymentRequests = new HashSet<PaymentRequest>();
        }

        [Column("MarketID")]
        public long MarketID { get; set; }
        [Required]
        [StringLength(200)]
        public string MarketName { get; set; }
        [Column("DivisionID")]
        public int? DivisionID { get; set; }
        [StringLength(50)]
        public string InvoiceMappingPrefix { get; set; }
        public bool? AppendInvoicePrefixOnPaymentScreens { get; set; }
        public int? MinInvoiceNumberLength { get; set; }
        public int? MaxInvoiceNumberLength { get; set; }
        public bool? DoNotAllowLetters { get; set; }
        public bool? DoNotAllowNumbers { get; set; }
        public bool? DoNotAllowSpecialCharacters { get; set; }
        public bool? IsMasterRegion { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("Markets")]
        public virtual Division Division { get; set; }
        [InverseProperty("Market")]
        public virtual ICollection<PaymentRequestCustomer> PaymentRequestCustomers { get; set; }
        [InverseProperty("Market")]
        public virtual ICollection<PaymentRequest> PaymentRequests { get; set; }
    }
}