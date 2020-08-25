using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfSamples.ViewModels
{
    public class SampleViewModel : BindableBase
    {
        public string Title { get; set; } = "Sample View";
        public string Message { get; set; } = "hello hello junk world";
        public string Now { get; set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        public SampleViewModel()
        {

        }
    }
}
