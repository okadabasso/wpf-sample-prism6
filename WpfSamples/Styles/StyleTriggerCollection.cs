using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSamples.Styles
{
    public class StyleTriggerCollection : FreezableCollection<System.Windows.Interactivity.TriggerBase>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StyleTriggerCollection();
        }
    }
}
