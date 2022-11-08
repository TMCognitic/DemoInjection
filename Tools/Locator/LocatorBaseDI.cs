using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Locator
{
    public abstract class LocatorBaseDI
    {
        private readonly IServiceProvider _container;

        protected IServiceProvider Container
        {
            get
            {
                return _container;
            }
        }

        protected LocatorBaseDI()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureService(services);
            _container = services.BuildServiceProvider();
        }

        protected abstract void ConfigureService(IServiceCollection services);
    }
}
