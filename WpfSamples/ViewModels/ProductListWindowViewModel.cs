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
using WpfSamples.Views;

namespace WpfSamples.ViewModels
{
    [DependencyObject]
    public class ProductListWindowViewModel : BindableBase
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        public ReactiveCommand ShowEditWindowCommand { get; set; } = new ReactiveCommand();
        public InteractionRequest<Notification> ShowEditWindowRequest{ get; set; } = new InteractionRequest<Notification>();

        public ReadOnlyReactiveCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ReactiveProperty<int> SelectedCategoryId { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> SelectedProductId { get; set; } = new ReactiveProperty<int>();
        public ProductListWindowViewModel(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
            _logger.BlockTrace(() =>
            {
                LoadedCommand = new DelegateCommand(OnLoaded);
                SubmitCommand = new DelegateCommand(Submit);
                ShowEditWindowCommand.Subscribe(ShowEditWindow);
                SelectedCategoryId.Subscribe(value =>
                {
                    _logger.Trace($"value changed {value}");
                    UpdateProductList();
                });
            });
        }
        [Trace]
        protected virtual void OnLoaded()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                var products = new ObservableCollection<Product>(db.Products
                    .Include(product => product.Supplier)
                    .Include(product => product.Category)
                    .OrderBy(x => x.ProductId)
                    );
                Products = products.ToReadOnlyReactiveCollection(product => product);

                var categories = new ObservableCollection<Category>(db.Categories
                    .OrderBy(x => x.CategoryId)
                    .AsNoTracking()
                    );
                Categories.Clear();
                Categories.AddRange(categories);

                RaisePropertyChanged(nameof(Products));
                RaisePropertyChanged(nameof(Categories));
            }
        }
        [Trace]
        protected virtual void Submit()
        {

        }
        [Trace]
        protected virtual void UpdateProductList()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                var products = new ObservableCollection<Product>(db.Products
                    .Include(product => product.Supplier)
                    .Include(product => product.Category)
                    .Where(product => product.CategoryId == SelectedCategoryId.Value)
                    .OrderBy(x => x.ProductId)
                    );
                Products = products.ToReadOnlyReactiveCollection(product => product);

                RaisePropertyChanged(nameof(Products));

            }
        }
        protected virtual void ShowEditWindow()
        {
            this.ShowEditWindowRequest.Raise(new SubWindowOpenNotification {
                ContentType = typeof(ProductEditWindow),
                Id = SelectedProductId.Value
            },
            notification => {
                _logger.Trace($" complete");
            });

        }
    }
}
