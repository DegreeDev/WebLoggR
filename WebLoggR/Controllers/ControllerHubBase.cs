using Microsoft.AspNet.SignalR;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace WebLoggR.Controllers
{
    public class ApiHubBase<HubType> : ApiController
        where HubType : WebLoggR.Hubs.GreenRHub
    {
        private Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<HubType>()
        );
        public IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
    public class MvcHubBase<HubType> : Controller
        where HubType : WebLoggR.Hubs.GreenRHub
    {
        private Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<HubType>()
        );
        public IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}
