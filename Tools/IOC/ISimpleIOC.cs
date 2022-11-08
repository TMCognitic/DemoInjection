using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.IOC
{
    public interface ISimpleIOC
    {
        void Register<TResource>();
        void Register<TResource>(Func<TResource> builder);

        void Register<TResource, TConcrete>()
            where TConcrete : TResource;
        void Register<TResource, TConcrete>(Func<TConcrete> builder)
            where TConcrete : TResource;

        TResource Get<TResource>();
    }
}
