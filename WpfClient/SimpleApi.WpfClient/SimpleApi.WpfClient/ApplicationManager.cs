using SimpleApi.WpfClient.AutoSend;
using SimpleApi.WpfClient.DAL;
using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Host;
using SimpleApi.WpfClient.Logger;
using SimpleApi.WpfClient.Services;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleApi.WpfClient
{
    public static class ApplicationManager
    {
        public static void CreateServices(MainWindow window)
        {
            var databaseService = new DatabaseService();
            var connectionService = new ConnectionService();
            var autoSendService = new AutoSendService(window.Dispatcher);
            var logService = new LogService(window.LogBox);

            ServiceManager.SetService(databaseService);
            ServiceManager.SetService(connectionService);
            ServiceManager.SetService(autoSendService);
            ServiceManager.SetService(logService);
        }

        public static void InitServices()
        {
            var autoSendService = ServiceManager.GetService<IAutoSendService>();
            autoSendService.InitAsync();
        }

        public static async Task<AppActionResult> SendNewMassageAsync(string message)
        {
            var task = new SendMessageTask(message);
            await task.ExecuteAsync();
            return task.Result;
        }

        private class SendMessageTask
        {
            private readonly string message;

            private readonly IDatabaseService databaseService;
            private readonly IConnectionService connectionService;
            private readonly ILogService logService;
            private readonly IAutoSendService autoSendService;

            public AppActionResult Result { get; private set; }

            public SendMessageTask(string message)
            {
                this.message = message;

                databaseService = ServiceManager.GetService<IDatabaseService>();
                connectionService = ServiceManager.GetService<IConnectionService>();
                autoSendService = ServiceManager.GetService<IAutoSendService>();                
                logService = ServiceManager.GetService<ILogService>();
            }

            public async Task ExecuteAsync()
            {
                var note = CreateNote();

                var result = await TryAddNoteToDatabase(note);
                if (!result.Success)
                {
                    Result = result;
                    return;
                }
                                
                result = await TrySendNoteToServer(note);
                if (!result.Success)
                {
                    Result = result;
                    autoSendService.TrySendNotes(note);
                    await TryAddResultToDatabase(note, result);
                    return;
                }

                Result = await TryAddResultToDatabase(note, result);
            }

            private async Task<AppActionResult> TryAddNoteToDatabase(Note note)
            {
                return await databaseService.AddNote(note);
            }

            private async Task<AppActionResult> TrySendNoteToServer(Note note)
            {
                return await connectionService.SendMessage(note.Message);
            }

            private async Task<AppActionResult> TryAddResultToDatabase(Note note, AppActionResult result)
            {
                return await databaseService.AddSending(note.Id, result);
            }

            private Note CreateNote()
            {
                return new Note
                {
                    CreateDate = DateTime.Now,
                    Message = message
                };
            }
        }

        
    }
}
