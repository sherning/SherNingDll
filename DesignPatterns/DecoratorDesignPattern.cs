using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class DecoratorDesignPattern
    {
        public static void Main()
        {
            Client();
        }

        private static void Client()
        {
            // Structual Design Pattern
            // Extend the behavior of objects by placing theses objects 
            // into a special wrapper class.
            RegularOrder regularOrder = new RegularOrder();
            Console.WriteLine(regularOrder.CalculateTotalOrderPrice());
            Console.WriteLine();

            Preorder preorder = new Preorder();
            Console.WriteLine(preorder.CalculateTotalOrderPrice());
            Console.WriteLine();

            // On top of pre-order price, you can add an additional premium + pre-order price.
            PremiumPreOrder premiumPreOrder = new PremiumPreOrder(preorder);
            Console.WriteLine(premiumPreOrder.CalculateTotalOrderPrice());
        }
        class PremiumRegularOrder : OrderDecorator
        {
            public PremiumRegularOrder(OrderBase order) : base(order)
            {
            }

            public override double CalculateTotalOrderPrice()
            {
                Console.WriteLine($"Calculating the total price in the {nameof(PremiumRegularOrder)} class.");
                var preOrderPrice = base.CalculateTotalOrderPrice();

                Console.WriteLine("Additional discount to a regular order price.");
                return preOrderPrice * 0.9;
            }
        }

        class PremiumPreOrder : OrderDecorator
        {
            public PremiumPreOrder(OrderBase order) : base(order)
            {
            }

            public override double CalculateTotalOrderPrice()
            {
                Console.WriteLine($"Calculating the total price in the {nameof(PremiumPreOrder)} class.");
                var preOrderPrice = base.CalculateTotalOrderPrice();

                Console.WriteLine("Additional discount to a preorder price.");
                return preOrderPrice * 0.9;
            }
        }

        class OrderDecorator : OrderBase
        {
            protected OrderBase Order;

            public OrderDecorator(OrderBase order)
            {
                Order = order;
            }
            public override double CalculateTotalOrderPrice()
            {
                Console.WriteLine($"Calculating the total price in a decorator class.");
                return Order.CalculateTotalOrderPrice();
            }
        }

        /// <summary>
        /// Component Class / Base Class
        /// </summary>
        abstract class OrderBase
        {
            protected List<Product> products = new List<Product>()
            {
                new Product {Name = "Phone", Price = 587},
                new Product {Name = "Tablet", Price = 800},
                new Product {Name = "PC", Price = 1200},
            };

            public abstract double CalculateTotalOrderPrice();
        }

        // Regular and PreOrder structure very similar to strategy pattern.
        // However, instead of interfaces. Abstract class is used.
        // Below are concrete implementation of the concrete classes.
        // You can think of inheritance as partial class concept with the fact of encapsulation.
        class RegularOrder : OrderBase
        {
            public override double CalculateTotalOrderPrice()
            {
                Console.WriteLine("Calculating the total price of a regular order");
                return products.Sum(p => p.Price);
            }
        }

        class Preorder : OrderBase
        {
            public override double CalculateTotalOrderPrice()
            {
                Console.WriteLine("Calculating the total price of a preorder.");
                return products.Sum(p => p.Price) * 0.9;
            }
        }
    }

    class DecoratorDesignPatternExample
    {
        // -------------------------------------- Decorator Pattern --------------------------------------//
        //     https://www.freecodecamp.org/news/the-basic-design-patterns-all-developers-need-to-know/   //
        // -------------------------------------- Decorator Pattern --------------------------------------//

        public static void Main() => Client();
        private static void Client()
        {
            Chef chef = new Chef();

            // Client make an order and sets the receiver as the chef.
            Order order = new Order(chef, Food.Pasta);

            // Order is sent to the waiter
            Waiter waiter = new Waiter(order);
            waiter.Execute();

            order = new Order(chef, Food.Cake);
            waiter = new Waiter(order);
            waiter.Execute();
        }

        class Waiter : Command
        {
            private Order Order;
            public Waiter(Order order)
            {
                Order = order;
            }

            public void Execute()
            {
                Order.Execute();
            }
        }
        enum Food
        {
            Pasta, Cake
        }
        class Order : Command
        {
            private Chef Chef;
            private Food Food;

            public Order(Chef chef, Food food)
            {
                Chef = chef;
                Food = food;
            }

            public void Execute()
            {
                if (Food == Food.Pasta)
                {
                    Chef.CookPasta();
                }

                if (Food == Food.Cake)
                {
                    Chef.BakeCake();
                }
            }
        }

        interface Command
        {
            void Execute();
        }
        class Chef
        {
            public void CookPasta()
            {
                Console.WriteLine("Chef is cooking Chicken Alfredo..");
            }

            public void BakeCake()
            {
                Console.WriteLine("Chef is baking Chocolate Fudge Cake...");
            }
        }
    }
}
