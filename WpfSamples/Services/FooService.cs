using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;

namespace WpfSamples.Services
{
    [DependencyObject(Transactional = true)]
    public class FooService : IDisposable
    {
        private readonly ILogger logger;
        public void Dispose()
        {
            logger.Trace("disposed");
        }

        public FooService(ILogger logger)
        {
            this.logger = logger;
        }
        [Trace]
        public virtual string Send()
        {
            return nameof(FooService);
        }
    }
}
