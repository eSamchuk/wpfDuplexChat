using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ChatService : IChatService, INotifyPropertyChanged
    {
        public ChatService()
        {
            conectedUsers = new ObservableCollection<ChatUser>();
        }

        ISendToClient callback;

        private ObservableCollection<ChatUser> _conectedUsers;

        public ObservableCollection<ChatUser> conectedUsers
        {
            get { return _conectedUsers; }
            set
            {
                if (_conectedUsers == value) return;
                _conectedUsers = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, List<ChatMessage>> incomingMessages = new Dictionary<string, List<ChatMessage>>();
    
        public ChatUser ClientConnect(ChatUser user)
        {
            var exists =
               from ChatUser e in this.conectedUsers
               where e.UserName == user.UserName
               select e;

            if (exists.Count() == 0)
            {
                this.conectedUsers.Add(user);
                return user;
            }
            else return null;
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
       
        public void sendNewMessage(ChatMessage newMessage)
        {
            callback = OperationContext.Current.GetCallbackChannel<ISendToClient>();
            callback.deliverNewMessage(newMessage);
        }
      
        public ObservableCollection<ChatUser> GetAllUsers()
        {
            return conectedUsers;
        }

        public void RemoveUser(ChatUser user)
        {
            var t = conectedUsers.First(u => u.UserName == user.UserName);
            this.conectedUsers.Remove(t);
        }


       #region PropertyChangedMembers
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
