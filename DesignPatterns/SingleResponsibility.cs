using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// A class should only have one reason to change.
    /// Separation of concers - different classes handling different, independent problems
    /// </summary>
    class SingleResponsibility
    {
        public static void Main()
        {
            Journal journal = new Journal();
            journal.AddEntries("What the fuck?");
            journal.AddEntries("Why FF must close ?");
            Console.WriteLine(journal);

            Journal journal2 = new Journal();
            journal2.AddEntries("Ahhh.. v pissed");
            // Check the count, it should be 3, instead of 1
            Console.WriteLine(journal2);

            var p = new Persistence();
            string path = @"c:\temp\journal.txt";
            p.SaveToFile(journal2, path, true);
            
            // System.Diagonistics.
            Process.Start(path);
        }
    }

    /// <summary>
    /// Journal class job is to keep entries only.
    /// </summary>
    class Journal
    {
        // Evaluated at run-time vs const keyword which is evaluated at compile time.
        // Const fields can only be initialized during the time of declaration.
        // Field value cannot be changed once the constructor method is executed.
        // Can only be initialized during declaration or in a constructor.
        // Cannot be changed or updated during runtime, so if you use console.readline to update ...
        // it will not work or cause a compile time error.
        private readonly List<string> enteries = new List<string>();
        
        // Static variable, will be shared. Test
        private static int Count = 0;

        public int AddEntries(string text)
        {
            enteries.Add($"{++Count}: {text}");
            return Count;
        }

        public void RemoveEntries(int index)
        {
            enteries.RemoveAt(index);
        }

        public override string ToString() => string.Join(Environment.NewLine, enteries);
    }

    /// <summary>
    /// Persistence job is to save files.
    /// </summary>
    class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (overwrite == true || File.Exists(fileName) == false)
            {
                File.WriteAllText(fileName, j.ToString());
            }
        }
    }
}
