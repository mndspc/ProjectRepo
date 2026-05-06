using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class OrderDetailsService : IOrderDetailsService<SelectOrderWithCustomer>
    {
        public async Task<List<SelectOrderWithCustomer>> GetOrderDetailsByEmail(string email)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                return await dbContext.SelectOrdersWithCustomers.Where(order => order.EmailId.Equals(email)).ToListAsync();
            }
        }

        public async Task<SelectOrderWithCustomer> GetOrderDetailsByOrderId(int orderId)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                return await dbContext.SelectOrdersWithCustomers.FirstOrDefaultAsync(order=>order.OrderId.Equals(orderId));
            }
        }

        public async Task<List<SelectOrderWithCustomer>> GetOrderDetailsWithCustomer()
        {
            using (EcommContext dbContext = new EcommContext())
            {
                return await dbContext.SelectOrdersWithCustomers.ToListAsync();
            }
        }
    }
}
