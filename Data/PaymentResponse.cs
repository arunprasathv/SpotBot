using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentResponse")]
    public partial class PaymentResponse
    {
        public PaymentResponse()
        {
            PaymentInstruments = new HashSet<PaymentInstrument>();
            PaymentResponseDivisions = new HashSet<PaymentResponseDivision>();
        }

        [Column("PaymentResponseID")]
        public long PaymentResponseID { get; set; }
        [Column("PaymentRequestID")]
        public long PaymentRequestID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AuthorizeDateTimeUtc { get; set; }
        [StringLength(50)]
        public string AuthorizeTransactionReferenceIdentifier { get; set; }
        [StringLength(50)]
        public string Decision { get; set; }
        [StringLength(200)]
        public string Message { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? AuthorizeAmount { get; set; }
        [StringLength(50)]
        public string RequestCardType { get; set; }
        [StringLength(50)]
        public string PaymentCardName { get; set; }
        [StringLength(50)]
        public string RequestPaymentMethod { get; set; }
        [StringLength(50)]
        public string RequestCardNumber { get; set; }
        [StringLength(50)]
        public string RequestCardExpiration { get; set; }
        public string ResponseDataJson { get; set; }
        [StringLength(5)]
        public string PaymentStatusCode { get; set; }
        [StringLength(250)]
        public string PaymentStatusMessage { get; set; }
        public Guid? PaymentResponseRawGuid { get; set; }
        [StringLength(50)]
        public string RequestTransactionTypeCode { get; set; }
        [StringLength(50)]
        public string PaymentProcessorTransactionIdentifier { get; set; }
        public bool? Level3Eligible { get; set; }
        [StringLength(100)]
        public string PaymentToken { get; set; }
        [StringLength(50)]
        public string BillToState { get; set; }
        [StringLength(50)]
        public string BillToPostalCode { get; set; }
        [StringLength(50)]
        public string BillToCountry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VoidRefundDateTimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDateTimeUtc { get; set; }
        [StringLength(50)]
        public string RequestRoutingNumber { get; set; }
        [StringLength(50)]
        public string RequestAccountNumber { get; set; }
        [StringLength(10)]
        public string RequestAccountType { get; set; }
        [StringLength(50)]
        public string PaymentAccountName { get; set; }
        [StringLength(50)]
        public string RequestCheckNumber { get; set; }

        [ForeignKey("PaymentRequestId")]
        [InverseProperty("PaymentResponses")]
        public virtual PaymentRequest PaymentRequest { get; set; }
        [InverseProperty("OriginalPaymentResponse")]
        public virtual ICollection<PaymentInstrument> PaymentInstruments { get; set; }
        [InverseProperty("PaymentResponse")]
        public virtual ICollection<PaymentResponseDivision> PaymentResponseDivisions { get; set; }
    }
}