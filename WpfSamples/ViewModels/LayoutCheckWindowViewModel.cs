using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using WpfSamples.Models;
using WpfSamples.Models.Entities;

namespace WpfSamples.ViewModels
{
	public class LayoutCheckWindowViewModel : BindableBase
	{
        public ReactiveCommand LoadedCommand { get; set; } = new ReactiveCommand();
        public ObservableCollection<Category> Categories { get; set; }
        public ReactiveProperty<Visibility> CategoryPanelVisibility { get; set; } = new ReactiveProperty<Visibility>();
        public ReactiveProperty<int> SelectedCategoryId { get; set; } = new ReactiveProperty<int>();

        public ReactiveProperty<Visibility> ProductListVisibility { get; set; } = new ReactiveProperty<Visibility>();
        public ObservableCollection<Product> Products { get; set; }

        public ReactiveCommand NavigateNext { get; set; } = new ReactiveCommand();
        public ReactiveCommand SelectAll{ get; set; } = new ReactiveCommand();
        public ReactiveCommand ExitView2Command { get; set; } = new ReactiveCommand();

        public LayoutCheckWindowViewModel()
        {
            LoadedCommand.Subscribe(()=> {
                using(var context = new NorthwindDbContext())
                {
                    Categories = new ObservableCollection<Category>(context.Categories.AsNoTracking().ToList());
                    CategoryPanelVisibility.Value = Visibility.Visible;
                    ProductListVisibility.Value = Visibility.Collapsed;

                    RaisePropertyChanged(null);
                }
            });
            NavigateNext.Subscribe(() => {
                using (var context = new NorthwindDbContext())
                {
                    Products = new ObservableCollection<Product>(context.Products.Include(x => x.Category).Include(x => x.Supplier).Where(x => x.CategoryId == SelectedCategoryId.Value).AsNoTracking().ToList());
                    CategoryPanelVisibility.Value = Visibility.Collapsed;
                    ProductListVisibility.Value = Visibility.Visible;

                    RaisePropertyChanged(null);
                }

            });
            SelectAll.Subscribe(() => {
                using (var context = new NorthwindDbContext())
                {
                    Products = new ObservableCollection<Product>(context.Products.Include(x => x.Category).Include(x => x.Supplier).AsNoTracking().ToList());
                    CategoryPanelVisibility.Value = Visibility.Collapsed;
                    ProductListVisibility.Value = Visibility.Visible;

                    RaisePropertyChanged(null);
                }

            });
            ExitView2Command.Subscribe(() => {
                CategoryPanelVisibility.Value = Visibility.Visible;
                ProductListVisibility.Value = Visibility.Collapsed;

            });
        }

    }
}
