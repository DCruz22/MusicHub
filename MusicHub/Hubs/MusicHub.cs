using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MusicHub.Hubs
{
    public class MusicHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}