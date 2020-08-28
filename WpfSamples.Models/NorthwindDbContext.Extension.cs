using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Models
{
    public partial class NorthwindDbContext
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            LogManager.GetCurrentClassLogger().Trace("dispose");
        }
    }
}
