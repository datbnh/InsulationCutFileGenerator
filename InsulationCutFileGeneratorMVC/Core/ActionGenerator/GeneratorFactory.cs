using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    internal class GeneratorFactory
    {
        private static readonly GeneratorFactory instance = new GeneratorFactory();
        private static Dictionary<InsulationType, Func<IGenerator>>
            GeneratorCreators
        { get; set; }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static GeneratorFactory()
        {

        }

        private GeneratorFactory()
        {
            Console.WriteLine("Initialising GeneratorFactory...");
            //Here, we use reflection and Linq to find all IGenerator implementations;
            //other methods to dynamically set up the dictionary exist
            GeneratorCreators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t
                    => typeof(IGenerator).IsAssignableFrom(t) && t.IsInterface == false)
                .Select(t
                    => new Func<IGenerator>(()
                        => Activator.CreateInstance(t) as IGenerator))
                .ToDictionary(f => f().InsulationType);
            Console.WriteLine("GeneratorFactory initialised.");
        }

        public static GeneratorFactory Factory => instance;

        public IGenerator GetInstance(InsulationType type)
        {
            return GeneratorCreators[type]();
        }

        public Func<IGenerator> GetFactoryMethod(InsulationType type)
        {
            return GeneratorCreators[type];
        }
    }
}
