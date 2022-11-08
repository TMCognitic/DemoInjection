using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.IOC;

namespace Tools.Locator
{
    public abstract class LocatorBase
    {
        private readonly ISimpleIOC _container;
        protected ISimpleIOC Container
        {
            get
            {
                return _container;
            }
        }

        protected LocatorBase() : this(new SimpleIOC())
        {
           
        }

        protected LocatorBase(ISimpleIOC container)
        {
            _container = container;
            ConfigureService(_container);
        }


        protected abstract void ConfigureService(ISimpleIOC container);
    }
}
