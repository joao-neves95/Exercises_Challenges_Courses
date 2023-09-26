using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class ErrorUsernameExists
    {
        private const string ERROR = "Username already in use.";

        public string Error
        {
            get { return ERROR; }
        }

    }
}
