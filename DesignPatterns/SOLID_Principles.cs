using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Solid Principles if Implemented PROPERLY 
    /// should improve our code significally.
    /// </summary>
    class SOLID_Principles
    {
        // Solid Principles.
        // https://code-maze.com/solid-principles/
        public static void Main()
        {
            SingleResponsibilityPrinciple();
            Console.WriteLine();
            OpenClosePrinciple_example1();
            Console.WriteLine();
            OpenClosePrinciple_example2();
            Console.WriteLine();
            LiskovSubstituitionPrinciple();
            Console.WriteLine(); 
            DependencyInversionPrinciple(); 
        }

        #region Single Responsibility Principles

        //Every class should do its own task and do it well.
        private static void SingleResponsibilityPrinciple()
        {
            var report = new WorkReport();

            report.AddEntry(
                new WorkReportEntry
                {
                    ProjectCode = "123Ds",
                    ProjectName = "Project 1",
                    SpentHours = 5
                });

            report.AddEntry(
                new WorkReportEntry
                {
                    ProjectCode = "321Dp",
                    ProjectName = "Project 2",
                    SpentHours = 3
                });

            var scheduler = new Schedule();

            scheduler.AddEntry(
               new ScheduleTask
               {
                   TaskId = 1,
                   Content = "Do some shit",
                   ExecuteOn = DateTime.Now.AddDays(5)
               });

            scheduler.AddEntry(
                new ScheduleTask
                {
                    TaskId = 2,
                    Content = "Do more shit",
                    ExecuteOn = DateTime.Now.AddDays(3)
                });

            Console.WriteLine(report);
            Console.WriteLine(scheduler);

            var saver = new FileSaver();
            saver.SaveToFile(@"Reports", "WorkReport.txt", report);
            saver.SaveToFile(@"Reports", "Schedule.txt", scheduler);
        }
        // Code reusability with implementation of IEntryManager
        interface IEntryManager<T>
        {
            void AddEntry(T entry);
            void RemoveEntryAt(int index);
        }
        class WorkReportEntry
        {
            public string ProjectCode { get; set; }
            public string ProjectName { get; set; }
            public int SpentHours { get; set; }
        }
        class ScheduleTask
        {
            public int TaskId { get; set; }
            public string Content { get; set; }
            public DateTime ExecuteOn { get; set; }
        }

        class Schedule : IEntryManager<ScheduleTask>
        {
            private readonly List<ScheduleTask> ScheduleTasks;
            public Schedule()
            {
                ScheduleTasks = new List<ScheduleTask>();
            }
            public void AddEntry(ScheduleTask entry)
            {
                ScheduleTasks.Add(entry);
            }

            public void RemoveEntryAt(int index)
            {
                ScheduleTasks.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, ScheduleTasks
                             .Select(x => $"Task Id: {x.TaskId}, " +
                             $"Task Content: {x.Content} is going to be executed"));
            }
        }

        /// <summary>
        /// Sole responsibility is to keep track of work report entries.
        /// </summary>
        class WorkReport : IEntryManager<WorkReportEntry>
        {
            // Refactor for code maintainability.
            private readonly List<WorkReportEntry> Entries;
            public WorkReport()
            {
                Entries = new List<WorkReportEntry>();
            }

            public void AddEntry(WorkReportEntry entry) => Entries.Add(entry);
            public void RemoveEntryAt(int index) => Entries.RemoveAt(index);
            public override string ToString()
            {
                return string
                    .Join(Environment.NewLine, Entries

                    // Selector: A transform function to apply to each element of source.
                    .Select(entry => $"Code: {entry.ProjectCode}, " +
                    $"Name: {entry.ProjectName}, Time: {entry.SpentHours}hrs"));
            }
        }
        /// <summary>
        /// Sole responsibility of FileSaver is to save files.
        /// </summary>
        class FileSaver
        {
            public void SaveToFile<T>(string directoryPath, string fileName, IEntryManager<T> report)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                File.WriteAllText(Path.Combine(directoryPath, fileName), report.ToString());
            }
        }
        #endregion

        #region Open Closed Principle Example 1

        private static void OpenClosePrinciple_example1()
        {
            // Open for extension but closed for modification.
            var devReports = new List<DeveloperReport>
            {
                new DeveloperReport{Id = 1, Name = "Dev1", Level = "Senior", HourlyRate = 30.5, WorkingHours = 60},
                new DeveloperReport{Id = 2, Name = "Dev2", Level = "Junior", HourlyRate = 20, WorkingHours = 150},
                new DeveloperReport{Id = 3, Name = "Dev3", Level = "Senior", HourlyRate = 30.5, WorkingHours = 180},
            };

            var calculator = new SalaryCalculator(devReports);
            Console.WriteLine($"Sum of all the developer salaries is {calculator.CalculateTotalSalaries():C2}");

            var devCalculations = new List<BaseCalculator>
            {
                new SeniorDevSalaryCalculator(new DeveloperReport
                {
                    Id = 1,
                    Name = "Dev1",
                    Level = "Senior",
                    HourlyRate = 30.5,
                    WorkingHours = 60
                }),

                new JuniorDevSalaryCalculator(new DeveloperReport
                {
                    Id = 2,
                    Name = "Dev2",
                    Level = "Junior",
                    HourlyRate = 20,
                    WorkingHours = 150
                }),

                new SeniorDevSalaryCalculator(new DeveloperReport
                {
                    Id = 3,
                    Name = "Dev3",
                    Level = "Senior",
                    HourlyRate = 30.5,
                    WorkingHours = 180
                })
            };

            // this is very similar to strategy pattern.
            NewSalaryCalculator newSalaryCalculator = new NewSalaryCalculator(devCalculations);
            Console.WriteLine($"Sum of all the developer salaries is " +
                $"{newSalaryCalculator.CalculateTotalSalaries():C2}");
        }

        class DeveloperReport
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Level { get; set; }
            public int WorkingHours { get; set; }
            public double HourlyRate { get; set; }
        }

        // This class will ensure that all extensions will have Calculate Salary method.
        abstract class BaseCalculator
        {
            protected DeveloperReport DeveloperReport { get; private set; }
            public BaseCalculator(DeveloperReport developerReport)
            {
                DeveloperReport = developerReport;
            }

            public abstract double CalculateSalary();
        }

        class SeniorDevSalaryCalculator : BaseCalculator
        {
            public SeniorDevSalaryCalculator(DeveloperReport developerReport)
                : base(developerReport)
            {
            }

            public override double CalculateSalary()
            {
                return DeveloperReport.HourlyRate * DeveloperReport.WorkingHours * 1.2;
            }
        }

        // Remember think of inheritance as a partial class with encapsulation.
        class JuniorDevSalaryCalculator : BaseCalculator
        {
            public JuniorDevSalaryCalculator(DeveloperReport developerReport)
                : base(developerReport)
            {
            }

            public override double CalculateSalary()
            {
                return DeveloperReport.HourlyRate * DeveloperReport.WorkingHours;
            }
        }

        // This class will be closed for modifications.
        class NewSalaryCalculator
        {
            private readonly IEnumerable<BaseCalculator> DeveloperCalculator;
            public NewSalaryCalculator(IEnumerable<BaseCalculator> baseCalculators)
            {
                DeveloperCalculator = baseCalculators;
            }

            public double CalculateTotalSalaries()
            {
                double totalSalaries = 0D;

                foreach (var devCalc in DeveloperCalculator)
                {
                    totalSalaries += devCalc.CalculateSalary();
                }

                return totalSalaries;
            }
        }

        /// <summary>
        /// Bad example of open close principle
        /// </summary>
        class SalaryCalculator
        {
            private readonly IEnumerable<DeveloperReport> DeveloperReports;
            public SalaryCalculator(List<DeveloperReport> developerReports)
            {
                DeveloperReports = developerReports;
            }

            public double CalculateTotalSalaries()
            {
                double totalSalaries = 0D;
                foreach (var devReport in DeveloperReports)
                {
                    totalSalaries += devReport.HourlyRate * devReport.WorkingHours;
                }

                return totalSalaries;
            }
        }
        #endregion

        #region Open Closed Princple Example 2

        private static void OpenClosePrinciple_example2()
        {
            var monitors = new List<ComputerMonitor>
            {
                new ComputerMonitor { Name = "Samsung S345", Screen = Screen.CurvedScreen, Type = MonitorType.OLED },
                new ComputerMonitor { Name = "Philips P532", Screen = Screen.WideScreen, Type = MonitorType.LCD },
                new ComputerMonitor { Name = "LG L888", Screen = Screen.WideScreen, Type = MonitorType.LED },
                new ComputerMonitor { Name = "Samsung S999", Screen = Screen.WideScreen, Type = MonitorType.OLED },
                new ComputerMonitor { Name = "Dell D2J47", Screen = Screen.CurvedScreen, Type = MonitorType.LCD }
            };

            // Old design
            var filter = new MonitorFilter();
            var lcdMonitors = filter.FilterByType(monitors, MonitorType.LCD);
            Console.WriteLine("All LCD monitors");
            foreach (var monitor in lcdMonitors)
            {
                Console.WriteLine($"Name: {monitor.Name}, Type: {monitor.Type}, Screen: {monitor.Screen}");
            }

            // New Design
            var newFilter = new NewMonitorFilter();
            List<ComputerMonitor> listOfLCDs = newFilter.Filter(monitors, new MonitorTypeSpecification(MonitorType.LCD));
            Console.WriteLine("\nAll LCD Monitors");
            listOfLCDs.ForEach(m => Console.WriteLine($"Name: {m.Name}, Type: {m.Type}, Screen: {m.Screen}"));
        }
        class ScreenSpecification : ISpecification<ComputerMonitor>
        {
            private readonly Screen Screen;
            public ScreenSpecification(Screen screen)
            {
                Screen = screen;
            }
            public bool IsSatisfied(ComputerMonitor item)
            {
                return item.Screen == Screen;
            }
        }
        class MonitorTypeSpecification : ISpecification<ComputerMonitor>
        {
            private readonly MonitorType Type;
            public MonitorTypeSpecification(MonitorType type)
            {
                Type = type;
            }
            public bool IsSatisfied(ComputerMonitor item)
            {
                return item.Type == Type;
            }
        }

        class NewMonitorFilter : IFilter<ComputerMonitor>
        {
            public List<ComputerMonitor> Filter
                (IEnumerable<ComputerMonitor> monitors, ISpecification<ComputerMonitor> specification)
            {
                return monitors.Where(m => specification.IsSatisfied(m)).ToList();
            }
        }

        interface ISpecification<T>
        {
            bool IsSatisfied(T item);
        }
        interface IFilter<T>
        {
            // returns a list.
            List<T> Filter(IEnumerable<T> monitors, ISpecification<T> specification);
        }
        enum MonitorType
        {
            OLED, LCD, LED
        }

        enum Screen
        {
            WideScreen, CurvedScreen
        }

        class ComputerMonitor
        {
            public string Name { get; set; }
            public MonitorType Type { get; set; }
            public Screen Screen { get; set; }
        }

        class MonitorFilter
        {
            public List<ComputerMonitor> FilterByType(IEnumerable<ComputerMonitor> monitors, MonitorType type)
                => monitors.Where(m => m.Type == type).ToList();
        }
        #endregion

        #region Liskov Substitution Principle
        // Child class objects should be able to replace parent class objects
        // without compromising application integrity. Covariance ?

        private static void LiskovSubstituitionPrinciple()
        {
            SumCalculator sum = new SumCalculator(5, 7, 8, 9, 1, 6, 4);
            Console.WriteLine($"The sum of all the numbers: {sum.Calculate()}");
            Console.WriteLine();
            EvenNumbersSumCalculator evenSum = new EvenNumbersSumCalculator(5, 7, 8, 9, 1, 6, 4);
            Console.WriteLine($"The sum of all the even numbers: {evenSum.Calculate()}");

            // Child class inherits from parent, child is a parent.
            // Change to virual and problem will be fixed.
            // We can use any subclass reference into a base class and behavior wont change.
            Calculator evenSum2 = new EvenNumbersSumCalculator(5, 7, 8, 9, 1, 6, 4);
            Console.WriteLine($"The sum of all the even numbers: {evenSum2.Calculate()}");
        }
        abstract class Calculator
        {
            protected readonly int[] Numbers;
            public Calculator(int[] numbers)
            {
                Numbers = numbers;
            }

            public abstract int Calculate();
        }

        class SumCalculator : Calculator
        {
            public SumCalculator(params int[] numbers)
                :base(numbers)
            {
            }

            public override int Calculate() => Numbers.Sum();
        }

        class EvenNumbersSumCalculator : Calculator
        {
            public EvenNumbersSumCalculator(params int[] numbers)
                :base(numbers)
            {
            }

            public override int Calculate() => Numbers.Where(x => x % 2 == 0).Sum();
        }
        #endregion

        #region Interface Segregation Principle
        private static void InterfaceSegregationPrinciple()
        {
            // No client should be forced to depend on methods it does not use.
            // We should reduce code objects fown to the smallest required implementation
            // thus creating interfacs with only required declarations.
        }

        interface ICar
        {
            void Drive();
        }

        interface IAirplane
        {
            void Fly();
        }
        interface IMultiFunctionalCar : ICar, IAirplane
        {
            // Better to have many smaller interfaces then few large interfaces that
            // forces us to implement non-required methods in our classes.
        }

        class Car : ICar
        {
            public void Drive()
            {
                Console.WriteLine("Driving a multifunctional car");
            }
        }

        class Airplane : IAirplane
        {
            public void Fly()
            {
                Console.WriteLine("Flying a multifunctional car");
            }
        }

        /// <summary>
        /// Application of decorator pattern.
        /// </summary>
        class MultiFunctionalCar : IMultiFunctionalCar
        {
            private readonly ICar Car;
            private readonly IAirplane Airplane;

            public MultiFunctionalCar(ICar car, IAirplane airplane)
            {
                Car = car;
                Airplane = airplane;
            }
            public void Drive()
            {
                Car.Drive();
            }

            public void Fly()
            {
                Airplane.Fly();
            }
        }

        #endregion

        #region Dependency Inversion Principle
        private static void DependencyInversionPrinciple()
        {
            // Create decoupling structure between high and low level modules through abstraction.
            var empManager = new EmployeeSort();
            empManager.AddEmployee(new Employee { Name = "Leen", Gender = Gender.Female, Position = Position.Manager });
            empManager.AddEmployee(new Employee { Name = "Mike", Gender = Gender.Male, Position = Position.Administrator });

            var stats = empManager.Sort(new TwoSpecs<Employee>(new GenderSpec(Gender.Female), new PositionSpec(Position.Manager)));
            Console.WriteLine(string.Join(" ", stats));
        }

        enum Gender
        {
            Male, Female
        }

        enum Position
        {
            Administrator, Manager, Executive
        }

        class Employee
        {
            public string Name { get; set; }
            public Gender Gender { get; set; }
            public Position Position { get; set; }

            public override string ToString()
            {
                return $"Employee Name: {Name}, Gender: {Gender}, Position: {Position}";
            }
        }
      
        interface ISpecs<T>
        {
            bool IsSatisfied(T item);
        }

        class TwoSpecs<T> : ISpecs<T>
        {
            ISpecs<T> First;
            ISpecs<T> Second;
            public TwoSpecs(ISpecs<T> first, ISpecs<T> second)
            {
                First = first;
                Second = second;
            }
            public bool IsSatisfied(T item)
            {
                return First.IsSatisfied(item) && Second.IsSatisfied(item);
            }
        }

        class GenderSpec : ISpecs<Employee>
        {
            Gender Gender;
            public GenderSpec(Gender gender)
            {
                Gender = gender;
            }
            public bool IsSatisfied(Employee item)
            {
                return item.Gender == Gender;
            }
        }

        class PositionSpec : ISpecs<Employee>
        {
            Position Position;
            public PositionSpec(Position position)
            {
                Position = position;
            }
            public bool IsSatisfied(Employee item) => item.Position == Position;
        }
        
        
        // Data Base
        class EmployeeManager
        {
            protected List<Employee> Employees;
            public EmployeeManager()
            {
                Employees = new List<Employee>();
            }

            public void AddEmployee(Employee employee)
            {
                Employees.Add(employee);
            }
        }

        // Sorting Class
        class EmployeeSort : EmployeeManager
        {
            // Extension to access the protected List inside Employee Manager.
            public IEnumerable<Employee> Sort(ISpecs<Employee> specs)
            {
                // why this method be static?
                // Object reference is required for static field method
                // Employees is a field of an instance class.
                // It cannot be initialised if this method is static.
                foreach (var employee in Employees)
                {
                    if (specs.IsSatisfied(employee))
                    {
                        yield return employee;
                    }
                }
            }
        }
        #endregion
    }
}
