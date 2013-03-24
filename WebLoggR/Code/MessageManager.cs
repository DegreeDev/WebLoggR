using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebLoggR.Models;

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

        private Dictionary<Guid, LogMessage> _persistedMessage;


        protected MessageManager()
        {
            _persistedMessage = new Dictionary<Guid, LogMessage>();
        }

        public IEnumerable<LogMessage> FindByApp(Guid ApiKey)
        {
            List<LogMessage> messages = new List<LogMessage>();

            //add the messages stored on the server to the specific app
            //using (var session = NHibernateHelper.Session.OpenSession())
            //{
            //    NHibernateHelper.ExecuteTransaction(session, () =>
            //    {
            //        messages.AddRange(session
            //                .CreateQuery("from LogMessage log where log.ApiKey = :apiKey")
            //                .SetParameter("apiKey", ApiKey)
            //                .List<LogMessage>());
            //    });
            //}
            using (var db = new WebLoggR.Models.Entities())
            {
                messages.AddRange(db.LogMessages.Where(x => x.ApiKey == ApiKey));
            }

            messages.AddRange(_persistedMessage.Where(x => x.Value.ApiKey == ApiKey).Select(x => x.Value));
            return messages;

        }
        public void Persist(LogMessage lm)
        {
            _persistedMessage.Add(lm.Id, lm);
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

            this.Persist(lm);

            return lm;
        }

    }
}