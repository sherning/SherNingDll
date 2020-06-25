using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class DesignPatternsExercise
    {
        public static void Main()
        {
            var Johnny = PersonFactory.CreatePerson("Johnny");
            Console.WriteLine(Johnny);

            var Jimmy = PersonFactory.CreatePerson("Jimmy");
            Console.WriteLine(Jimmy);
        }

        class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return $"Hi, I am {Name}, my Id is {Id}";
            }
        }

        class PersonFactory
        {
            // Id will be shared across all instances created.
            private static int _ID = 1;
            public static Person CreatePerson(string name)
            {
                return new Person { Id = _ID++, Name = name };
            }
        }

    }
}
