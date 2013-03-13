using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebLoggR.Code
{
    public class MessageManager
    {
        private static MessageManager _manager;
        public static MessageManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new MessageManager();
                }
                return _manager;
            }
        }

        private List<LogMessage> _persistedMessage;


        protected MessageManager()
        {
            _persistedMessage = new List<LogMessage>();
        }

        public IEnumerable<LogMessage> FindByApp(Guid ApiKey)
        {
            return _persistedMessage.Where(x => x.ApiKey == ApiKey);
        }

        public LogMessage Persist(Guid apiKey, string logLevel, string title, string message, DateTime time)
        {
            Guid messageId = Guid.NewGuid();

            LogMessage lm = new LogMessage()
            {
                Id = messageId,
                ApiKey = apiKey,
                LogLevel = logLevel,
                Title = title,
                Message = message,
                Time = time
            };

            _persistedMessage.Add(lm);

            return lm;
        }

    }
}