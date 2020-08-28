using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSamples.Notifications
{
    public class SubWindowOpenNotification : Notification
    {
        /// <summary>ID ( 主に一覧から編集画面を呼ぶ際に使用 )</summary>
        public int Id { get; set; }

        /// <summary>メッセージ ( 主に文字列を受け渡す際に使用 )</summary>
        public string Message { get; set; }

        /// <summary>呼び出すビュー</summary>
        public Type ContentType { get; set; }

        /// <summary>CallBack 引き数はIdのInt想定</summary>
        public Action<int> CallBack { get; set; }
    }
}
