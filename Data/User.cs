using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.SpotBot.Data
{
    public partial class User
    {
        public User()
        {
            PaymentInstruments = new HashSet<PaymentInstrument>();
            PaymentRequests = new HashSet<PaymentRequest>();
        }

        [Column("UserID")]
        public long UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public int? SecretQuestion { get; set; }
        [StringLength(500)]
        public string SecretAnswer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpirationDate { get; set; }
        [Column("UserTypeID")]
        public int? UserTypeID { get; set; }
        public bool? ActiveDirectory { get; set; }
        [Column("CustomerGUID")]
        public Guid? CustomerGuid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastEmailSentOn { get; set; }
        [StringLength(100)]
        public string Company { get; set; }
        [Column("CustomerID")]
        [StringLength(50)]
        public string CustomerID { get; set; }
        public bool ForceNewPassword { get; set; }
        public int LoginCount { get; set; }
        [StringLength(40)]
        public string LoginKey { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDateTimeUtc { get; set; }
        public string PasswordHash { get; set; }
        [StringLength(50)]
        public string PasswordSalt { get; set; }
        [StringLength(100)]
        public string ApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedOn { get; set; }
        public bool? NotifyNewUserAccessRequest { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<PaymentInstrument> PaymentInstruments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<PaymentRequest> PaymentRequests { get; set; }
    }
}