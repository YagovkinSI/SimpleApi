using SimpleApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApiServer.ResponseModels
{
    public class ResponseNotePost
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string SenderIp { get; set; }
        public DateTime Date { get; set; }

        public ResponseNotePost(string message, string senderIp, DateTime date, string error = null)
        {
            Message = message;
            SenderIp = senderIp;
            Date = date;
            Error = error ?? "";
            Success = error == null;
        }

        public ResponseNotePost(Note note, string error = null)
                : this (note.Message, note.IpAdress, note.Date, error)
        {
        }
    }
}
