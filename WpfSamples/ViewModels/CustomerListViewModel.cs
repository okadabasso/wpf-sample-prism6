using Autofac;
using NLog;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Models;
using WpfSamples.Models.Entities;

namespace WpfSamples.ViewModels
{
    public class CustomerListViewModel : BindableBase
    {
        private readonly IContainer container;
        private readonly ILogger logger;

        public ObservableCollection<Customer> Customers { get; set; }
        public CustomerListViewModel(IContainer container, ILogger logger)
        {
            this.container = container;
            this.logger = logger;

            LoadCustomers();
        }
        private void LoadCustomers()
        {
            using(var scope = container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                Customers = new ObservableCollection<Customer>(db.Customers.OrderBy(x => x.CustomerId).AsNoTracking().ToList());
            }
        }
    }
}
