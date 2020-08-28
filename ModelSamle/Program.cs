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

namespace ModelSamle
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.BuildContainer();

            using(var scope = container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();

                var order = db.Orders.Include(x => x.OrderDetails).FirstOrDefault(x => x.OrderId == 11078);
            }
            Console.WriteLine("process end");
            Console.ReadLine();
        }
    }
}
