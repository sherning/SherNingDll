using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class CompositeDesignPattern
    {
        // Structual Pattern
        // How classes and objects are being composed to form larger structures
        // Composite design pattern consist of three parts: Component, Leaf, Composite.
        public static void Main()
        {
            Client();
        }

        /// <summary>
        /// Method to demonstrate a structual composite design pattern
        /// </summary>
        private static void Client()
        {
            // Create a graph like structure to add objects.
            SingleGift phone = new SingleGift("iPhone", 899);
            phone.CalculateTotalPrice();
            Console.WriteLine();

            // Composite Gift
            CompositeGift giftBox = new CompositeGift("Gift Box", 0);
            CompositeGift childBox = new CompositeGift("Child Box", 0);

            giftBox.Add(new SingleGift("TruckToy", 289.5));
            giftBox.Add(new SingleGift("Plain Toy", 587));
            childBox.Add(new SingleGift("Soldier Toy", 199.99));

            // This creates a tree structure.
            giftBox.Add(childBox);

            Console.WriteLine($"\nTotal price of this composite present is: {giftBox.CalculateTotalPrice():C2}");
        }

        /// <summary>
        /// Component part of Composite Design Pattern
        /// </summary>
        abstract class GiftBase
        {
            protected string Name;
            protected double Price;

            public GiftBase(string name, double price)
            {
                Name = name;
                Price = price;
            }

            public abstract double CalculateTotalPrice();
        }

        interface IGiftOperations
        {
            void Add(GiftBase gift);
            void Remove(GiftBase gift);
        }

        /// <summary>
        /// Composite component of Composite Design Pattern
        /// </summary>
        class CompositeGift : GiftBase, IGiftOperations
        {
            private List<GiftBase> Gifts;

            public CompositeGift(string name, double price)
                :base(name,price) // send to the base class constructor.
            {
                Gifts = new List<GiftBase>();
            }

            public void Add(GiftBase gift)
            {
                Gifts.Add(gift);
            }
            
            public void Remove(GiftBase gift)
            {
                Gifts.Remove(gift);
            }

            public override double CalculateTotalPrice()
            {
                double total = 0.0;
                Console.WriteLine($"{Name} contains the following products with prices");

                // this will prove that each gift : giftbase is a unique and different instance.
                foreach (var gift in Gifts)
                {
                    total += gift.CalculateTotalPrice();
                }

                return total;
            }
        }

        /// <summary>
        /// Leaf component of Composite Design Patterns
        /// </summary>
        class SingleGift : GiftBase
        {
            public SingleGift(string name, double price)
                :base(name,price)
            {
                // use back the base class constructor and its fields.
                // each instance will have have its own constructor and fields.
            }
            public override double CalculateTotalPrice()
            {
                Console.WriteLine($"{Name} with the price {Price:C2}");
                return Price;
            }
        }
    }
}
