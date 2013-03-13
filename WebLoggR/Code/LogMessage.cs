using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLoggR.Code
{
    public class LogMessage
    {
        public Guid Id { get; set; }
        public Guid ApiKey { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

    }
}