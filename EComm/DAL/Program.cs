using DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL
{
    internal class Program
    {
        static void Main()
        {
          //  ProductService productService = new ProductService();
          //var rowAffected=  productService.SaveProduct(new Product { ProductId = 5, ProductName = "Keyboard", ProductCategory = "Gadget", ListPrice = 500 });
          //  Console.WriteLine(rowAffected.Result>0?"Product Added":"Error");

            OrderDetailsService orderDetailsService = new OrderDetailsService();
           var allOrders= orderDetailsService.GetOrderDetailsWithCustomer();
            foreach (var order in allOrders.Result)
            {
                Console.WriteLine($"{order.EmailId}\t{order.OrderId}\t{order.OrderDate}\t{order.Role}");
            }

            Console.WriteLine("========Select Orders by email id=========");
            var orderByEmail = orderDetailsService.GetOrderDetailsByEmail("scott@gmail.com");
            foreach (var order in orderByEmail.Result)
            {
                Console.WriteLine($"{order.EmailId}\t{order.OrderId}\t{order.OrderDate}\t{order.Role}");
            }

            Console.WriteLine("========Select Order by order id=========");
          var orderById=  orderDetailsService.GetOrderDetailsByOrderId(2).Result;
            Console.WriteLine($"{orderById.EmailId}\t{orderById.OrderId}\t{orderById.OrderDate}\t{orderById.Role}");
        }
    }
}
