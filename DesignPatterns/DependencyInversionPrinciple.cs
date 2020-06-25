using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// High level modules should not depend upon low level ones.
    /// </summary>
    class DependencyInversionPrinciple
    {
        public static void Main()
        {
            // Static field demo.
            // DependencyInversionPrinciple.numList.Add(1);

            Relationships relationships = new Relationships();
            Person parent = new Person("John");
            Person child = new Person("Chris");
            Person child2 = new Person("Mary");

            relationships.AddRelationship(parent, child);
            relationships.AddRelationship(parent, child2);

            // Using the constructor method to print.
            Research research = new Research(relationships, parent.Name);
        }

        enum Relationship
        {
            Parent, Child, Siblings
        }
        class Person
        {
            public string Name { get; set; }
            public Person(string name)
            {
                Name = name;
            }
        }

        /// <summary>
        /// Low level class. Deep level class.
        /// </summary>
        class Relationships : IRelationshipBrowser
        {
            // This is never exposed to high level application consuming it.
            // This is abstraction.
            private List<(Person, Relationship, Person)> relations 
                = new List<(Person, Relationship, Person)>();

            public void AddRelationship(Person parent, Person child)
            {
                // Need extra parenthesis for valueTuple
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations
                    // Parent's name matches name input 
                    .Where(x => x.Item1.Name == name)

                    // Check the relationship if the name is a parent
                    .Where(x => x.Item2 == Relationship.Parent)

                    // Output the child.
                    .Select(x => x.Item3);
            }
        }

        /// <summary>
        /// High level class, closer to user usuage.
        /// </summary>
        class Research
        {
            // Method inside contructor.
            // The constructor is a special method which will be called once
            // when the new key word is used to initialize the instance of the object.
            public Research(IRelationshipBrowser browser, string name)
            {
                foreach (var person in browser.FindAllChildrenOf(name))
                {
                    Console.WriteLine($"{name} has a child called {person.Name}");
                }
            }
        }
        /// <summary>
        /// Query all children of this parent.
        /// </summary>
        interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        #region Utility Class Concept
        // Why does list needs to be static ?
        // Answer : numList is a member of an instance class.
        // without the static keyword, you need to create an instance of the class
        // to access numList. 

        // Think of it this way, instance class is to create a template for an object.
        // And static class is more of a utility class with several functions.
        // Generally, utility class should not contain properties or fields.
        // Only methods.

        private static List<int> numList = new List<int>();

        private static void FillValues()
        {
            for (int i = 1; i < 10; i++)
            {
                numList.Add(i);
            }
        }

        /// <summary>
        /// Yield Recap : how to use yield keyword.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<int> Filter()
        {
            foreach (var i in numList)
            {
                if (i > 3)
                {
                    // with yield, you do not need to create a 
                    // temp list to store return value.
                    // stateful iteration and custom iteration.
                    // Yield helps to remember the state.
                    yield return i;
                }
            }
        }
        #endregion
    }
}
