using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class ProductService : IProductService<Product>
    {
        public async Task<int> AddProduct(Product entity)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                //Passing value to shadow property, if not mentioned in OnModelCreating()
                //dbContext.Entry(entity).Property("CreatedDate").CurrentValue = DateTime.Now;

                dbContext.Products.Add(entity); //Add() method change the state of an entity from UnChanged to Added 
                return await  dbContext.SaveChangesAsync(); //This method observes current entity state (Added) and build the t-sql statement (insert into Product values(...))
                //after calling SaveChangesAsync() it will change entity state from Added to Unchanged
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                var existingProduct = dbContext.Products.FirstOrDefault(pro=>pro.ProductId.Equals(id));
                if (existingProduct is not null)
                {
                    dbContext.Products.Remove(existingProduct);//This method will change entity state from UnChanged to Removed
                    return  await dbContext.SaveChangesAsync();//It observed current entity state and build the t-sql statement (Delete from Product....)
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (EcommContext dbContext = new EcommContext())
            {
                return await dbContext.Products.ToListAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (EcommContext dbContext=new EcommContext())
            {
             return await  dbContext.Products.FirstOrDefaultAsync(pro=>pro.ProductId.Equals(id));
            }
        }

        public async Task<int> UpdateProduct(Product entity)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                var existingProduct = dbContext.Products.FirstOrDefault(pro => pro.ProductId.Equals(entity.ProductId));
                if(existingProduct is not null)
                {
                    //It will change entity state from UnChanged to Modified
                    existingProduct.ProductId = entity.ProductId;
                    existingProduct.ProductName = entity.ProductName;
                    existingProduct.ProductCategory = entity.ProductCategory;
                    existingProduct.ListPrice = entity.ListPrice;
                   return await dbContext.SaveChangesAsync(); //It observed current entity state and build the t-sql statement (Update Product Set...)
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<int> SaveProduct(Product entity)
        {
            using (EcommContext dbContext=new EcommContext())
            {
             return await dbContext.Database.ExecuteSqlRawAsync("EXEC usp_Add_Product  @ProductId,@ProductName, @Category,@ListPrice,@CreatedDate",
                    new SqlParameter("@ProductId", entity.ProductId), new SqlParameter("@ProductName", entity.ProductName), new SqlParameter("@Category", entity.ProductCategory), new SqlParameter("@ListPrice", entity.ListPrice), new SqlParameter("@CreatedDate", DateTime.Now));
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                var productByName =await dbContext.Products.FirstOrDefaultAsync(pro => pro.ProductName.Equals(name));
                return productByName;
            }
        }
    }
}
