using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    [Table("Division")]
    public partial class Division
    {
        public Division()
        {
            Markets = new HashSet<Market>();
            PaymentRequestCustomers = new HashSet<PaymentRequestCustomer>();
            PaymentResponseDivisions = new HashSet<PaymentResponseDivision>();
        }

        [Column("DivisionID")]
        public int DivisionID { get; set; }
        [Required]
        [StringLength(50)]
        public string DivisionName { get; set; }
        [StringLength(50)]
        public string PaymentProcessorProfileIdentifier { get; set; }
        [StringLength(100)]
        public string PaymentProcessorAccessKey { get; set; }
        [StringLength(500)]
        public string PaymentProcessorSecretKey { get; set; }
        [StringLength(10)]
        public string ShipFromZipCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedByUserName { get; set; }
        [StringLength(10)]
        public string ShipFromStateAbbreviation { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<Market> Markets { get; set; }
        [InverseProperty("Division")]
        public virtual ICollection<PaymentRequestCustomer> PaymentRequestCustomers { get; set; }
        [InverseProperty("Division")]
        public virtual ICollection<PaymentResponseDivision> PaymentResponseDivisions { get; set; }
    }
}