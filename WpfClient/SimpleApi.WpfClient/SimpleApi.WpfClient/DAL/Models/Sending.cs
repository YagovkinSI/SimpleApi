using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.DAL.Models
{
    public class Sending
    {
        public Guid Id { get; set; }
        public long NoteId { get; set; }
        public Note Note { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public DateTime SendDate { get; set; }
    }
}
