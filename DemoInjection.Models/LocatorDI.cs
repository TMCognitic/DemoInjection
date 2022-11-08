using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Locator;

namespace DemoInjection.Models
{
    public class LocatorDI : LocatorBaseDI
    {
        private static LocatorDI? _instance;

        public static LocatorDI Instance
        {
            get
            {
                return _instance ??= new LocatorDI();
            }
        }

        public IService1 Service1
        {
            get
            {
                return Container.GetService<IService1>()!;
            }
        }

        public IService2 Service2
        {
            get
            {
                return Container.GetService<IService2>()!;
            }
        }

        protected override void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<IService1, Service1>();
            services.AddSingleton<IService2, Service2>();
        }
    }
}
