using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class ErrorUnknown
    {
        private const string ERROR = "An unknown error occured.";

        public string Error
        {
            get { return ERROR; }
        }
    }
}
