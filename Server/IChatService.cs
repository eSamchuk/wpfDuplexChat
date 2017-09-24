using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISendToClient))]
    interface IChatService
    {
        [OperationContract]
        ChatUser ClientConnect(ChatUser userName);
        [OperationContract]
        void RemoveUser(ChatUser user);
        [OperationContract(IsOneWay = true)]
        void sendNewMessage(ChatMessage message);
    }
    interface ISendToClient
    {
        [OperationContract]
        void newUserConnected(ChatUser user);
        [OperationContract(IsOneWay = true)]
        void deliverNewMessage(ChatMessage message);

    }
}
