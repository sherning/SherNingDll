using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }

    class PersonBuilder
    {
        /// <summary>
        /// A list of Action with takes a single argument Person. Action is return void.
        /// </summary>
        public readonly List<Action<Person>> Actions = new List<Action<Person>>();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            Person p = new Person();

            // Learning to apply a list of different actions on the same object.

            //Actions.ForEach(a => a(p));
            foreach (var action in Actions)
            {
                action(p);
            }
            return p;
        }

        public void TakeAction(Action<Person> action, Person p)
        {
            action(p);
        }
    }

    static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p => { p.Position = position; });
            return builder;
        }

        public static PersonBuilder Print(this PersonBuilder builder)
        {
            builder.Actions.Add(p => { Console.WriteLine($"Name: {p.Name} , Position: {p.Position}, Age: {p.Age}"); });
            return builder;
        }
        public static PersonBuilder AddAge(this PersonBuilder builder, int age)
        {
            builder.Actions.Add(p => p.Age = age);
            return builder;
        }
    }

    class ExtensionMethods
    {
        public static void Main()
        {
            var pB = new PersonBuilder();
            pB.Called("Sher Ning")
                .WorksAsA("Quant")
                .AddAge(35)
                .Print()
                .Build();
        }
    }
}
