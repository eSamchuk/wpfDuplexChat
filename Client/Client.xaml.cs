using System;
using System.Windows;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ClientHandler clHandler { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            clHandler = new ClientHandler();
            this.DataContext = clHandler;
            lst.ItemsSource = clHandler.messHistory;
        }
    }
}
