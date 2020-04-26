using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IDatabaseService : IAppService
    {
        Task<bool> AddNote(Note message);

        void AddSending(long noteId, bool success, string response);

        Task<Note[]> GetNotSendedNotes();
    }
}
