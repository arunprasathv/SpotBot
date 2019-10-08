using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentRequest")]
    public partial class PaymentRequest
    {
        public PaymentRequest()
        {
            PaymentInstruments = new HashSet<PaymentInstrument>();
            PaymentRequestCustomers = new HashSet<PaymentRequestCustomer>();
            PaymentResponses = new HashSet<PaymentResponse>();
        }

        [Column("PaymentRequestID")]
        public long PaymentRequestID { get; set; }
        [Column("UserID")]
        public long? UserID { get; set; }
        [Column("UserTypeID")]
        public int UserTypeID { get; set; }
        [Column("EffectedUserTypeID")]
        public int EffectedUserTypeID { get; set; }
        [Column("MarketID")]
        public long MarketID { get; set; }
        public Guid PaymentRequestGuid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PaymentRequestDateTimeUtc { get; set; }
        [Column("PaymentExportStatusTypeID")]
        public int? PaymentExportStatusTypeID { get; set; }
        public Guid? PaymentProcessor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExportDateTimeUtc { get; set; }
        [StringLength(50)]
        public string TransactionTypeCode { get; set; }
        public int? PaymentProcessorTransactionStatusID { get; set; }
        public string PaymentProcessorApiError { get; set; }
        public long? AdvertiserID { get; set; }
        public long? AgencyID { get; set; }
        public long? PaymentInstrumentID { get; set; }
        [Column("RefundExportStatusTypeID")]
        public int? RefundExportStatusTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RefundExportDateTimeUtc { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PaymentAmount { get; set; }
        [Column("RecurringPaymentID")]
        public long? RecurringPaymentID { get; set; }
        [Column("RecurringPaymentItemID")]
        public long? RecurringPaymentItemID { get; set; }
        [StringLength(100)]
        public string PaymentUserName { get; set; }
        public bool? SaveCreditCardRequested { get; set; }
        [Column("SaveCreditCardUserID")]
        public long? SaveCreditCardUserID { get; set; }
        [Column("SelfServiceOrderID")]
        public Guid? SelfServiceOrderID { get; set; }
        public string PaymentNotes { get; set; }
        [StringLength(100)]
        public string PaymentProxyUserName { get; set; }
        [StringLength(50)]
        public string MerchantIdentifier { get; set; }

        [ForeignKey("MarketId")]
        [InverseProperty("PaymentRequests")]
        public virtual Market Market { get; set; }
        [ForeignKey("PaymentInstrumentId")]
        [InverseProperty("PaymentRequests")]
        public virtual PaymentInstrument PaymentInstrument { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("PaymentRequests")]
        public virtual User User { get; set; }
        [InverseProperty("OriginalPaymentRequest")]
        public virtual ICollection<PaymentInstrument> PaymentInstruments { get; set; }
        [InverseProperty("PaymentRequest")]
        public virtual ICollection<PaymentRequestCustomer> PaymentRequestCustomers { get; set; }
        [InverseProperty("PaymentRequest")]
        public virtual ICollection<PaymentResponse> PaymentResponses { get; set; }
    }
}