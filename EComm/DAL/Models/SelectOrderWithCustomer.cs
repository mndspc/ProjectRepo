using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SelectOrderWithCustomer
    {
        public string? EmailId { get; set; }

        public string? Role { get; set; }

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
