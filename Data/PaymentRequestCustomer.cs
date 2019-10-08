using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentRequestCustomer")]
    public partial class PaymentRequestCustomer
    {
        public PaymentRequestCustomer()
        {
            PaymentRequestInvoices = new HashSet<PaymentRequestInvoice>();
        }

        [Column("PaymentRequestCustomerID")]
        public long PaymentRequestCustomerID { get; set; }
        [Column("PaymentRequestID")]
        public long PaymentRequestID { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(50)]
        public string AgencyNumber { get; set; }
        [StringLength(100)]
        public string AgencyName { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PrePayAmount { get; set; }
        public long? AdvertiserID { get; set; }
        public long? AgencyID { get; set; }
        public long? MarketID { get; set; }
        public int? DivisionID { get; set; }
        [Column("PaymentExportStatusTypeID")]
        public int? PaymentExportStatusTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExportDateTimeUtc { get; set; }
        [Column("RefundExportStatusTypeID")]
        public int? RefundExportStatusTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RefundExportDateTimeUtc { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("PaymentRequestCustomers")]
        public virtual Division Division { get; set; }
        [ForeignKey("MarketId")]
        [InverseProperty("PaymentRequestCustomers")]
        public virtual Market Market { get; set; }
        [ForeignKey("PaymentRequestId")]
        [InverseProperty("PaymentRequestCustomers")]
        public virtual PaymentRequest PaymentRequest { get; set; }
        [InverseProperty("PaymentRequestCustomer")]
        public virtual ICollection<PaymentRequestInvoice> PaymentRequestInvoices { get; set; }
    }
}