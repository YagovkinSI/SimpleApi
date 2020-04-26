using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleApi.WpfClient.AutoSend
{
    public class AutoSendObject
    {
        public Dispatcher Dispatcher;
        public TextBlock TbLog;

        public AutoSendObject(Dispatcher dispatcher, TextBlock tbLog)
        {
            Dispatcher = dispatcher;
            TbLog = tbLog;
        }
    }
}
