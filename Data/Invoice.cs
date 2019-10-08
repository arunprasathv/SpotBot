using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("Invoice")]
    public partial class Invoice
    {
        public long FileID { get; set; }
        public long InvoiceID { get; set; }
        [Column("CBInvoiceIdentifier")]
        public long? CbinvoiceIdentifier { get; set; }
        public long MarketID { get; set; }
        public long AdvertiserID { get; set; }
        [Required]
        [StringLength(6)]
        public string BroadcastMonth { get; set; }
        public bool? New { get; set; }
        public bool? Viewed { get; set; }
        public bool InActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [StringLength(500)]
        public string FilePath { get; set; }
        [Column("XMLFile")]
        [StringLength(500)]
        public string Xmlfile { get; set; }
        public bool? FileProcessed { get; set; }
        public long? AgencyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FileProcessedDate { get; set; }
        [Column("pdfFilePath")]
        [StringLength(500)]
        public string PdfFilePath { get; set; }
        [Column("xlsFilePath")]
        [StringLength(500)]
        public string XlsFilePath { get; set; }
        [Column("AE")]
        [StringLength(500)]
        public string Ae { get; set; }
        [Column("SendStatusID")]
        public int SendStatusID { get; set; }
        [Column("CBBatchId")]
        public long? CbbatchID { get; set; }
        [Column("InvoiceArID")]
        public long? InvoiceArID { get; set; }
        [Column("DivisionID")]
        public int? DivisionID { get; set; }
        [Column("AdvertiserMasterID")]
        public long? AdvertiserMasterID { get; set; }
        [Column("AgencyMasterID")]
        public long? AgencyMasterID { get; set; }
        [StringLength(500)]
        public string InvoiceIdentifier { get; set; }
        [StringLength(500)]
        public string InvoiceNumber { get; set; }
        [StringLength(500)]
        public string InvoiceNumberWithoutPrefix { get; set; }
        [Column(TypeName = "money")]
        public decimal? OriginalInvoiceAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? CurrentBalanceAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? CalculatedCurrentBalanceAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? PendingScheduledPaymentAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? PendingRefundAmount { get; set; }
        public bool? IsPendingPaymentExist { get; set; }
        public bool IsActive { get; set; }
        [Column("MASBatchId")]
        public long? MasbatchID { get; set; }
    }
}