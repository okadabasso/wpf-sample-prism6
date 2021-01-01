using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Infrastructure.Presentation.Notifications
{
    public class PopupNotification : Notification
    {
        public IRegionManager PopupRegionManager { get; set; }

        /// <summary>呼び出すビュー</summary>
        public Type ContentType { get; set; }
    }
}
