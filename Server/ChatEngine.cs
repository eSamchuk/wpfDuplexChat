using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ChatEngine
    {
        private List<ChatUser> conectedUsers = new List<ChatUser>();
        private Dictionary<string, List<ChatMessage>> incomingMessages = new Dictionary<string, List<ChatMessage>>();
        public List<ChatUser> ConectedUsers
        {
            get { return conectedUsers; }
        }
        public ChatUser AddNewChatUser(ChatUser user)
        {
            var exists =
                from ChatUser e in this.ConectedUsers
                where e.UserName == user.UserName
                select e;

            if (exists.Count() == 0)
            {
                this.ConectedUsers.Add(user);
                incomingMessages.Add(user.UserName, new List<ChatMessage>() {
                    new ChatMessage(){User=user,Message="Welcome to WPF simple chat",Date=DateTime.Now}
                });

                Console.WriteLine("New user connected: " + user);
                return user;
            }
            else
                return null;
        }

        public void AddNewMessage(ChatMessage newMessage)
        {
            Console.WriteLine(newMessage.User.UserName + " says :" + newMessage.Message + " at " + newMessage.Date);

            //se envian mensajes a todos los usuarios conectados menos al que lo envia
            foreach (var user in this.ConectedUsers)
            {
                if (!newMessage.User.UserName.Equals(user.UserName))
                {
                    incomingMessages[user.UserName].Add(newMessage);
                }
            }
        }
        public List<ChatMessage> GetNewMessages(ChatUser user)
        {
           
            List<ChatMessage> myNewMessages = incomingMessages[user.UserName];
          
            incomingMessages[user.UserName] = new List<ChatMessage>();

            if (myNewMessages.Count > 0)
                return myNewMessages;
            else
                return null;
        }

   
        public void RemoveUser(ChatUser user)
        {
            this.ConectedUsers.RemoveAll(u => u.UserName == user.UserName);
        }
    }
}
