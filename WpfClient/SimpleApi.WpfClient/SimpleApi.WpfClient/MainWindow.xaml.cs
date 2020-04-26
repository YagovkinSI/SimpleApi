
using SimpleApi.WpfClient.Enums;
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
    public partial class MainWindow : Window, IAppWindow
    {
        public RichTextBox LogBox => tbLog;

        private readonly IDatabaseService databaseService;
        private readonly IConnectionService connectionService;
        private readonly ILogService logService;

        public MainWindow()
        {
            InitializeComponent();

            ApplicationManager.CreateServices(this);
            ApplicationManager.InitServices();

            databaseService = ServiceManager.GetService<IDatabaseService>();
            connectionService = ServiceManager.GetService<IConnectionService>();
            logService = ServiceManager.GetService<ILogService>();
        }

        private async void UpdateConnectionState()
        {
            lHostConnect.Foreground = Brushes.Black;
            lHostConnect.Content = "Проверка соединения...";

            var ping = await connectionService.PingHost();

            lHostConnect.Content = ping ? "Соединение установлено" : "Нет соединения";
            lHostConnect.Foreground = ping ? Brushes.Green : Brushes.Red;
        }

        private async void SendNewMassage(string message)
        {
            var result = await ApplicationManager.SendNewMassageAsync(message);
            var logText = result.Success
                ? "Сообщение успешно доставлено."
                : result.Error;
            var logType = result.Success ? enLogType.Message : enLogType.Error;
            logService.AddLog(logText, logType);
        }

        private void onHostCheckClick(object sender, RoutedEventArgs e)
        {
            UpdateConnectionState();
        }

        private async void onSendClick(object sender, RoutedEventArgs e)
        {
            var message = tbMessage.Text;
            SendNewMassage(message);

        }
    }
}
