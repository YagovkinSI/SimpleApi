using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Http;
using Newtonsoft.Json;
using SimpleApi.WpfClient.Services.Interfaces;
using SimpleApi.WpfClient.Services;

namespace SimpleApi.WpfClient.Host
{
    public class ConnectionService : IConnectionService
    {
        public const string HOST_IP = "localhost";
        public const int HOST_PORT = 44304;
        public string APP_PATH => $"https://{HOST_IP}:{HOST_PORT}";

        public async Task<bool> PingHost()
        {
            var pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                var reply = await pinger.SendPingAsync(HOST_IP);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                pinger?.Dispose();
            }

            return pingable;
        }

        public async Task<AppActionResult> SendMessage(string message)
        {
            var note = JsonConvert.SerializeObject(new { Message = message });
            var content = new StringContent(note, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response =
                        await client.PostAsync(APP_PATH + "/api/notes", content);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var result = await response.Content.ReadAsAsync<AppActionResult>();
                            if (result.Success)
                                return result;
                            else
                                return new AppActionResult(false,
                                    $"Ошибка сохранения сообщения в северную БД.\r\t{result.Error}\r" +
                                    $"\tПовторная попытка отправки будет произведена позднее автоматически.");
                        }
                        catch
                        {
                            return new AppActionResult(false,
                                $"Сервер вернул неизвестный формат результата операции:\r\t{response.Content}\r" +
                                $"\tПовторная попытка отправки будет произведена позднее автоматически.");
                        }
                    }
                    else
                    {
                        return new AppActionResult(false,
                            $"Ошибка запроса: \r\t{response.Content}\r" +
                            $"\tПовторная попытка отправки будет произведена позднее автоматически.");
                    }
                }
            }
            catch (Exception ex)
            {
                return new AppActionResult(false,
                    $"Ошибка приложения! Сообщение не отправлено!\r\t{ex.InnerException?.Message ?? ex.Message}\r" +
                    $"\tПовторная попытка отправки будет произведена позднее автоматически.");
            }          
        }
    }
}
