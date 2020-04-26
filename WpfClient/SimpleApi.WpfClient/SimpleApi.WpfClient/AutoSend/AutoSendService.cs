using SimpleApi.WpfClient.Services.Interfaces;
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
    public class AutoSendService : IAutoSendService
    {
        public bool isAutoSendOn;

        private string message = "Автоматическая отправка ранее не отправленных писем..." + Environment.NewLine;

        public void Run(AutoSendObject autoSendObject)
        {
            if (isAutoSendOn)
                return;

            isAutoSendOn = true;
            var thread = new Thread(AutoSend) { IsBackground = true };
            thread.Start(autoSendObject);

        }

        private void AutoSend(object obj)
        {
            var autoSendObject = (AutoSendObject)obj;
            while(isAutoSendOn)
            {
                autoSendObject.Dispatcher.Invoke(() => { autoSendObject.TbLog.Text += message; });
                Thread.Sleep(10000);
            }
        }

    }
}
