using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("CustomerInfo")]
    public class CustomerInfo
    {
        [Key]
        [Column("EmailId")]
        [StringLength(maximumLength:100,MinimumLength =2)]
        public string?  EmailId   { get; set; }

        [Column("Password")]
        [StringLength(maximumLength:20,MinimumLength =3)]
        [Required]
        public string? Password { get; set; }

        [Column("Role")]
        [StringLength(maximumLength:10)]
        [DefaultValue("Customer")]
        public string? Role { get; set; }
    }
}
