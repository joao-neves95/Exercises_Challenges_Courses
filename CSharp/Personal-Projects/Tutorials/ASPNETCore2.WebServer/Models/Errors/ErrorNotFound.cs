using System.Collections.Generic;

namespace WebServer.Models
{
    public class ErrorNotFound
    {
        private const string ERROR = "Not Found";
        public string Error
        {
            get { return ERROR; }
        }
    }
}
