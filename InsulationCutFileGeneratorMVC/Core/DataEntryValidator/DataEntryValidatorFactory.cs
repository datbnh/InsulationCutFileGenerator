using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InsulationCutFileGeneratorMVC.Core
{
    // following the pattern on https://softwareengineering.stackexchange.com/questions/345672/c-when-one-should-go-for-factory-method-pattern-instead-of-factory-pattern

    public class DataEntryValidatorFactory
    {
        private static readonly DataEntryValidatorFactory instance = new DataEntryValidatorFactory();
        private static Dictionary<InsulationType, Func<IDataEntryValidator>> 
            DataEntryValidatorCreators { get; set; }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DataEntryValidatorFactory()
        {

        }

        private DataEntryValidatorFactory()
        {
            Console.WriteLine("Initialising DataEntryValidatorFactory...");
            //Here, we use reflection and Linq to find all IDataEntryValidator implementations;
            //other methods to dynamically set up the dictionary exist
            DataEntryValidatorCreators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t
                    => typeof(IDataEntryValidator).IsAssignableFrom(t) && t.IsInterface == false)
                .Select(t
                    => new Func<IDataEntryValidator>(()
                        => Activator.CreateInstance(t) as IDataEntryValidator))
                .ToDictionary(f => f().InsulationType);
            Console.WriteLine("DataEntryValidatorFactory initialised.");
        }

        public static DataEntryValidatorFactory Factory => instance;

        public IDataEntryValidator GetInstance(InsulationType type)
        {
            return DataEntryValidatorCreators[type]();
        }

        public Func<IDataEntryValidator> GetFactoryMethod(InsulationType type)
        {
            return DataEntryValidatorCreators[type];
        }
    }
}