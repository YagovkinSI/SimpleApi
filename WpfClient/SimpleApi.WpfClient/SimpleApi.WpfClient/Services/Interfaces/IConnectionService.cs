using SimpleApi.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IConnectionService : IAppService
    {
        Task<bool> PingHost();
        Task<(bool, string)> SendMessage(string message);
    }
}
