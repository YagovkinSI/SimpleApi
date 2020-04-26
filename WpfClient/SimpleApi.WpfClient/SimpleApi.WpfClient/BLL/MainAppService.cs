using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Services;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.BLL
{
    public class MainAppService : IMainAppService
    {
        private IDatabaseService databaseService;
        private IConnectionService connectionService;
        private IAutoSendService autoSendService;


        public void Run()
        {
            databaseService = ServiceManager.GetService<IDatabaseService>();
            connectionService = ServiceManager.GetService<IConnectionService>();
            autoSendService = ServiceManager.GetService<IAutoSendService>();

            var notSendedNotes = databaseService.GetNotSendedNotes();
            autoSendService.Run(notSendedNotes);
        }

        public async Task<(bool, string)> SendMessageAsync(string message)
        {
            var note = new Note
            {
                CreateDate = DateTime.Now,
                Message = message
            };

            var isNoteAdded = await databaseService.AddNote(note);
            if (!isNoteAdded)
                return (false, "Ошибка базы данных! Сообщение не сохранено и не отправлено!");

            (var success, var resonse) = await SendMessageAsync(note);

            if (!success)
                autoSendService.TrySendNotes(note);

            return (success, resonse);

        }

        public async Task<(bool, string)> SendMessageAsync(Note note)
        {
            (bool success, string response) = await connectionService.SendMessage(note.Message);
            databaseService.AddSending(note.Id, success, response);

            return (success, response);
        }


    }
}
