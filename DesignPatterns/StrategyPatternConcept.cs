using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class StrategyPatternConcept
    {
        /*
         * Stratey Design Pattern
         * https://www.code-maze.com/strategy
         * 
         * Context : Background information or arguments to a program
         * https://stackoverflow.com/questions/6145091/the-term-context-in-programming
         * Read the above for clarity.
         * 
         * Context object is the background information or references to the strategy object.
         * Use strategy patterns whenever we have different variation for some functionality 
         * in an object and we want to switch from one variation to another in runtime.
         */
        public static void Main()
        {
            Client();
        }

        private static void Client()
        {
            DeveloperReport newReport = new DeveloperReportBuilder()
                .MyPersonalInfo
                .NameIs("Sher Ning")
                .IDis(10)
                .MyWorkInfo
                .LevelIs(DeveloperLevel.Senior)
                .HoursWorked(50)
                .RatesAt(150)
                .Build();

            Console.WriteLine(newReport);

            List<DeveloperReport> reports = new List<DeveloperReport>
            {
                new DeveloperReportBuilder()
                .MyPersonalInfo
                .NameIs("Sher Ning")
                .IDis(1)
                .MyWorkInfo
                .LevelIs(DeveloperLevel.Senior)
                .HoursWorked(50)
                .RatesAt(150)
                .Build(),

                new DeveloperReportBuilder()
                .MyPersonalInfo
                .NameIs("Johnny")
                .IDis(2)
                .MyWorkInfo
                .LevelIs(DeveloperLevel.Junior)
                .HoursWorked(60)
                .RatesAt(60)
                .Build()

            };

            // This line is the essense of a strategy pattern design.
            var calculatorContext = new SalaryCalculator(new JuniorDeveloperSalaryCalculator());

            var juniorTotal = calculatorContext.Calculate(reports);
            Console.WriteLine($"Total amount for junior salaries is: {juniorTotal}");

            calculatorContext.SetCalculator(new SeniorDeveloperSalaryCalculator());
            var seniorTotal = calculatorContext.Calculate(reports);
            Console.WriteLine($"Total amount for senior salaries is: {seniorTotal}");

            Console.WriteLine($"Total cost for all the salaries is: {juniorTotal + seniorTotal}");
        }

        enum DeveloperLevel { Senior, Junior }

        class DeveloperReport
        {
            // Properties
            public int Id { get; set; }
            public string Name { get; set; }
            public DeveloperLevel Level { get; set; }
            public int WorkingHours { get; set; }
            public int HourlyRate { get; set; }

            // Methods
            public double CalculateSalary() => WorkingHours * HourlyRate;

            public override string ToString()
            {
                return new StringBuilder()
                    .AppendLine($"My name is {Name} and my Id is {Id}")
                    .AppendLine($"I am currently a {Level} Developer") // no need to cast enum.
                    .AppendLine($"I work {WorkingHours} hrs a week")
                    .AppendLine($"And my rates are {HourlyRate:C2}/hr")
                    .ToString();
            }
        }

        // Implement facade builder to build developer report.
        class DeveloperReportBuilder
        {
            /* Why do you need to pass the DeveloperReport field into Personal and Employment builder ?
             * When PersonalInfo is set to a new instance, it will have its own Developer builder,
             * with its own default DeveloperReport. So, in order for Employement to share the report
             * Employment must take in the same DeveloperReport instances for Personal to use. Otherwise,
             * Employment will inherit a defaulted DeveloperReportBuilder with a default DeveloperReport.
             * https://stackoverflow.com/questions/42592348/instance-variable-inheritance-in-java
             */
            protected DeveloperReport DeveloperReport;
            public PersonalInfoBuilder MyPersonalInfo => new PersonalInfoBuilder(DeveloperReport);
            public EmploymentInfoBuilder MyWorkInfo => new EmploymentInfoBuilder(DeveloperReport); 
            public DeveloperReportBuilder()
            {
                // Constructor has to be public for inheritance.
                // https://stackoverflow.com/questions/31483210/why-does-private-constructor-prohibits-a-inheritance
                // When child constructor is called, the parent constructor is also invoked.

                DeveloperReport = new DeveloperReport();
            }

            // return developer report.
            public DeveloperReport Build() => DeveloperReport;
        }

        class PersonalInfoBuilder : DeveloperReportBuilder
        {
            public PersonalInfoBuilder(DeveloperReport report)
            {
                DeveloperReport = report;
            }
           
            public PersonalInfoBuilder NameIs(string name)
            {
                DeveloperReport.Name = name;

                // return this is a mechanism to chain all the DeveloperReportBuilder.
                return this;
            }

            public PersonalInfoBuilder IDis(int id)
            {
                DeveloperReport.Id = id;
                return this;
            }
        }

        class EmploymentInfoBuilder : DeveloperReportBuilder
        {
            public EmploymentInfoBuilder(DeveloperReport report)
            {
                DeveloperReport = report;
            }
           
            public EmploymentInfoBuilder LevelIs(DeveloperLevel level)
            {
                DeveloperReport.Level = level;
                return this;
            }

            public EmploymentInfoBuilder HoursWorked(int hours)
            {
                DeveloperReport.WorkingHours = hours;
                return this;
            }
            public EmploymentInfoBuilder RatesAt(int rates)
            {
                DeveloperReport.HourlyRate = rates;
                return this;
            }
        }

        interface ISalaryCalculator
        {
            double CalculateTotalSalary(IEnumerable<DeveloperReport> reports);
        }

        class JuniorDeveloperSalaryCalculator : ISalaryCalculator
        {
            public double CalculateTotalSalary(IEnumerable<DeveloperReport> reports)
            {
                return reports
                    .Where(report => report.Level == DeveloperLevel.Junior)
                    .Select(report => report.CalculateSalary())
                    .Sum();
            }
        }

        class SeniorDeveloperSalaryCalculator : ISalaryCalculator
        {
            public double CalculateTotalSalary(IEnumerable<DeveloperReport> reports)
            {
                return reports
                   .Where(report => report.Level == DeveloperLevel.Senior)
                   .Select(report => report.CalculateSalary())
                   .Sum();
            }
        }

        class SalaryCalculator
        {
            /// <summary>
            /// Set either Junior or Senior Developer Salary Calculator here.
            /// </summary>
            private ISalaryCalculator Calculator;

            /// <summary>
            /// Set the strategy object (developer salary calculator) at compile time
            /// </summary>
            /// <param name="calculator"></param>
            public SalaryCalculator(ISalaryCalculator calculator)
            {
                Calculator = calculator;
            }

            // Do an experiment, property vs static field that returns an object.

            /// <summary>
            /// Using the SetCalculator method to modify during run time.
            /// </summary>
            public void SetCalculator(ISalaryCalculator calculator) => Calculator = calculator;
            public double Calculate(IEnumerable<DeveloperReport> reports)
            {
                return Calculator.CalculateTotalSalary(reports);
            }
        }
    }
}
