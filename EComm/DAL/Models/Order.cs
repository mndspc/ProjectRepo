using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [Column("OrderId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Column("OrderDate")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime OrderDate { get; set; }

        [Column("EmailId")]
        [EmailAddress()]
        [StringLength(maximumLength:100,MinimumLength =3)]
        [Required]
        public string? EmailId { get; set; }
    }
}
