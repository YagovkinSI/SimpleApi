using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Http;
using SimpleApi.WpfClient.Host.ResponseModels;
using Newtonsoft.Json;

namespace SimpleApi.WpfClient.Host
{
    public static class HostHelper
    {
        public const string HOST_IP = "localhost";
        public const int HOST_PORT = 44304;
        public static string APP_PATH => $"https://{HOST_IP}:{HOST_PORT}";

        public async static Task<bool> PingHost()
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

        public async static Task<(bool,string)> SendMessage(string message)
        {
            var note = JsonConvert.SerializeObject(new { Message = message });
            var content = new StringContent(note, Encoding.UTF8, "application/json");

            bool success;
            string result;
            try
            {
                using (var client = new HttpClient())
                {
                    var response =
                        await client.PostAsync(APP_PATH + "/api/notes", content);
                    if (response.IsSuccessStatusCode)
                    {
                        ResponseNotePost responseNote;
                        try
                        {
                            responseNote = await response.Content.ReadAsAsync<ResponseNotePost>();
                            success = responseNote.Success;
                            result = responseNote.Success
                                ? "Сообщение отправлено и сохранено"
                                : $"Ошибка сохранения сообщения в базу данных: {responseNote.Error}";
                        }
                        catch
                        {
                            success = false;
                            result = $"Ошибка клиента (сервер вернул неизвестный формат): {response.Content}";
                        }
                    }
                    else
                    {
                        success = false;
                        result = $"Ошибка запроса: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                result = ex.InnerException?.Message ?? ex.Message;
            }

            return (success, result);            
        }
    }
}
