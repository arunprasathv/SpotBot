using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("Agency")]
    public partial class Agency
    {
        [Column("AgencyID")]
        public long AgencyID { get; set; }
        [Required]
        [StringLength(100)]
        public string AgencyName { get; set; }
        [Column("MarketID")]
        public long MarketID { get; set; }
        [Required]
        [StringLength(50)]
        public string AgencyCode { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("AgencyMasterID")]
        public long? AgencyMasterID { get; set; }
        public int IsActive { get; set; }
    }
}