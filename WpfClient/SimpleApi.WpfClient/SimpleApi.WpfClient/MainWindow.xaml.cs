using SimpleApi.WpfClient.DAL;
using SimpleApi.WpfClient.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SimpleApi.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void onHostCheckClick(object sender, RoutedEventArgs e)
        {
            lHostConnect.Foreground = Brushes.Black;
            lHostConnect.Content = "Проверка соединения...";
            var ping = await HostHelper.PingHost();
            lHostConnect.Content = ping ? "Соединение установлено" : "Нет соединения";
            lHostConnect.Foreground = ping ? Brushes.Green : Brushes.Red;
        }

        private async void onSendClick(object sender, RoutedEventArgs e)
        {
            var message = tbMessage.Text;
                        
            var noteId = await DbHelper.AddNote(message);
            if (!noteId.HasValue)
                return;

            (bool success, string response) = await HostHelper.SendMessage(message);
            DbHelper.AddSending(noteId.Value, success, response);
            tbLog.Text += response + Environment.NewLine;
        }
    }
}
