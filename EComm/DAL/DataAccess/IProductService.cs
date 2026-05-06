using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public  interface IProductService<TEntity>
    {
        Task <int> AddProduct(TEntity entity);

        Task<int> DeleteProduct(int id);

        Task<int> UpdateProduct(TEntity entity);

        Task<TEntity> GetProductById(int id);

        Task<TEntity> GetProductByName(string name);
        Task<List<TEntity>> GetAllProducts();

        Task<int> SaveProduct(TEntity entity);

    }
}
