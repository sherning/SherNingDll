using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    public class PassByReference
    {
        public static void Main()
        {
            List<List<int>> ListOfInts = new List<List<int>>();

            for (int i = 0; i < 5; i++)
                ListOfInts.Add(new List<int>());

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    ListOfInts[i].Add(j);
                }
            }

            foreach (var list in ListOfInts)
            {
                foreach (var num in list)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }

            List<List<int>> newListOfInts = ListOfInts;

            foreach (var list in newListOfInts)
            {
                foreach (var num in list)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }

        }
    }
}
