using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.WpfClient.Services
{
    public static class ServiceManager
    {
        private static List<IAppService> services = new List<IAppService>();

        public static T GetService<T>()
        {
            var service = services.FirstOrDefault(s => typeof(T) == s.GetType());
            return (T)service;
        }

        public static void SetService(IAppService service)
        {
            services.Add(service);
        }

    }
}
