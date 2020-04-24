using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.DAL.Models
{
    public class Note
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Sending> Sendings { get; set; }
    }
}
