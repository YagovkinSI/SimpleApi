using SimpleApi.WpfClient.AutoSend;
using SimpleApi.WpfClient.DAL;
using SimpleApi.WpfClient.Host;
using SimpleApi.WpfClient.Services;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SimpleApi.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDatabaseService databaseService = new DatabaseService();
        IConnectionService connectionService = new ConnectionService();
        IAutoSendService autoSendService = new AutoSendService();

        public MainWindow()
        {
            InitializeComponent();

            Generateservices();

            autoSendService.Run(new AutoSendObject(this.Dispatcher, tbLog));
        }
        private void Generateservices()
        {
            ServiceManager.SetService(databaseService);
            ServiceManager.SetService(connectionService);
            ServiceManager.SetService(connectionService);
        }


        private async void onHostCheckClick(object sender, RoutedEventArgs e)
        {
            lHostConnect.Foreground = Brushes.Black;
            lHostConnect.Content = "Проверка соединения...";
            var ping = await connectionService.PingHost();
            lHostConnect.Content = ping ? "Соединение установлено" : "Нет соединения";
            lHostConnect.Foreground = ping ? Brushes.Green : Brushes.Red;
        }

        private async void onSendClick(object sender, RoutedEventArgs e)
        {
            var message = tbMessage.Text;

            var noteId = await databaseService.AddNote(message);
            if (!noteId.HasValue)
                return;

            (bool success, string response) = await connectionService.SendMessage(message);
            databaseService.AddSending(noteId.Value, success, response);
            tbLog.Text += response + Environment.NewLine;
        }
    }
}
