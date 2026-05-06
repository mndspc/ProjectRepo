using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EcommContext:DbContext
    {

        //To declare prop. of type DbSet<TEntity>
        public DbSet<AdminInfo> AdminInfos { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SelectOrderWithCustomer> SelectOrdersWithCustomers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //To configure a connection string
            if (!optionsBuilder.IsConfigured)
            {
              var conStr=  DatabaseHelper.GetConnectionString();
                optionsBuilder.UseSqlServer(conStr);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //To configure entities/model using fluent api

            //One-To-Many relationship (CustomerInfo-->Order)
            modelBuilder.Entity<Order>().HasOne<CustomerInfo>().WithMany().HasForeignKey(cust=>cust.EmailId);

            //To Add Seed data for AdminInfo
            modelBuilder.Entity<AdminInfo>().HasData(
             new AdminInfo { EmailId = "admin@gmail.com", Password = "admin123", Role = "Admin" }
              );

            //To declare shadow property
            modelBuilder.Entity<Product>().Property<DateTime>("CreatedDate").HasDefaultValueSql("GETDATE()");

            //To map view with entity
            modelBuilder.Entity<SelectOrderWithCustomer>().HasNoKey().ToView("Select_Order_With_Customer");

            base.OnModelCreating(modelBuilder);
        }
    }
}
