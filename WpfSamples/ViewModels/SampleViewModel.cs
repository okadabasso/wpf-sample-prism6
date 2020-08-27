using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfSamples.ComponentManagement.Attributes;
using WpfSamples.Services;

namespace WpfSamples.ViewModels
{
    public class SampleViewModel : BindableBase
    {
        private ILogger logger;
        public string Title { get; set; } = "Sample View";
        public string Message { get; set; } = "hello " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        private readonly FooService fooService;
        public SampleViewModel(ILogger logger, FooService service)
        {
            this.logger = logger;
            logger.Trace("start");

            this.fooService = service;
            Foo();
        }
        [Trace]
        public virtual void Foo()
        {
            fooService.Send();
        }
    }
}
