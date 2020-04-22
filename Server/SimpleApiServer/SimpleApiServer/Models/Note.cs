using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApiServer.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string IpAdress { get; set; }
        public DateTime Date { get; set; }
    }
}
