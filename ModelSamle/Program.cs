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

namespace ModelSamle
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.BuildContainer();

            using(var scope = container.BeginLifetimeScope())
            {
                
                foreach(ServiceResult value in Enum.GetValues(typeof(ServiceResult)))
                {
                    Console.WriteLine($"{value.ToString()}= {value.DisplayName()}");
                }

            }
            Console.WriteLine("process end");
            Console.ReadLine();
        }
    }
}
