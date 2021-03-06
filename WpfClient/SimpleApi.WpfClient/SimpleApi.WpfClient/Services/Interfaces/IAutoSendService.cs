﻿using SimpleApi.WpfClient.AutoSend;
using SimpleApi.WpfClient.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IAutoSendService : IAppService
    {
        Task InitAsync();
        void TrySendNotes(params Note[] newNotSendedNotes);
    }
}
