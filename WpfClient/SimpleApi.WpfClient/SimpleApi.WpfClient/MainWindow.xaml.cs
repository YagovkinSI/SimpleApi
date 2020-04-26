using SimpleApi.WpfClient.AutoSend;
using SimpleApi.WpfClient.BLL;
using SimpleApi.WpfClient.DAL;
using SimpleApi.WpfClient.Enums;
using SimpleApi.WpfClient.Host;
using SimpleApi.WpfClient.Logger;
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
        private IDatabaseService databaseService;
        private IConnectionService connectionService;
        private IAutoSendService autoSendService;
        private IMainAppService mainAppService;
        private ILogService logService;

        public MainWindow()
        {
            InitializeComponent();

            CreateServices();

            mainAppService.Run();
        }

        private void CreateServices()
        {
            databaseService = new DatabaseService();
            ServiceManager.SetService(databaseService);

            connectionService = new ConnectionService();
            ServiceManager.SetService(connectionService);

            autoSendService = new AutoSendService();
            ServiceManager.SetService(autoSendService);
            autoSendService.Init(Dispatcher);

            mainAppService = new MainAppService();
            ServiceManager.SetService(mainAppService);

            logService = new LogService(tbLog);
            ServiceManager.SetService(logService);
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

            (var success, var response) = await mainAppService.SendMessageAsync(message);

            logService.AddLog(response, enLogType.Message);
        }
    }
}
