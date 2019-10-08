using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentInstrument")]
    public partial class PaymentInstrument
    {
        public PaymentInstrument()
        {
            PaymentRequests = new HashSet<PaymentRequest>();
        }

        public long ID { get; set; }
        public long UserID { get; set; }
        [Required]
        [StringLength(100)]
        public string PaymentToken { get; set; }
        [StringLength(100)]
        public string InstrumentName { get; set; }
        public long? OriginalPaymentRequestID { get; set; }
        public long? OriginalPaymentResponseID { get; set; }
        public long? OriginalTokenRequestID { get; set; }
        public long? OriginalTokenResponseID { get; set; }
        [StringLength(10)]
        public string CreditCardLast4 { get; set; }
        public int? CreditCardExpirationMonth { get; set; }
        public int? CreditCardExpirationYear { get; set; }
        [StringLength(50)]
        public string CreditCardName { get; set; }
        public bool? Level3Eligible { get; set; }
        [StringLength(50)]
        public string BillToState { get; set; }
        [StringLength(50)]
        public string BillToPostalCode { get; set; }
        [StringLength(50)]
        public string BillToCountry { get; set; }
        public int? RecurringPaymentFailCount { get; set; }
        public long? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public long? ModifiedByUserID { get; set; }
        public DateTime? ModifiedDateTimeUtc { get; set; }
        public bool? IsExpired { get; set; }
        [StringLength(10)]
        public string InstrumentType { get; set; }
        [StringLength(10)]
        public string RoutingNumberLast4 { get; set; }
        [StringLength(10)]
        public string AccountNumberLast4 { get; set; }
        [StringLength(50)]
        public string AccountType { get; set; }
        [StringLength(50)]
        public string RequestCheckNumber { get; set; }

        [ForeignKey("OriginalPaymentRequestId")]
        [InverseProperty("PaymentInstruments")]
        public virtual PaymentRequest OriginalPaymentRequest { get; set; }
        [ForeignKey("OriginalPaymentResponseId")]
        [InverseProperty("PaymentInstruments")]
        public virtual PaymentResponse OriginalPaymentResponse { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("PaymentInstruments")]
        public virtual User User { get; set; }
        [InverseProperty("PaymentInstrument")]
        public virtual ICollection<PaymentRequest> PaymentRequests { get; set; }
    }
}