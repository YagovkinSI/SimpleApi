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
        Task<AppActionResult> AddNote(Note message);

        Task<AppActionResult> AddSending(long noteId, AppActionResult actionResult);

        Task<Note[]> GetNotSendedNotes();
    }
}
