using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("Advertiser")]
    public partial class Advertiser
    {
        [Column("AdvertiserID")]
        public long AdvertiserID { get; set; }
        [StringLength(100)]
        public string AdvertiserName { get; set; }
        [Column("MarketID")]
        public long? MarketID { get; set; }
        [StringLength(50)]
        public string AdvertiserCode { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("AdvertiserMasterID")]
        public long? AdvertiserMasterID { get; set; }
        public int IsActive { get; set; }
        [StringLength(50)]
        public string CreditStatus { get; set; }
    }
}