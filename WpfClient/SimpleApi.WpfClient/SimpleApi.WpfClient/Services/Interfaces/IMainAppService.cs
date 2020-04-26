using SimpleApi.WpfClient.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IMainAppService : IAppService
    {
        void Run();
        Task<(bool, string)> SendMessageAsync(string message);
        Task<(bool, string)> SendMessageAsync(Note note);
    }
}
