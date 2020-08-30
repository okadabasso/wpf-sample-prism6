using Autofac;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.Logging;
using WpfSamples.Models;
using WpfSamples.Models.Entities;
using System.Data.Entity;
using System.Windows;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class ProductListWindowViewModel : BindableBase
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public ProductListWindowViewModel(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
            _logger.BlockTrace(() => {
                LoadedCommand = new DelegateCommand(OnLoaded);
                SubmitCommand = new DelegateCommand(Submit);
            });
        }
        [Trace]
        protected virtual void OnLoaded()
        {
            using(var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                var products = db.Products
                    .Include(product => product.Supplier)
                    .Include(product => product.Category)
                    .OrderBy(x => x.ProductId);
                Products.Clear();
                Products.AddRange(products);

                RaisePropertyChanged();
            }
        }
        [Trace]
        protected virtual void Submit()
        {

        }
    }
}
