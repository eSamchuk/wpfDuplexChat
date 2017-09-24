using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Server
{
    public class ServerHandler
    {
        private ServiceHost host = new ServiceHost(typeof(ChatService));
        public ChatService cService { get; set; }
        public ServerHandler()
        {
            host.Open();
            cService = new ChatService();
        }
    }
}
