using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebLoggR.Code;
using WebLoggR.Models;

namespace WebLoggR.Hubs
{
    [HubName("greenRHub")]
    public class GreenRHub : Hub
    {
        private Entities _db;
        public GreenRHub()
        {
            _db = new Entities();
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public async override Task OnConnected()
        {
            await base.OnConnected();
        }

        public async Task JoinGroup(Guid accountId)
        {
            List<Application> applications = _db.Applications.Where(x => x.Account == accountId).ToList();


            var data = applications.Select(application => new
            {
                Name = application.Name,
                ApiKey = application.ApiKey,
                ID = application.ID
            });

            await Clients.Caller.addApplication(data);


            await Groups.Add(Context.ConnectionId, accountId.ToString());
        }

        public async Task ServerLog(Guid apiKey, string logLevel, string title, string message)
        {
            Application app = _db.Applications.FirstOrDefault(x => x.ApiKey == apiKey);

            try
            {
                string x = null;

                string y = x.Substring(0, 100);

            }
            catch (Exception e)
            {
                title = "Tried to perform substring on null string";
                message = e.Message;
                message += e.StackTrace;
            }


            var m1 = MessageManager.Manager;
            var m2 = MessageManager.Manager;

            var test = m1 == m2; 

            LogMessage logMessage = MessageManager.Manager.Persist(apiKey, logLevel, title, message, DateTime.UtcNow);



            await Clients.Group(app.Account.ToString()).log(logMessage);
        }
    }
}