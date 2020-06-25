using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class UnderstandingTuple
    {
        // Tuple<T> was introduced in .NET Framework 4.0
        // Tuple is a data structure that contains a sequence of different data types
        // Create a data structure to hold an object with property, but you dont want to 
        // create a seperate class for it. Something like anonymous types.
        public static void Main()
        {
            // Introducing Tuple Class
            var person = Tuple.Create(1, "Steve", "Jobs");
            PrintInformation(person);

            var person2 = CreatePerson(2, "Bob", "Marley");
            Console.WriteLine($"{person2.Item1} - {person2.Item2} {person2.Item3}");

            // Introducing Value Tuple
            // Tuple requires at least 2 values.
            // Unlike Tuple, valueTuple can hold more than 8 values.
            var person3 = (3, "James", "Bond"); // or..
            ValueTuple<int, string, string> person4 = (4, "Jamie", "King"); // or..
            (int, string, string) person5 = (5, "David", "Beckham");
            (int Id, string firstName, string lastName) person6 = (6, "Victoria", "Beckham");

            List<(int, string, string)> personList = new List<(int, string, string)>();
            personList.Add(person3);
            personList.Add(person4);
            personList.Add(person5);
            personList.Add(person6);

            foreach (var p in personList)
            {
                PrintInformation(p);
            }
        }
        /// <summary>
        /// Pass multiple values to a method through a single paramenter.
        /// </summary>
        /// <param name="person"></param>
        private static void PrintInformation(Tuple<int,string,string> person)
        {
            Console.WriteLine($"{person.Item1} - {person.Item2} {person.Item3}");
        }

        /// <summary>
        /// Overload to print person information which takes in a value tuple.
        /// </summary>
        /// <param name="person"></param>
        private static void PrintInformation((int, string, string) person)
        {
            Console.WriteLine($"{person.Item1} - {person.Item2} {person.Item3}");
        }

        /// <summary>
        /// Return multiple values from a method without using ref or out parameters
        /// </summary>
        /// <param name="index">Position to be stored, preferably used with a List<> </param>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        /// <returns></returns>
        private static Tuple<int,string,string> CreatePerson(int index, string firstName, string lastName)
        {
            return new Tuple<int, string, string>(index, firstName, lastName);
        }
    }
}
