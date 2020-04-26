using SimpleApi.WpfClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Logger
{
    public class LogElement
    {
        public DateTime Date;
        public string Message;
        public enLogType LogType;

        public LogElement(string message, enLogType logType)
        {
            Message = message;
            LogType = logType;
            Date = DateTime.Now;
        }
    }
}
