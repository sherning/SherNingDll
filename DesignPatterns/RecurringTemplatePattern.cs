using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class RecurringTemplatePattern
    {
        public static void Main()
        {
            BuilderApproach();
            RecursiveGenerics();
        }

        #region Builder Approach
        // Understanding the basic principles of builder design pattern first
        // Why use builder ? Create a complex object. Prevent the use of a large constructor 
        // To construct object.
        public static void BuilderApproach()
        {
            List<Product> products = new List<Product>
            {
                new Product{ Name = "Monitor", Price = 200.50 },
                new Product{ Name = "Mouse", Price = 20.40 },
                new Product{ Name = "Key Board", Price = 30.50 }
            };

            var builder = new ProductStockReportBuilder(products);
            var director = new ProductStockReportDirector(builder);

            // Only director can give the command to build the report.
            director.BuildStockReport();

            var report = builder.GetReport();
            Console.WriteLine(report);
        }

        class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        class ProductStockReport
        {
            public string Header { get; set; }
            public string Body { get; set; }
            public string Footer { get; set; }

            public override string ToString()
            {
                return new StringBuilder()
                    .AppendLine(Header)
                    .AppendLine(Body)
                    .AppendLine(Footer)
                    .ToString();
            }
        }

        /// <summary>
        /// Using interface vs abstract class
        /// </summary>
        interface IProductStockReportBuilder
        {
            // Instead of returning void, we return interface to create fluent builder.
            IProductStockReportBuilder BuildHeader();
            IProductStockReportBuilder BuildBody();
            IProductStockReportBuilder BuildFooter();
            ProductStockReport GetReport();
        }

        class ProductStockReportBuilder : IProductStockReportBuilder
        {
            private ProductStockReport ProductStockReport;
            private IEnumerable<Product> Products;

            // Receive all the products requried for our report and instantiate the object.
            public ProductStockReportBuilder(IEnumerable<Product> products)
            {
                Products = products;
                ProductStockReport = new ProductStockReport();
            }

            public IProductStockReportBuilder BuildHeader()
            {
                ProductStockReport.Header = $"STOCK REPORT FOR ALL THE PRODUCTS ON DATE: {DateTime.Now}\n";
                return this;
            }

            public IProductStockReportBuilder BuildBody()
            {
                ProductStockReport.Body =
                    string.Join
                    (
                        // Create a new line, and print each product name and price.
                        Environment.NewLine, Products.Select(p => $"Product Name: {p.Name}, Price: {p.Price:C2}")
                    );

                return this;
            }

            public IProductStockReportBuilder BuildFooter()
            {
                ProductStockReport.Footer = "\nReport provided by Sher Ning Technologies";
                return this;
            }

            public ProductStockReport GetReport()
            {
                var temp = ProductStockReport;

                // Reset object and prepare new instance to create another report.
                Clear();
                return temp;
            }

            private void Clear() => ProductStockReport = new ProductStockReport();
        }

        /// <summary>
        /// Encapsulate the building process inside a Director Class
        /// </summary>
        class ProductStockReportDirector
        {
            // use interface, as it guruantees that the class uses those methods you need to create a report.
            private readonly IProductStockReportBuilder ProductStockReportBuilder;
            public ProductStockReportDirector(IProductStockReportBuilder productReport)
            {
                ProductStockReportBuilder = productReport;
            }

            // Applying fluent builder interface to build stock report.
            public void BuildStockReport()
            {
                ProductStockReportBuilder
                    .BuildHeader()
                    .BuildBody()
                    .BuildFooter();
            }
        }
        #endregion

        #region Recursive Generics
        // Before learning builder recursive generics

        public static void RecursiveGenerics()
        {
            // The whole concept is to link all the hierachy of inheritance with
            // a common T type. 
            // To really understand recursive generic you must see the inheritance structure
            // without any generics.
            Employee employee = EmployeeBuilderDirector
                .NewEmployee
                .SetName("Sher Ning")
                .AtPosition("Quant Engineer")
                .WithSalary(1_000_000)
                .Build();

            Console.WriteLine(employee);
        }
        
        class Employee
        {
            public string Name { get; set; }
            public string Position { get; set; }
            public double Salary { get; set; }
            public override string ToString()
            {
                // format for currency. 
                return   $"Name: {Name}\n" +
                         $"Position: {Position}\n" +
                         $"Salary: {Salary:C2}\n";
            }
        }

        /// <summary>
        /// Cannot be interface, as you need Employee object to be passed down the inheritance hierachy
        /// At the same time, you dont want people to instaniate your Employee builder class.
        /// </summary>
        abstract class EmployeeBuilder
        {
            protected Employee Employee;
            public EmployeeBuilder()
            {
                Employee = new Employee();
            }

            // Call build to return employee property
            public Employee Build() => Employee;
        }

        class EmployeeInfoBuilder<T> : EmployeeBuilder 
            where T : EmployeeInfoBuilder<T>
        {
            // SetName has to return a generic type. cannot return InfoBuilder.
            // Otherwise, the next builder cannot be chained.

            // Since SetName() must return a generic type, the class has to be generic as well.
            // Use Employee object reference by inheriting from EmployeeBuilder.

            // Need to restrict T to be a EmployeeInfoBuilder type. 
            // Since it is generic therefore the EmployeeInfoBuilder "<T>".

            // object.GetType() vs typeOf(class) are the same. one is a method, whereas one is a keyword.
            public T SetName(string name)
            {
                Employee.Name = name;
                Console.WriteLine("Name: " + typeof(T).Name); // T is PositionBuilder.
                return (T) this;
            }
        }

        // This is the child class, parent is employee info builder
        // Therefore, the parent do not inherit the child members.
        class EmployeePositionBuilder<U> : EmployeeInfoBuilder<EmployeePositionBuilder<U>>
            where U : EmployeePositionBuilder<U> // which U inherits from infobuilder
        {
            // The reason, why can chain is because of return type inheritance.
            public U AtPosition(string position)
            {
                Employee.Position = position;
                Console.WriteLine("Position: " + typeof(U).Name); // T is SalaryBuilder
                return (U)this;
            }
        }

        class EmployeeSalaryBuilder<P> : EmployeePositionBuilder<EmployeeSalaryBuilder<P>>
            where P : EmployeeSalaryBuilder<P>
            // where P : SalaryBuilder inherits from positionbuilder and infobuilder.
        {
            public P WithSalary(double salary)
            {
                Employee.Salary = salary;
                Console.WriteLine("Salary: " + typeof(P).Name); // T is EmployerBuilderDirector.
                return (P)this;
            }
        }

        /// <summary>
        /// API to allow the building of our project, which is the top of the inheritance.
        /// Or, child.
        /// </summary>
        class EmployeeBuilderDirector : EmployeeSalaryBuilder<EmployeeBuilderDirector>
        {
            // this is a property. which will have all the inheritance.
            public static EmployeeBuilderDirector NewEmployee => new EmployeeBuilderDirector();
        }
        #endregion
    }
}
