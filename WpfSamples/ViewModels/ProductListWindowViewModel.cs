using Autofac;
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
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;

namespace WpfSamples.ViewModels
{
    public class ProductListWindowViewModel : BindableBase
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        public ReactiveCommand ShowCreateWindowCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand ShowEditWindowCommand { get; set; } = new ReactiveCommand();
        public InteractionRequest<Notification> ShowEditWindowRequest{ get; set; } = new InteractionRequest<Notification>();

        public ReadOnlyReactiveCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ReactiveProperty<int> SelectedCategoryId { get; set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int?> SelectedProductId { get; set; } = new ReactiveProperty<int?>();
        public ReactiveProperty<int?> IsEditable { get; set; } = new ReactiveProperty<int?>();

        private ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();
        public ProductListWindowViewModel(IContainer container, ILogger<ProductListWindowViewModel> logger)
        {
            _container = container;
            _logger = logger;
            _logger.BlockTrace(() =>
            {
                LoadedCommand = new DelegateCommand(OnLoaded);
                SubmitCommand = new DelegateCommand(Submit);

                ShowCreateWindowCommand.Subscribe(ShowCreateWindow);
                ShowEditWindowCommand = SelectedProductId.Select(x => x.HasValue && x.Value != 0).ToReactiveCommand();
                ShowEditWindowCommand.Subscribe(ShowEditWindow);

                Products = products.ToReadOnlyReactiveCollection();
            });
        }
        protected void OnLoaded()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var db = scope.Resolve<NorthwindDbContext>();
                products.Clear();
                products.AddRange(db.Products
                    .Include(product => product.Supplier)
                    .Include(product => product.Category)
                    .OrderBy(x => x.ProductId)
                    .AsNoTracking()
                    .ToList()
                    );

                Categories.Clear();
                Categories.AddRange(db.Categories
                    .OrderBy(x => x.CategoryId)
                    .AsNoTracking());
            }
            SelectedCategoryId.Subscribe(value =>
            {
                SelectedProductId.Value = null;
                UpdateProductList();
            });
            
        }
        protected void Submit()
        {

        }
        protected void UpdateProductList()
        {
            if(SelectedCategoryId.Value != 0)
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var db = scope.Resolve<NorthwindDbContext>();
                    products.Clear();
                    products.AddRange(db.Products
                        .Include(product => product.Supplier)
                        .Include(product => product.Category)
                        .Where(x => x.CategoryId == SelectedCategoryId.Value)
                        .OrderBy(x => x.ProductId)
                        .AsNoTracking()
                        .ToList()
                        );

                }

            }
        }
        protected virtual void ShowCreateWindow()
        {
            this.ShowEditWindowRequest.Raise(new SubWindowOpenNotification {
                ContentType = typeof(ProductEditWindow),
                Id = 0
            },
            notification => {
                UpdateProductList();
                SelectedProductId.Value = (notification as SubWindowOpenNotification).Id;
                _logger.LogTrace($" complete");
            });

        }
        protected virtual void ShowEditWindow()
        {
            this.ShowEditWindowRequest.Raise(new SubWindowOpenNotification {
                ContentType = typeof(ProductEditWindow),
                Id = SelectedProductId.Value ?? 0
            },
            notification => {
                UpdateProductList();
                SelectedProductId.Value = (notification as SubWindowOpenNotification).Id;
                _logger.LogTrace($" complete");
            });

        }
    }
}
