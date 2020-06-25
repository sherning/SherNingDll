using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // Experimenting with List, Dictionary and Tuple Class
    class Container
    {
        // List with tuple vs Dictionary.
        public static void Main()
        {
            Console.WriteLine("Using List with Tuple combo to store reference types and a key");
            List<Tuple<int,IHotDrinks>> hotDrinks = new List<Tuple<int, IHotDrinks>>();
            hotDrinks.Add(Tuple.Create(1, new Tea("Ice Tea") as IHotDrinks));
            hotDrinks.Add(Tuple.Create(2, (IHotDrinks)new Coffee("Mocha")));
            hotDrinks.Add(Tuple.Create(3, (IHotDrinks)new HotChocolate("Mashmallo Chocolate")));
            hotDrinks.Add(Tuple.Create(4, (IHotDrinks)new Tea("Earl Grey")));
            hotDrinks.Add(Tuple.Create(5, (IHotDrinks)new Tea("Jasmin")));

            foreach (var drink in hotDrinks)
            {
                Console.WriteLine($"No. {drink.Item1} on the list is {drink.Item2.WhatDrink}");
            }

            Console.WriteLine("\nUsing dictionary to store reference types with key");
            // IHotDrinks can be used like an enum here. 
            Dictionary<int, IHotDrinks> hotDrinks2 = new Dictionary<int, IHotDrinks>();
            hotDrinks2.Add(1, new Tea("Ice Tea"));
            hotDrinks2.Add(2, new Coffee("Mocha"));
            hotDrinks2.Add(3, new HotChocolate("Mashmallo Chocolate"));
            hotDrinks2.Add(4, new Tea("Earl Grey"));
            hotDrinks2.Add(5, new Tea("Jasmin"));

            foreach (var drink in hotDrinks2)
            {
                Console.WriteLine($"No. {drink.Key} on the list is {drink.Value.WhatDrink}");
            }

            Console.WriteLine("\nUsing reflections to find all the classes that use IHotDrinks interface.");
            List<Tuple<int, IHotDrinks>> hotDrinks3 = new List<Tuple<int, IHotDrinks>>();
            int count = 1;
            foreach (var drink in typeof(Container).Assembly.GetTypes())
            {
                // Check if the class does use IHotDrinks interface and is a concrete class and not a interface.
                if (typeof(IHotDrinks).IsAssignableFrom(drink) && drink.IsInterface == false)
                {
                    // Create an instance of an unknown type.
                    hotDrinks3.Add(Tuple.Create(count++, (IHotDrinks)Activator.CreateInstance(drink)));
                }
            }

            foreach (var drink in hotDrinks3)
            {
                Console.WriteLine($"No. {drink.Item1} on the list is {drink.Item2}");
            }
        }

        interface IHotDrinks
        {
            string WhatDrink { get; set; }
        }

        class Coffee : IHotDrinks
        {
            public Coffee()
            {

            }
            public string WhatDrink { get; set; }
            public Coffee(string whatDrink)
            {
                WhatDrink = whatDrink;
            }
        }

        class Tea : IHotDrinks
        {
            public Tea()
            {

            }
            public string WhatDrink { get; set; }
            public Tea(string whatDrink)
            {
                WhatDrink = whatDrink;
            }
        }

        class HotChocolate : IHotDrinks
        {
            public HotChocolate()
            {

            }
            public string WhatDrink { get; set; }
            public HotChocolate(string whatDrink)
            {
                WhatDrink = whatDrink;
            }
        }
    }
}
