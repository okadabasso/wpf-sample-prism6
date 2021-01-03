using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Module1.ViewModels
{
    public class SampleGridWindow1ViewModel : BindableBase
    {
        public ObservableCollection<FooItem> FooModels { get; set; }
        public SampleGridWindow1ViewModel()
        {
            var list = new List<FooItem>
            {
                new FooItem() {Id=1, Name = "item 001" },
                new FooItem() { Id = 2, Name = "item 002" },
                new FooItem() { Id = 3, Name = "item 003" },
                new FooItem() { Id = 4, Name = "item 004" },
                new FooItem() { Id = 5, Name = "item 005" }
            };
            FooModels = new ObservableCollection<FooItem>(list);
        }
    }
}
