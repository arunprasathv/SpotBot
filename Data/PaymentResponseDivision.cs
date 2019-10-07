using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("PaymentResponseDivision")]
    public partial class PaymentResponseDivision
    {
        public long ID { get; set; }
        [Column("PaymentResponseID")]
        public long PaymentResponseID { get; set; }
        [Column("DivisionID")]
        public int DivisionID { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? AuthorizeAmount { get; set; }
        public DateTime? AuthorizeDateTimeUtc { get; set; }
        [StringLength(50)]
        public string PaymentProcessorTransactionIdentifier { get; set; }
        [StringLength(50)]
        public string Decision { get; set; }
        [Column("PaymentRequestID")]
        public long? PaymentRequestID { get; set; }
        [StringLength(50)]
        public string AuthorizeTransactionReferenceIdentifier { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("PaymentResponseDivisions")]
        public virtual Division Division { get; set; }
        [ForeignKey("PaymentResponseId")]
        [InverseProperty("PaymentResponseDivisions")]
        public virtual PaymentResponse PaymentResponse { get; set; }
    }
}