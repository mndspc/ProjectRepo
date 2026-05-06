using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DAL.Models
{
    [Table("AdminInfo")]
    public class AdminInfo
    {
        [Key]
        [Column("EmailId")]
        [StringLength(maximumLength:100,MinimumLength =2)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? EmailId { get; set; }

        [Column("Password")]
        [StringLength(maximumLength:20,MinimumLength =3)]
        [Required]
        public string? Password { get; set; }

        [Column("Role")]
        [StringLength(maximumLength:10)]
        [DefaultValue("Admin")]
        public string? Role { get; set; }
    }
}
