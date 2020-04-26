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
        Task<long?> AddNote(string message);
        void AddSending(long noteId, bool success, string response);
    }
}
