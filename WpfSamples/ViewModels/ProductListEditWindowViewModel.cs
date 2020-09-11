using Autofac;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WpfSamples.Models;
using System.Threading.Tasks;

namespace WpfSamples.ViewModels
{
    public class ProductListEditWindowViewModel : BindableBase
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;

        public ReactiveCommand OnLoadedCommand { get; set; }
        public ReactiveCommand SaveCommand { get; set; }
        public ReactiveCommand CloseCommand { get; set; }
        public ProductListEditWindowViewModel(IContainer container, ILogger logger)
        {
            this._container = container;
            this._logger = logger;


            OnLoadedCommand = new ReactiveCommand();
            OnLoadedCommand.Subscribe(OnLoaded);

        }

        private async void OnLoaded()
        {
            using(var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();

                var products = await db.Products
                                    .Include(product => product.Supplier)
                                    .Include(product => product.Category)
                                    .OrderBy(x => x.ProductId)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
        }
    }
}
