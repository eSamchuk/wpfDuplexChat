using System;
using Server;
using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel;
using Client.ServiceReference1;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace Client
{
    public class ClientHandler :IChatServiceCallback, INotifyPropertyChanged
    {
        static InstanceContext site = new InstanceContext(new ClientHandler());
        public static ChatServiceClient proxy = new ChatServiceClient(site);

        private ChatUser _clientUser;
        public ChatUser ClientUser
        {
            get { return _clientUser; }
            set
            {
                if (_clientUser == value) return;
                _clientUser = value;
                OnPropertyChanged();
            }
        }

        private ChatMessage _mess;
        public ChatMessage Message
        {
            get { return _mess; }
            set
            {
                if (_mess == value) return;
                _mess = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ChatMessage> _messHistory;

        public ObservableCollection<ChatMessage> messHistory
        {
            get
            {
                return _messHistory;
            }
            set
            {
                if (_messHistory == value) return;
                _messHistory = value;
                OnPropertyChanged("messHistory");
            }
        }

        private ObservableCollection<ChatUser> _users;

        public ObservableCollection<ChatUser> Users
        {
            get { return _users; }
            set
            {
                if (_users == value) return;
                _users = value;
                OnPropertyChanged();
            }
        }

        private bool _isConnected;
        public bool isConnected
        {
            get { return _isConnected; }
            set
            {
                if (_isConnected == value) return;
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private relayCommand _connect;

        public relayCommand ConnectCommand
        {
            get { return _connect; }
            set
            {
                if (_connect == value) return;
                _connect = value;
                OnPropertyChanged();
            }
        }


        private relayCommand _send;

        public relayCommand SendMessage
        {
            get { return _send; }
            set
            {
                if (_send == value) return;
                _send = value;
                OnPropertyChanged();
            }
        }

        public ClientHandler()
        {
            this.ClientUser = new ChatUser();
            this.Message = new ChatMessage();
            this.Users = new ObservableCollection<ChatUser>();
            this.messHistory = new ObservableCollection<ChatMessage>();
            ConnectCommand = new relayCommand(param => this.Connect());
            SendMessage = new relayCommand(param => this.sendNewMessage());

            messHistory.Add(new ChatMessage() { Message = "Hello!" });
        }

        public void Connect()
        {
            try
            {
                ChatUser tmp = proxy.ClientConnect(ClientUser);

                if (tmp != null)
                {
                    isConnected = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message.ToString());
            }
        }

        public void sendNewMessage()
        {
            ChatMessage newMessage = new ChatMessage()
            {
                Date = DateTime.Now,
                Message = this.Message.Message,
                User = this.ClientUser
            };

            proxy.sendNewMessage(newMessage);
        }


        public void newUserConnected(ChatUser user)
        {
            Users.Add(user);
        }

        public void deliverNewMessage(ChatMessage message)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                messHistory.Add(message);
            }));
        }

        #region PropertyChangedMembers
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void sendNewMEssage(ChatMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
