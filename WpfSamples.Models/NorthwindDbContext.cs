














using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using WpfSamples.Models.Entities;
namespace WpfSamples.Models
{
    public class NorthwindDbContext : DbContext{
        public NorthwindDbContext()
            : base("name=NorthwindDb")
        {
            Database.SetInitializer<NorthwindDbContext>(null);
            //Database.Log = x => Console.Out.WriteLine(x);
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }

        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Region> Region { get; set; }

        public virtual DbSet<Shipper> Shippers { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()

                .HasMany(e => e.Products)

                .WithOptional(e => e.Categories)

                .HasForeignKey(e => e.CategoryId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<CustomerCustomerDemo>()
                .Property(e => e.CustomerId)
                .IsFixedLength();


            modelBuilder.Entity<CustomerCustomerDemo>()
                .Property(e => e.CustomerTypeId)
                .IsFixedLength();

            modelBuilder.Entity<CustomerDemographic>()

                .HasMany(e => e.CustomerCustomerDemos)

                .WithRequired(e => e.CustomerDemographics)

                .HasForeignKey(e => e.CustomerTypeId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<CustomerDemographic>()
                .Property(e => e.CustomerTypeId)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()

                .HasMany(e => e.CustomerCustomerDemos)

                .WithRequired(e => e.Customers)

                .HasForeignKey(e => e.CustomerId)

                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()

                .HasMany(e => e.Orders)

                .WithOptional(e => e.Customers)

                .HasForeignKey(e => e.CustomerId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerId)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()

                .HasMany(e => e.Employees)

                .WithOptional(e => e.ReportsToEmployee)

                .HasForeignKey(e => e.ReportsTo)

                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()

                .HasMany(e => e.EmployeeTerritories)

                .WithRequired(e => e.Employees)

                .HasForeignKey(e => e.EmployeeId)

                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()

                .HasMany(e => e.Orders)

                .WithOptional(e => e.Employees)

                .HasForeignKey(e => e.EmployeeId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()

                .HasMany(e => e.OrderDetails)

                .WithRequired(e => e.Orders)

                .HasForeignKey(e => e.OrderId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerId)
                .IsFixedLength();


            modelBuilder.Entity<Order>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()

                .HasMany(e => e.OrderDetails)

                .WithRequired(e => e.Products)

                .HasForeignKey(e => e.ProductId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Region>()

                .HasMany(e => e.Territories)

                .WithRequired(e => e.Region)

                .HasForeignKey(e => e.RegionId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Region>()
                .Property(e => e.RegionDescription)
                .IsFixedLength();

            modelBuilder.Entity<Shipper>()

                .HasMany(e => e.ShipViaOrders)

                .WithOptional(e => e.ShipViaShippers)

                .HasForeignKey(e => e.ShipVia)

                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()

                .HasMany(e => e.Products)

                .WithOptional(e => e.Suppliers)

                .HasForeignKey(e => e.SupplierId)

                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Territory>()

                .HasMany(e => e.EmployeeTerritories)

                .WithRequired(e => e.Territories)

                .HasForeignKey(e => e.TerritoryId)

                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Territory>()
                .Property(e => e.TerritoryDescription)
                .IsFixedLength();


        }
    }
}
