using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInjection.Models
{
    public sealed class Service2 : IService2
    {
        private readonly IService1 service1;

        public Service2(IService1 service1)
        {
            this.service1 = service1;
        }

        public void M2()
        {

        }
    }
}
