using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Learning Recursive Generics
    /// </summary>
    class FluentInterface
    {
        // Can i change this to Fluent interface ????
        // to call Main().Main()
        public static void Main()
        {
            var me = Person.New
                            .Called("Sher Ning")
                            .WorkedAs("Quant")
                            .Build();
            Console.WriteLine(me);
        }

        class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
            where SELF : PersonJobBuilder<SELF>
            // Generic type constraint is used for only reference types as to set minimum threshold
            // for inheritance. Since, value types are sealed, you are effectively limited to that type.
        {
            public SELF WorkedAs(string position)
            {
                person.Position = position;
                return (SELF) this;
            }
        }

        class PersonInfoBuilder<SELF> : PersonBuilder 
            where SELF : PersonInfoBuilder<SELF>
        {
            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF) this;
            }
        }

        abstract class PersonBuilder
        {
            protected Person person = new Person();

            // Last call for fluent interface return type can be anything.
            public Person Build()
            {
                return person;
            }
        }

        class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }
            public class Builder : PersonJobBuilder<Builder>
            {

            }
            public static Builder New => new Builder();
            public Person()
            {

            }
            public Person(string name, string position = null)
            {
                Name = name;
                Position = position;
            }
            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

    }
}
