using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services
{
    public class AppActionResult
    {
        public bool Success { get; }
        public string Error { get; }

        public AppActionResult(bool success, string error = "")
        {
            Success = success;
            Error = error;
        }
    }
}
