using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IOrderDetailsService<TEntity>
    {
        Task<List<TEntity>> GetOrderDetailsWithCustomer();

        Task<List<TEntity>> GetOrderDetailsByEmail(string email);

        Task<TEntity> GetOrderDetailsByOrderId(int orderId);
    }
}
