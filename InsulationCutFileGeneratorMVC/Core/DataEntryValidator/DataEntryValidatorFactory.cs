using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core
{
    // following the pattern on https://softwareengineering.stackexchange.com/questions/345672/c-when-one-should-go-for-factory-method-pattern-instead-of-factory-pattern
    
    public class DataEntryValidatorFactory
    {
        public Dictionary<InsulationType, Func<IDataEntryValidator>> DataEntryValidatorCreators { get; private set; }
        public DataEntryValidatorFactory()
        {
            //Here, we use reflection and Linq to find all IShip implementations;
            //other methods to dynamically set up the dictionary exist
            DataEntryValidatorCreators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t 
                    => typeof(IDataEntryValidator).IsAssignableFrom(t) && t.IsInterface == false)
                .Select(t 
                    => new Func<IDataEntryValidator>(() 
                        => Activator.CreateInstance(t) as IDataEntryValidator))
                .ToDictionary(f => f().InsulationType);
        }

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
