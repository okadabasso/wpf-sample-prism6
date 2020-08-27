using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.ComponentManagement.Attributes;

namespace WpfSamples.Services
{
    public class FooService
    {
        [Trace]
        public virtual string Send()
        {
            return nameof(FooService);
        }
    }
}
