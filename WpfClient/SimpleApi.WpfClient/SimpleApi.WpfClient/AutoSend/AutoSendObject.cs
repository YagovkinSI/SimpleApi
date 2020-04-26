using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleApi.WpfClient.AutoSend
{
    public class AutoSendObject
    {
        public Dispatcher Dispatcher;
        public ILogService LogService;
        public List<Note> NotSendedNotes;

        public AutoSendObject(Dispatcher dispatcher, ILogService logService)
        {
            Dispatcher = dispatcher;
            LogService = logService;
            NotSendedNotes = new List<Note>();
        }
    }
}
