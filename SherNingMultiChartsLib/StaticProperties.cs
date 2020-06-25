using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    class StaticProperties
    {
        public static void Main()
        {
            Test.Creed = "Shared by all?";

            Test test1 = new Test();
            test1.Name = "Test 1";

            Test test2 = new Test();
            test2.Name = "Test 2";

            Console.WriteLine("Calling from static properties.");
            Console.WriteLine(Test.Creed);
        }
       
    }

    class Test
    {
        public string Name { get; set; }
        public static string Creed { get; set; }
    }
}
