using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Services;
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
        
        private AutoSendObject autoSendObject;

        IConnectionService connectionService;
        IDatabaseService databaseService;

        public AutoSendService()
        {
            connectionService = ServiceManager.GetService<IConnectionService>();
            databaseService = ServiceManager.GetService<IDatabaseService>();
        }

        public void Init(Dispatcher dispatcher, TextBlock tbLog)
        {
            autoSendObject = new AutoSendObject(dispatcher, tbLog);
            
        }

        public void TrySendNotes(params Note[] newNotSendedNotes)
        {
            autoSendObject.NotSendedNotes.AddRange(newNotSendedNotes);

            if (isAutoSendOn || autoSendObject.NotSendedNotes.Count == 0)
                return;

            isAutoSendOn = true;
            var thread = new Thread(AutoSendAsync) { IsBackground = true };
            thread.Start(autoSendObject);
        }

        public async Task Run(Task<Note[]> newNotSendedNotes)
        {
            TrySendNotes(await newNotSendedNotes);
        }

        private void AutoSendAsync(object obj)
        {
            var autoSendObject = (AutoSendObject)obj;
            while(isAutoSendOn)
            {
                var count = 0;
                foreach(var note in autoSendObject.NotSendedNotes)
                {
                    (var success, var response) = connectionService.SendMessage(note.Message).Result;
                    databaseService.AddSending(note.Id, success, response);

                    if (!success)
                        return;
                }
                if (count > 0 && autoSendObject.NotSendedNotes.Count > 0)
                    autoSendObject.Dispatcher.Invoke(() => { autoSendObject.TbLog.Text +=
                        $"Часть ранее неотправленных сообщений успешно переданы на сервер.\r\nКоличество таких сообщений - {count}.\r\nОсталоне не передано - {autoSendObject.NotSendedNotes.Count}" + Environment.NewLine; });

                Thread.Sleep(10000);

                if (autoSendObject.NotSendedNotes.Count == 0)
                {
                    autoSendObject.Dispatcher.Invoke(() => {
                        autoSendObject.TbLog.Text +=
                            $"Все ранее неотправленные сообщения успешно переданы на сервер." + Environment.NewLine;
                    });
                    isAutoSendOn = false;
                }
            }
        }

    }
}
