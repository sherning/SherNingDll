using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /* Singleton Concept 
     * www.code-maze.com/singleton
     * Creational Design Pattern, to create a single instance of an object
     * ONLY one instance.
     * Not really recommended
     * Design a logger class with singleton design patter which is thread safe
     */
    class SingletonConcept
    {
        public static void Main()
        {
            // Read all the data from a file. And, use that data.
            var dd0 = SingletonDataContainer.Instance;
            Console.WriteLine(dd0.GetPopulation("Singapore "));
            var dd1 = SingletonDataContainer.Instance;
            var dd2 = SingletonDataContainer.Instance;
            var dd3 = SingletonDataContainer.Instance;
            var dd4 = SingletonDataContainer.Instance;

        }

        interface ISingletonContainer
        {
            int GetPopulation(string name);
        }

        class SingletonDataContainer : ISingletonContainer
        {
            // Instead of a dictionary, try List + ValueTuple
            private List<(string,int)> Capitals = new List<(string, int)>();

            private readonly string FilePath = @"c:\temp\journal.txt";

            // this is auto property syntax, the constructor will be called only once.
            public static SingletonDataContainer Instance { get; } = new SingletonDataContainer();

            // is it true that it will be only instantiated once ?
            public static int Counter { get; set; }

            /// <summary>
            /// Private constructor for singleton design pattern. Only called once.
            /// </summary>
            private SingletonDataContainer()
            {
                // Check if calling the property multiple times will have a change in the counter
                Counter++;
                Console.WriteLine("How many times constructor is called: "  + Counter);

                Console.WriteLine("Initializing singleton object");

                string[] elements = File.ReadAllLines(FilePath);

                for (int i = 0; i < elements.Length; i += 2)
                {
                    Capitals.Add((elements[i], int.Parse(elements[i + 1])));
                }
            }
            public int GetPopulation(string name)
            {
                for (int i = 0; i < Capitals.Count; i++)
                {
                    if (Capitals[i].Item1 == name)
                    {
                        return Capitals[i].Item2;
                    }
                }

                return 0;
            }
        }


    }
}
