using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Tools.IOC
{
    internal class SimpleIOC : ISimpleIOC
    {
        private readonly IDictionary<Type, object?> _instances;
        private readonly IDictionary<Type, Type> _mappers;
        private readonly IDictionary<Type, Func<object>> _builders;

        public SimpleIOC()
        {
            _instances = new Dictionary<Type, object?>();
            _mappers = new Dictionary<Type, Type>();
            _builders = new Dictionary<Type, Func<object>>();
        }

        public TResource Get<TResource>()
        {
            return (TResource)Get(typeof(TResource));
        }

        public object Get(Type resourceType)
        {
            if (!_instances.ContainsKey(resourceType))
            {
                throw new InvalidOperationException("Resource not registed!");
            }

            return (_instances[resourceType] ??= Resolve(resourceType));
        }

        private object Resolve(Type type)
        {
            Type resourceType = type;

            if (_builders.ContainsKey(resourceType))
            {
                return _builders[resourceType].Invoke();
            }

            if(_mappers.ContainsKey(resourceType))
            {
                resourceType = _mappers[resourceType];
            }

            ConstructorInfo? constructorInfo = resourceType.GetConstructors().SingleOrDefault();

            if(constructorInfo is not null)
            {
                object[] paremeters = constructorInfo.GetParameters()
                                                     .Select(p => Get(p.ParameterType))
                                                     .ToArray();

                return constructorInfo.Invoke(paremeters);
            }

            PropertyInfo? propertyInfo = resourceType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if(propertyInfo is not null)
            {
                if(propertyInfo.GetMethod is null)
                {
                    throw new InvalidOperationException($"The property Instance must have a public get!");
                }

                return propertyInfo.GetMethod.Invoke(null, null)!;
            }

            FieldInfo? fieldInfo = resourceType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);

            if (fieldInfo is not null)
            {
                if (fieldInfo is null)
                {
                    throw new InvalidOperationException($"The property Instance must have a public get!");
                }

                return fieldInfo.GetValue(null)!;
            }

            throw new InvalidOperationException($"Can't resolve the type {type.FullName}!");
        }

        public void Register<TResource>()
        {
            _instances.Add(typeof(TResource), null);
        }

        public void Register<TResource>(Func<TResource> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            Register<TResource>();
            _builders.Add(typeof(TResource), () => builder()!);
        }

        public void Register<TResource, TConcrete>() where TConcrete : TResource
        {
            Register<TResource>();
            _mappers.Add(typeof(TResource), typeof(TConcrete));
        }

        public void Register<TResource, TConcrete>(Func<TConcrete> builder) where TConcrete : TResource
        {
            ArgumentNullException.ThrowIfNull(builder);

            Register<TResource>();
            _builders.Add(typeof(TResource), () => builder()!);
        }
    }
}
