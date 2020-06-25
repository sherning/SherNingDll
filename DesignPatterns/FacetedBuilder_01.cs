using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class FacetedBuilder_01
    {
        public static void Main()
        {
            // This is implicit casting.
            Employee employee = new EmployeeBuilderFacade()
                .Info
                .FirstNameIs("Sher Ning")
                .LastNameIs("Teo");

            Console.WriteLine(employee);

            // vs Create method to return employee to caller
            Employee employee2 = new EmployeeBuilderFacade()
                 .Info
                 .FirstNameIs("Robbie")
                 .LastNameIs("Williams")
                 .Create();

            Console.WriteLine(employee2);
        }

        class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return $"Employee's Name: {FirstName} {LastName}";
            }
        }

        class EmployeeBuilderFacade
        {
            protected Employee Employee { get; set; }

            public EmployeeBuilderFacade()
            {
                Employee = new Employee();
            }

            // This is a class method, to return Employee to the caller.
            public Employee Create() => Employee;

            // Alternatively we can try using implicit operator
            public static implicit operator Employee(EmployeeBuilderFacade builder) => builder.Employee;

            // This is a class property to expose builder class inside the facade class
            public PersonalInfoBuilder Info => new PersonalInfoBuilder(Employee);
        }

        class PersonalInfoBuilder : EmployeeBuilderFacade
        {
            public PersonalInfoBuilder(Employee employee)
            {
                Employee = employee;
            }

            // Applying fluent interface for building purposes.
            public PersonalInfoBuilder FirstNameIs(string firstName)
            {
                Employee.FirstName = firstName;
                return this;
            }

            public PersonalInfoBuilder LastNameIs(string lastName)
            {
                Employee.LastName = lastName;
                return this;
            }
        }
    }
}
