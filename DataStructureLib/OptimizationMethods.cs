using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class OptimizationMethods
    {
        public static void Main()
        {
            KnapSackProblem(15);
        }

        // Greedy Method - Learning to solve optimization problems.
        // Knapsack Problem. 
        private static void KnapSackProblem(int weightConstraint = 15)
        {
            int totalWeight = 0;
            double totalProfits = 0.0;
            int count = 0;

            Product[] knapSack =
            {
                new Product("Flour", 10, 2),
                new Product("Potatoes", 5, 3),
                new Product("Beans", 15, 5),
                new Product("Tomatoes", 7, 7),
                new Product("Chilli", 6, 1),
                new Product("Sweet Potatoes", 18, 4),
                new Product("Onions", 3, 1)
            };

            // Sort based on highest Profit to weight ratio to lowest.
            Array.Sort(knapSack);

            Product[] temp = new Product[knapSack.Length];

            foreach (Product product in knapSack)
            {
                if (totalWeight <= weightConstraint &&
                    product.Weight <= (weightConstraint - totalWeight))
                {
                    // add the product to total weight
                    totalWeight += product.Weight;
                    totalProfits += product.Profits;
                }
                else
                {
                    // products that were not selected.
                    temp[count++] = product;
                }
            }

            foreach (Product product in temp)
            {
                if (product == null) break;

                if (totalWeight < weightConstraint &&
                     product.Weight <= (weightConstraint - totalWeight))
                {
                    totalWeight += product.Weight;
                    totalProfits += product.Profits;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("KnapSack Optimised Solution: ");
            Console.WriteLine("Total profits: " + totalProfits);
            Console.WriteLine("Total weight: " + totalWeight);
        }

        class Product : IComparable<Product>
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public double Profits { get; set; }
            public double ProfitWeight => CalcProfitOverWeight();
            public Product(string name, double profits, int weight)
            {
                Name = name;
                Profits = profits;
                Weight = weight;
            }

            private double CalcProfitOverWeight() => Profits / Weight;
            public override string ToString() =>
                string.Format($"{Name} with a Profit weight ratio of {ProfitWeight}");

            public int CompareTo(Product product)
            {
                if (product.ProfitWeight > ProfitWeight) return 1;
                else if (product.ProfitWeight < ProfitWeight) return -1;
                else return 0;
            }
        }
    }
}
