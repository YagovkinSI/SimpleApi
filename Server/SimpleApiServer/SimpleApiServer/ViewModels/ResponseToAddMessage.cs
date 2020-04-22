using SimpleApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApiServer.ViewModels
{
    public class ResponseToAddMessage
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string SenderIp { get; set; }
        public DateTime Date { get; set; }

        public ResponseToAddMessage(string message, string senderIp, DateTime date, string error = null)
        {
            Message = message;
            SenderIp = senderIp;
            Date = date;
            Error = error ?? "";
            Success = error == null;
        }

        public ResponseToAddMessage(Note note)
        {
            Message = note.Message;
            SenderIp = note.IpAdress;
            Date = note.Date;
            Error = "";
            Success = true;
        }
    }
}
