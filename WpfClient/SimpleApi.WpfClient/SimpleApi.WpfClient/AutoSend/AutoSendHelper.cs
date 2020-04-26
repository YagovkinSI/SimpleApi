using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleApi.WpfClient.AutoSend
{
    public static class AutoSendHelper
    {
        public static bool isAutoSendOn;

        private static string message = "Автоматическая отправка ранее не отправленных писем..." + Environment.NewLine;

        public static void Run(AutoSendObject autoSendObject)
        {
            if (isAutoSendOn)
                return;

            isAutoSendOn = true;
            var thread = new Thread(AutoSend) { IsBackground = true };
            thread.Start(autoSendObject);

        }

        private static void AutoSend(object obj)
        {
            var autoSendObject = (AutoSendObject)obj;
            while(isAutoSendOn)
            {
                autoSendObject.Dispatcher.Invoke(() => { autoSendObject.TbLog.Text += message; });
                Thread.Sleep(1000);
            }
        }

    }

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
