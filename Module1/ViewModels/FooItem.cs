using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1.ViewModels
{
    public class FooItem : BindableBase
    {
        private string _name;
        private int _id;

        public int Id { get => _id; set => SetProperty(ref _id , value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
    }
}
