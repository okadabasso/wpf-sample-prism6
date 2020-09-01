using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Models;
using WpfSamples.Models.Entities;

namespace ModelSamle.Services
{
    [DependencyObject]
    public class ProductCreateService
    {
        private NorthwindDbContext _context { get; set; }
        public ProductCreateService(NorthwindDbContext context)
        {
            _context = context;
        }

        [Trace]
        [TransactionMethod]
        public virtual async Task<bool> CreateAsync()
        {
            var supplier = _context.Suppliers.FirstOrDefault();
            var category = _context.Categories.FirstOrDefault();

            var product = new Product
            {
                Category = category,
                Discontinued = true,
                ProductName = "product" + DateTime.Now.ToString(),
                QuantityPerUnit = "15 per box",
                ReorderLevel = 15,
                Supplier = supplier,
                UnitPrice = 100M,
                UnitsInStock = 10,
                UnitsOnOrder = 20
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }
        [Trace]
        [TransactionMethod]
        public virtual bool Create()
        {
            var supplier = _context.Suppliers.FirstOrDefault();
            var category = _context.Categories.FirstOrDefault();

            var product = new Product
            {
                Category = category,
                Discontinued = true,
                ProductName = "product" + DateTime.Now.ToString(),
                QuantityPerUnit = "15 per box",
                ReorderLevel = 15,
                Supplier = supplier,
                UnitPrice = 100M,
                UnitsInStock = 10,
                UnitsOnOrder = 20
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }
    }
}
