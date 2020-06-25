using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class FacetedBuilder
    {
        // Implicitly Conversion
        class Scooter 
        {
            public int Mileage { get; set; }

            /// <summary>
            /// The name of this method is implicit operator.
            /// </summary>
            /// <param name="scoot"></param>
            public static implicit operator Car(Scooter scoot)
            {
                // if change implicit to explicit, user requires to manually cast it.
                return new Car { Mileage = scoot.Mileage };
            }
        }
        class Car 
        { 
            public int Mileage { get; set; }
        }

        public static void Main()
        {
            // Implicit casting of the scooter to Car.
            Scooter scooter = new Scooter { Mileage = 10 };
            Car car = scooter;
            Console.WriteLine("Car Mileage: " + car.Mileage);

            // implicit cast is used here. To convert EmployeeBuilder to Employee.
            EmployeeBuilder eB = new EmployeeBuilder();
            Employee employee = eB
            .PersonalInformation // property of EmployeeBuilder class
                .NameIs("Sher Ning")
                .AgeIs(35)
                .LivesIn("Singapore")
             .WorksAt
                .Company("Thian Huat Siang Pte Ltd")
                .PositionIs("Director")
                .withAnnualIncomeOf(100_000)
            .EducationInformation
                .StudiedAt("University of Singapore");

            Console.WriteLine(employee);
        }

        class EmployeeBuilder // facade.
        {
            // passing the reference to one another.
            // for value types will be difficult.
            protected Employee Employee;
            public EmployeeBuilder()
            {
                // different from demo
                Employee = new Employee();
            }
            /// <summary>
            /// PersonInformation is a getter property, and not a method.
            /// </summary>
            public EmployeePersonalBuilder PersonalInformation
            {
                get 
                { 
                    return new EmployeePersonalBuilder(); 
                }
                // Implicit private set.
            }
            public EmployeeJobBuilder WorksAt => new EmployeeJobBuilder(Employee);

            public EmployeeEducationBuilder EducationInformation 
            { 
                // Get is a method.
                get 
                {  
                    return new EmployeeEducationBuilder(Employee); 
                }
                private set { }
            }

            /// <summary>
            /// When the fuck you need to use an implicit operator ??
            ///  See above for answer.
            /// </summary>
            /// <param name="eb"></param>
            public static implicit operator Employee(EmployeeBuilder eb)
            {
                return eb.Employee;
            }
        } 

        class EmployeeEducationBuilder : EmployeeBuilder
        {
            // Create employee field for employee builder

            public EmployeeEducationBuilder(Employee employee)
            {
                Employee = employee;
            }

            public EmployeeEducationBuilder StudiedAt(string university)
            {
                Employee.University = university;
                return this;
            }
        }

        class EmployeePersonalBuilder : EmployeeBuilder
        {
            //public EmployeePersonalBuilder(Employee employee)
            //{
            //    this.Employee = employee;
            //}

            public EmployeePersonalBuilder NameIs(string name)
            {
                this.Employee.Name = name;
                return this;
            }

            public EmployeePersonalBuilder AgeIs(int age)
            {
                Employee.Age = age;
                return this;
            }

            public EmployeePersonalBuilder LivesIn(string address)
            {
                Employee.Address = address;
                return this;
            }
        }

        class EmployeeJobBuilder : EmployeeBuilder
        {
            public EmployeeJobBuilder(Employee employee)
            {
                Employee = employee;
            }

            public EmployeeJobBuilder Company(string company)
            {
                Employee.CompanyName = company;
                return this;
            }

            public EmployeeJobBuilder PositionIs(string position)
            {
                Employee.Position = position;
                return this;
            }

            public EmployeeJobBuilder withAnnualIncomeOf(int annualIncome)
            {
                Employee.AnnualIncome = annualIncome;
                return this;
            }
        }

        private class Employee
        {
            // Personal details
            public string Name { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }

            // Employment Details
            public string CompanyName { get; set; }
            public string Position { get; set; }
            public int AnnualIncome { get; set; }

            // Education Details
            public string University { get; set; }

            public override string ToString()
            {
                return $"Name: {Name}\nAge: {Age}\nAddress: {Address}\n"
                    + $"Company: {CompanyName}\nPosition: {Position}\nAnnual Income: ${AnnualIncome}\n"
                    + $"University: {University}";
            }

        }
    }
}
