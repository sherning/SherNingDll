using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Open close principle states that a software should be open for extension,
    /// but closed for modification.
    /// </summary>
    class OpenClosePrinciple
    {
        public static void Main()
        {
            var apple = new Product("Apple", ProductColor.red, Productsize.small);
            var durian = new Product("Durian", ProductColor.green, Productsize.medium);
            var watermelon = new Product("Watermelon", ProductColor.green, Productsize.large);
            var soursop = new Product("Soursop", ProductColor.green, Productsize.small);
            var blueberry = new Product("Blue Berries", ProductColor.blue, Productsize.small);

            // Array syntax.
            Product[] fruitBasket = { apple, durian, watermelon, soursop, blueberry };

            // If can use LINQ. use linq vs demo, we will use class specifications
            ProductFilter filter = new ProductFilter();

            var smallFruits = from fruit in fruitBasket
                              where fruit.Color == ProductColor.green
                              select fruit;

            // Using LINQ to test
            Console.WriteLine("Using LINQ to filter fruits by color.");
            foreach (var fruit in smallFruits)
            {
                Console.Write(fruit.Name + " ");
            }

            // Using Product filter to test
            Console.WriteLine("\n\nUsing product filter to filter fruits by color.");
            foreach (var fruit in filter.Filter(fruitBasket,new ColorSpecification(ProductColor.green)))
            {
                Console.Write(fruit.Name + " ");
            }
            Console.WriteLine();

            // Using product filter to filter both size and color
            Console.WriteLine("\nUsing product filter to filter both size and color.");
            foreach (var fruit in filter.Filter(fruitBasket,
                new AndSpecification<Product>(new ColorSpecification(ProductColor.green)
                                        ,new SizeSpecification(Productsize.medium))))
            {
                Console.Write(fruit.Name + " ");
            }
            Console.WriteLine();

        }
    }

    #region Interfaces
    interface ISpecification<T>
    {
        // Interface cannot have static methods.
        // By definition, interface is a contract for instances to fulfil.
        bool IsSatisfied(T item);
    }

    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    #endregion

    #region Specification Class
    // All Specification class must implement ISpecification interface.

    class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> First;
        private ISpecification<T> Second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            First = first;
            Second = second;
        }

        // Using interfaces to ensure implementation of the method.
        public bool IsSatisfied(T item) => First.IsSatisfied(item) && Second.IsSatisfied(item);
    }
    class ColorSpecification : ISpecification<Product>
    {
        private ProductColor Color;

        public ColorSpecification(ProductColor color)
        {
            Color = color;
        }

        // If item color == specified color, return true.
        public bool IsSatisfied(Product item) => item.Color == Color;
    }

    class SizeSpecification : ISpecification<Product>
    {
        private Productsize Size;
        public SizeSpecification(Productsize size)
        {
            Size = size;
        }

        public bool IsSatisfied(Product item) => item.Size == Size;
    }
    #endregion

    #region Product Filter
    class ProductFilter : IFilter<Product>
    {
        // Note: static class cannot implement interface.
        // A class that implements interface needs to implement them all as instance methods.
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
            {
                // spec is an instance, that must implement IsSatisfied.
                // Otherwise, the below method will not work.
                if (spec.IsSatisfied(item))
                {
                    // without yield keyword, you need to create a temp list to iterate.
                    // custom iteration here.
                    yield return item;
                }
            }
        }
    }
    #endregion

    #region Product Class
    enum Productsize
    { small, medium, large }

    enum ProductColor
    { green, red, blue }

    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ProductColor Color { get; set; }
        public Productsize Size { get; set; }

        public Product()
        {

        }
        public Product(string name, ProductColor color, Productsize size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
        public void IncreasePrice(int amount)
        {
            Price += amount;
            Console.WriteLine($"The price for the {Name} has been increased by {amount:c2}");
        }
        public bool DecreasePrice(int amount)
        {
            if (amount < Price)
            {
                Price -= amount;
                Console.WriteLine($"THe price for the {Name} has been decrease by {amount:c2}");
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Current price for the {Name} product is {Price:C2}";
        }
    }
    #endregion



}
