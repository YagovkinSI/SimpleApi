using SimpleApi.WpfClient.AutoSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IAutoSendService : IAppService
    {
        void Run(AutoSendObject autoSendObject);
    }
}
