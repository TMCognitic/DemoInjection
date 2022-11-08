using DemoInjection.Models;
using System.Linq;
using System.Reflection;

namespace DemoInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IService1 service1 = Locator.Instance.Service1;

            foreach (Type item in GetTypes())
            {
                Console.WriteLine(item.Name);
            }
        }



        static IEnumerable<Type> GetTypes()
        {
            return Assembly.GetEntryAssembly()!.GetTypes()
                .Union(Assembly.GetEntryAssembly()!.GetReferencedAssemblies().SelectMany(an => Assembly.Load(an).GetTypes())).OrderBy(t => t.Name);
        }
    }
}