using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [DataContract]
    public class ChatUser
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public string HostName { get; set; }
        public override string ToString()
        {
            return UserName;
        }
    }
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        public ChatUser User { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return $"{User} says: {Message}";
        }
    }
}
