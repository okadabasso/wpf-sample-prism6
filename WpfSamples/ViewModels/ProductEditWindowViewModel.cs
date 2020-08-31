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
using Reactive;
using Reactive.Bindings;
using Prism.Interactivity.InteractionRequest;
using WpfSamples.Notifications;
using System.Reactive.Linq;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class ProductEditWindowViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;

        private SubWindowOpenNotification _notification;
        public INotification Notification
        {
            get
            {
                return _notification;
            }
            set
            {
                this._notification = value as SubWindowOpenNotification;
            }
        }

        public Action FinishInteraction { get; set; }

        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand SubmitCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand CloseCommand { get; set; } = new ReactiveCommand();


        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Supplier> Suppliers { get; set; } = new ObservableCollection<Supplier>();

        public ReactiveProperty<int> ProductId { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<string> ProductName { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<int?> SupplierId { get; set; } = new ReactiveProperty<int?>();
        public ReactiveProperty<int?> CategoryId { get; set; } = new ReactiveProperty<int?>();
        public ReactiveProperty<string> QuantityPerUnit { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<decimal?> UnitPrice { get; set; } = new ReactiveProperty<decimal?>();
        public ReactiveProperty<short?> UnitsInStock { get; set; } = new ReactiveProperty<short?>();
        public ReactiveProperty<short?> UnitsOnOrder { get; set; } = new ReactiveProperty<short?>();
        public ReactiveProperty<short?> ReorderLevel { get; set; } = new ReactiveProperty<short?>();
        public ReactiveProperty<bool> Discontinued { get; set; } = new ReactiveProperty<bool>();


        public ProductEditWindowViewModel(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;


            LoadedCommand.Subscribe(OnLoaded);
            SubmitCommand.Subscribe(OnSubmit);

        }

        [Trace]
        protected virtual void OnLoaded()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();

                Suppliers.Clear();
                Suppliers.AddRange(new ObservableCollection<Supplier>(db.Suppliers
                    .OrderBy(x => x.SupplierId)
                    .AsNoTracking()
                    ));

                Categories.Clear();
                Categories.AddRange(new ObservableCollection<Category>(db.Categories
                    .OrderBy(x => x.CategoryId)
                    .AsNoTracking()
                    ));

                var product = db.Products.AsNoTracking().FirstOrDefault(x => x.ProductId == _notification.Id);
                ProductId.Value = product.ProductId;
                ProductName.Value = product.ProductName;
                SupplierId.Value = product.SupplierId;
                CategoryId.Value = product.CategoryId;
                QuantityPerUnit.Value = product.QuantityPerUnit;
                UnitPrice.Value = product.UnitPrice;
                UnitsInStock.Value = product.UnitsInStock;
                UnitsOnOrder.Value = product.UnitsOnOrder;
                ReorderLevel.Value = product.ReorderLevel;
                Discontinued.Value = product.Discontinued;

            }

        }



        [Trace]
        protected virtual void OnSubmit()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                using(var transaction = db.Database.BeginTransaction())
                {
                    var product = db.Products.FirstOrDefault(x => x.ProductId == _notification.Id);

                    product.ProductName = ProductName.Value;
                    product.SupplierId = SupplierId.Value;
                    product.CategoryId = CategoryId.Value;
                    product.QuantityPerUnit = QuantityPerUnit.Value;
                    product.UnitPrice = UnitPrice.Value ;
                    product.UnitsInStock = UnitsInStock.Value;
                    product.UnitsOnOrder = UnitsOnOrder.Value;
                    product.ReorderLevel = ReorderLevel.Value;
                    product.Discontinued = Discontinued.Value;

                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            FinishInteraction();

        }
        [Trace]
        protected virtual void OnClose()
        {
            FinishInteraction();
        }
    }
}
