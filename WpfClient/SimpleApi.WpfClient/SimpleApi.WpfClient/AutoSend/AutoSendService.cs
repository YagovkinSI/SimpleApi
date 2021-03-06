﻿using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Enums;
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

        Dispatcher dispatcher;
        private AutoSendObject autoSendObject;

        IConnectionService connectionService;
        IDatabaseService databaseService;
        ILogService logService;

        public AutoSendService(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public async Task InitAsync()
        {
            connectionService = ServiceManager.GetService<IConnectionService>();
            databaseService = ServiceManager.GetService<IDatabaseService>();
            logService = ServiceManager.GetService<ILogService>();
            
            autoSendObject = new AutoSendObject(dispatcher, logService);

            var notSendedNotes = await databaseService.GetNotSendedNotes();

            if (notSendedNotes.Length > 0)
            {
                AddLogFromAsync($"{Resource.HaveNotSendedMessage} (количество - {notSendedNotes.Length}).\r" +
                    $"\t{Resource.AutoSendOn}", enLogType.Attantion);
                TrySendNotes(notSendedNotes);
            }
            else
            {
                AddLogFromAsync($"{Resource.NoNotSendedMessage}", enLogType.Message);
            }
        }

        private void AddLogFromAsync(string message, enLogType logType)
        {
            autoSendObject.Dispatcher.Invoke(() => {
                autoSendObject.LogService.AddLog(message, logType);
            });
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

        private void AutoSendAsync(object obj)
        {
            var autoSendObject = (AutoSendObject)obj;
            while(isAutoSendOn)
            {
                var notSendedNoteCount = autoSendObject.NotSendedNotes.Count;                

                var errors = new List<string>();
                var successSendNotes = new List<Note>();
                foreach (var note in autoSendObject.NotSendedNotes)
                {
                    var resultSend = connectionService.SendMessage(note.Message).Result;
                    if (!resultSend.Success && !errors.Contains(resultSend.Error))
                        errors.Add(resultSend.Error);

                    var resultDb = databaseService.AddSending(note.Id, resultSend).Result;
                    if (!resultDb.Success && !errors.Contains(resultDb.Error))
                        errors.Add(resultDb.Error);

                    if (resultSend.Success && resultDb.Success)                    
                        successSendNotes.Add(note);                    
                }

                autoSendObject.NotSendedNotes.RemoveAll(n => successSendNotes.Contains(n));

                if (successSendNotes.Count == 0)
                {
                    var text = $"{Resource.AutoSendFail}\r";
                    foreach (var error in errors)
                        text += $"\t{error}\r";
                    AddLogFromAsync(text, enLogType.Attantion);
                }
                else
                {
                    if (notSendedNoteCount == successSendNotes.Count)
                    {
                        var text = $"{Resource.AutoSendSuccess}\r" +
                            $"\t{Resource.NoNotSendedMessage}";
                        AddLogFromAsync(text, enLogType.Attantion);
                        isAutoSendOn = false;
                    }
                    else
                    {
                        var text = $"{Resource.AutoSendPartially}\r" +
                            $"\tУспещно отправлно: {successSendNotes.Count}/{notSendedNoteCount}";
                        foreach (var error in errors)
                            text += $"\t{error}\r";
                        AddLogFromAsync(text, enLogType.Attantion);
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
