using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.IOC;
using Tools.Locator;

namespace DemoInjection.Models
{
    public class Locator : LocatorBase
    {
        private static Locator? _instance;

        public static Locator Instance
        {
            get
            {
                return _instance ??= new Locator();
            }
        }

        public IService1 Service1
        {
            get
            {
                return Container.Get<IService1>();
            }
        }

        public IService2 Service2
        {
            get
            {
                return Container.Get<IService2>();
            }
        }

        protected override void ConfigureService(ISimpleIOC container)
        {
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>();
        }
    }
}
