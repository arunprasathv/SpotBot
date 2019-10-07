using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentRequestInvoice")]
    public partial class PaymentRequestInvoice
    {
        [Column("PaymentRequestInvoiceID")]
        public long PaymentRequestInvoiceID { get; set; }
        [Column("PaymentRequestCustomerID")]
        public long PaymentRequestCustomerID { get; set; }
        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal PaymentAmount { get; set; }
        [Column("ArInvoiceID")]
        public long? ArInvoiceID { get; set; }

        [ForeignKey("PaymentRequestCustomerId")]
        [InverseProperty("PaymentRequestInvoices")]
        public virtual PaymentRequestCustomer PaymentRequestCustomer { get; set; }
    }
}