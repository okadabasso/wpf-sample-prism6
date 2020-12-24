using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Models;
using System.Data.Entity;
using WpfSamples.Models.Entities;
using System.Security.Cryptography.X509Certificates;
using ModelSamle.Services;
using WpfSamples.Infrastructure.Services;
using WpfSamples.Infrastructure.DataTypes;
using AutoMapper;
namespace ModelSamle
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.BuildContainer();

            Product p;
            Product p2;
            Category c;
            using (var scope = container.BeginLifetimeScope())
            {
                var context = scope.Resolve<NorthwindDbContext>();
                p = context.Products.Where(x => x.ProductId == 1).AsNoTracking().FirstOrDefault();
                var mapper = CreateMapper();
                p2 = mapper.Map<Product, Product>(p);

                c = context.Categories.AsNoTracking().FirstOrDefault(x => x.CategoryId == 1);
            }
            p2.ProductName = p.ProductName + " update";
            p2.Category = c;
            p2.CategoryId = c.CategoryId;

            using (var scope = container.BeginLifetimeScope())
            {
                var context = scope.Resolve<NorthwindDbContext>();
                var target = context.Products.Where(x => x.ProductId == 1).FirstOrDefault();

                var mapper = CreateMapper();
                mapper.Map<Product, Product>(p2, target);

                context.SaveChanges();
            }




            Console.WriteLine("process end");
            Console.ReadLine();
        }
        static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(c => {
                c.CreateMap<Product, Product>()
                .ForMember(x => x.OrderDetails, options => options.Ignore())
                .ForMember(x => x.Category, options => options.Ignore())
                .ForMember(x => x.Supplier, options => options.Ignore())
                ;
            });
            return config.CreateMapper();
        }
    }
}
