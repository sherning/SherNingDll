using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // C# Design Patterns - Factory Method
    // https://code-maze.com/factory-method/

    class FactoryMethod
    {
        public static void Main()
        {
            Aircon_Client();
            BMW_Client();
            HotDrinks_Client();
        }

        #region Abstract Method

        public static void HotDrinks_Client()
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
        }
        interface IHotDrink
        {
            void Consume();
        }

        class Tea : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This tea is piping hot and nice !");
            }
        }

        class Coffee : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This coffee is sensational");
            }
        }
        interface IHotDrinkFactory
        {
            // returns an object which implements this interface.
            IHotDrink Prepare(int amount);
        }

        class TeaFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Putting the tea bag into {amount}ml of water.");
                return new Tea();
            }
        }

        class CoffeeFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Grounding the beans and mixing {amount}ml of water.");
                return new Coffee();
            }
        }
        /// <summary>
        /// You should not modify this class when shipped out. The only way to add any changes
        /// is to use reflections to find the types that uses the particular interface.
        /// </summary>
        class HotDrinkMachine
        {
            // Either use a dictionary or list with tuple which ever is my preference.
            private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

            // Use reflections to detect the type.
            public HotDrinkMachine()
            {
                foreach (var item in typeof(HotDrinkMachine).Assembly.GetTypes())
                {
                    if (typeof(IHotDrinkFactory).IsAssignableFrom(item) && item.IsInterface == false)
                    {
                        factories.Add(Tuple.Create(item.Name.Replace("Factory", string.Empty),
                            (IHotDrinkFactory)Activator.CreateInstance(item)));
                    }
                }
            }

            public IHotDrink MakeDrink()
            {
                Console.WriteLine("Available Drinks: ");
                for (int i = 0; i < factories.Count; i++)
                {
                    var tuple = factories[i];
                    Console.WriteLine($"{i}: {tuple.Item1}");
                }

                while (true)
                {
                    string s;

                    if ((s = Console.ReadLine()) != null
                        && int.TryParse(s, out int i)
                        && i >= 0
                        && i < factories.Count)
                    {
                        Console.WriteLine("Specify amount: ");
                        s = Console.ReadLine();

                        if (s != null
                            && int.TryParse(s,out int amount)
                            && amount > 0)
                        {
                            // instead of passing through the constructor, you a factory method.
                            return factories[i].Item2.Prepare(amount);
                        }
                    }

                    Console.WriteLine("Incorrect input, try again!");
                }
            }
        }

        #endregion

        #region BMW Factory
        public static void BMW_Client()
        {
            BMWFactory
                .NewBMW_Z4
                .AppleCarPlay()
                .Voom();


            BMWFactory
                .NewBMW_X4
                .AppleCarPlay()
                .Voom();
                
        }
        /// <summary>
        /// Factory class should not mutate state.
        /// </summary>
        static class BMWFactory
        {
            // New BMW Z4
            // This is a property. Will be re-instatiated everytime its called.
            // Cause it returns a new object.
            public static IBMW_Media NewBMW_Z4 => new BMW_Z4();

            // This is a field, only instatiated once only.
            // Can be accessed everywhere.
            public static IBMW_Media NewBMW__Z4 = new BMW_Z4();

            // New BMW X6
            public static IBMW_Media NewBMW_X4 => new BMW_X4();

        }

        interface IBMWCar
        {
            string CarModel { get; set; }
            int TopSpeed { get; set; }
            IBMWCar Voom();
        }

        interface IBMW_Media : IBMWCar
        {
            IBMWCar AppleCarPlay();
        }
        abstract class BMW : IBMWCar
        {
            public string CarModel { get; set; }
            public int TopSpeed { get; set; }

            public IBMWCar Voom()
            {
                Console.WriteLine($"This BMW {CarModel} has a top speed of {TopSpeed} km/h");
                return this;
            }
        }

        class BMW_Z4 : BMW, IBMW_Media
        {
            public BMW_Z4()
            {
                CarModel = "Z4";
                TopSpeed = 200;
            }

            public IBMWCar AppleCarPlay()
            {
                Console.WriteLine($"BMW {CarModel} has Harman Kardon Sound system");
                return this;
            }
        }

        class BMW_X4 : BMW, IBMW_Media
        {
            public BMW_X4()
            {
                CarModel = "X4";
                TopSpeed = 150;
            }

            public IBMWCar AppleCarPlay()
            {
                Console.WriteLine($"BMW {CarModel} has lousy hi-Fi system.");
                return this;
            }
        }

        #endregion

        #region Aircon Factory Demo
        /// <summary>
        /// Factory method is a creational design pattern that provides an interface for
        /// Creating objects without specifying their concrete classes.
        /// Use a method to create an object instead of constructor.
        /// </summary>
        public static void Aircon_Client()
        {
            // Client uses Factory to Create Products.
            // Creation patterns to create objects.

            AirConditioner // Factory
                .InitializeFactories()
                .ExecuteCreation(Actions.Cooling, 22.5) // Create object
                .Operate(); // use the object created.
        }
        // Factory Method Implementation
        // Client does not know about the concrete class.
        interface IAirConditioner
        {
            void Operate();
        }

        class Cooling : IAirConditioner
        {
            private readonly double Temperature;
            public Cooling(double temperature)
            {
                Temperature = temperature;
            }
            public void Operate()
            {
                Console.WriteLine($"Cooling the room to the required " +
                    $"temperature of {Temperature} degrees");
            }
        }

        class Warming : IAirConditioner
        {
            private readonly double Temperature;
            public Warming(double temperature)
            {
                Temperature = temperature;
            }
            public void Operate()
            {
                Console.WriteLine($"Warming the room to the required " +
                   $"temperature of {Temperature} degrees");
            }
        }

        /// <summary>
        /// Abstract classes are static and sealed, abstract class cannot inherit nor instantiate.
        /// </summary>
        abstract class AirConditionerFactory
        {
            public abstract IAirConditioner Create(double temperature);
        }

        /// <summary>
        /// Concrete class for implementation of abstract class
        /// </summary>
        class CoolingFactory : AirConditionerFactory
        {
            public override IAirConditioner Create(double temperature)
            {
                return new Cooling(temperature);
            }
        }

        class WarmingFactory : AirConditionerFactory
        {
            // Because Warming and Cooling uses IAirConditioner interface
            // that's why you can return warming or cooling.
            public override IAirConditioner Create(double temperature)
            {
                return new Warming(temperature);
            }
        }

        // Factory Execution
        enum Actions
        {
            Cooling, Warming
        }

        class AirConditioner
        {
            private readonly Dictionary<Actions, AirConditionerFactory> Factories;
            public AirConditioner()
            {
                // Use a dictionary to store actions instead of a switch statement.
                Factories = new Dictionary<Actions, AirConditionerFactory>
                {
                    { Actions.Cooling, new CoolingFactory() },
                    { Actions.Warming, new WarmingFactory() },
                };
            }

            /// <summary>
            /// Method to initialise the object. This is called Factory Method.
            /// </summary>
            /// <returns></returns>
            public static AirConditioner InitializeFactories() => new AirConditioner();

            public IAirConditioner ExecuteCreation(Actions action, double temperature) 
                => Factories[action].Create(temperature);
        }

        // Using the factory method refactoring technique
        // We can use Factor Method to replace the constructor while creating an object.

        // Conclusion

    }
    #endregion
}
