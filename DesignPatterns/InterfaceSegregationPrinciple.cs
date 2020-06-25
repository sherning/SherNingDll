using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Don't put too much into an interface, split them into separate interfaces.
    /// </summary>
    class InterfaceSegregationPrinciple
    {
        public static void Main()
        {
            Document d = new Document("Text");

            // This is similar to multichart design, that takes in a class object.
            QuantumMachine qM = new QuantumMachine(new BasicPrinter(), new AdvancePrinter());
            qM.Print(d); // Print from basic machine
            qM.Scan(d);  // Scan from advance machine
        }

        class Document
        {
            public string Text { get; set; }
            public Document(string text)
            {
                Text = text;
            }
        }

        class BasicPrinter : IPrint
        {
            public void Print(Document d)
            {
                Console.WriteLine("Printing from basic printer");
            }
        }

        class AdvancePrinter : IMachine
        {
            public void Fax(Document d)
            {
                Console.WriteLine("Faxing from advance machine");
            }

            public void Print(Document d)
            {
                Console.WriteLine("Printing from advance machine");
            }

            public void Scan(Document d)
            {
                Console.WriteLine("Scanning from advance machine");
            }
        }

        class QuantumMachine : IMachine
        {
            private IPrint Printer;
            private IScan Scanner;

            public QuantumMachine(IPrint printer, IScan scanner)
            {
                Printer = printer;
                Scanner = scanner;
            }
            public void Fax(Document d)
            {
                Console.WriteLine("Faxing from Quantum Machine");
            }

            // Decorator Pattern
            public void Print(Document d)
            {
                Printer.Print(d);
            }

            public void Scan(Document d)
            {
                Scanner.Scan(d);
            }
        }

        // Better to have atomic size interfaces.
        // Than to have one BIG interface that implements many things.
        // This is the principle of interface Segregation Principle.
        // Interfaces can inherit from another interface.
        interface IMachine : IPrint, IScan
        {
            void Fax(Document d);
        }
        interface IPrint
        {
            void Print(Document d);
        }
        interface IScan
        {
            void Scan(Document d);
        }
    }
}
