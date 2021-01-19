using System.Windows;
using System.Windows.Interactivity;

namespace WpfSamples.Infrastructure.Presentation.Styles
{
    /// <summary>
    /// StyleでBehaviorを設定するために使用するコレクション
    /// </summary>
    public class StyleBehaviorCollection : FreezableCollection<Behavior>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StyleBehaviorCollection();
        }
    }
}
