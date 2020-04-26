using SimpleApi.WpfClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface ILogService : IAppService
    {
        void AddLog(string message, enLogType type);
    }
}
