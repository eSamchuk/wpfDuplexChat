using System;
using System.Windows;
using System.ServiceModel;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Service : Window
    {
        public ServerHandler ch { get; set; }

        public Service()
        {
            InitializeComponent();
            ch = new ServerHandler();
            this.DataContext = ch;
            lst.ItemsSource = ch.cService.conectedUsers;
        }
    }
}
