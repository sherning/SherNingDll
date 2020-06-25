using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class FacadeDesignPattern
    {
        public static void Main()
        {
            WithoutFacadePattern();
            Console.WriteLine();
            Client();
        }

        private static void Client()
        {
            var chickenOrder = new Order
            {
                DishName = "Chicken with rice",
                DishPrice = 20.0,
                User = "User1",
                ShippingAddress = "Random street 123"
            };

            var sushiOrder = new Order
            {
                DishName = "Salmon Sushi",
                DishPrice = 52.0,
                User = "User2",
                ShippingAddress = "Random street 456"
            };

            // Simplify the user ordering process.
            // Choose which restraunt and shipping service
            var facade = new Facade(new OnlineRestraunt("Panda Express"), new ShippingService("Pussy"));

            // Place food order.
            facade.OrderFood(new List<Order>() { chickenOrder, sushiOrder });
        }

        /// <summary>
        /// Single class interface that is simple, easy to read and use.
        /// In fact you can use a static method to do this.
        /// </summary>
        class Facade
        {
            private readonly OnlineRestraunt Restraunt;
            private readonly ShippingService ShippingService;

            public Facade(OnlineRestraunt restraunt, ShippingService shippingService)
            {
                Restraunt = restraunt;
                ShippingService = shippingService;
            }

            public void OrderFood(List<Order> orders)
            {
                // Add orders to cart.
                foreach (var order in orders)
                {
                    Restraunt.AddOrderToCart(order);
                }

                Restraunt.CompleteOrders();

                foreach (var order in orders)
                {
                    ShippingService.AcceptOrder(order);
                    ShippingService.CalculateShippingExpenses();
                    ShippingService.ShipOrder();
                }
            }
        }

        private static void WithoutFacadePattern()
        {
            var restraunt = new OnlineRestraunt("WTF Food");
            var shippingService = new ShippingService("DHL");

            var chickenOrder = new Order
            {
                DishName = "Chicken with rice",
                DishPrice = 20.0,
                User = "User1",
                ShippingAddress = "Random street 123"
            };

            var sushiOrder = new Order
            {
                DishName = "Salmon Sushi",
                DishPrice = 52.0,
                User = "User2",
                ShippingAddress = "Random street 456"
            };

            restraunt.AddOrderToCart(chickenOrder);
            restraunt.AddOrderToCart(sushiOrder);
            restraunt.CompleteOrders();

            shippingService.AcceptOrder(chickenOrder);
            shippingService.CalculateShippingExpenses();
            shippingService.ShipOrder();

            shippingService.AcceptOrder(sushiOrder);
            shippingService.CalculateShippingExpenses();
            shippingService.ShipOrder();
        }

        class Order
        {
            public string DishName { get; set; }
            public double DishPrice { get; set; }
            public string User { get; set; }
            public string ShippingAddress { get; set; }
            public double ShippingPrice { get; set; }
            public override string ToString()
            {
                return string.Format("User {0} ordered {1}. The full price is {2:C2}.", 
                    User, DishName, DishPrice + ShippingPrice);
            }
        }
        class OnlineRestraunt
        {
            public string Name { get; set; }
            private readonly List<Order> Cart;
            public OnlineRestraunt(string name)
            {
                Name = name;
                Cart = new List<Order>();
            }

            public void AddOrderToCart(Order order)
            {
                Cart.Add(order);
            }

            public void CompleteOrders()
            {
                Console.WriteLine("Orders completed. Dispatch in progress");
                Console.WriteLine($"Thank you for ordering from {Name}");
            }
        }

        class ShippingService
        {
            private Order Order;
            public string Name { get; set; }
            public ShippingService(string name)
            {
                Name = name;
            }
            public void AcceptOrder(Order order)
            {
                Order = order;
            }

            public void CalculateShippingExpenses()
            {
                Order.ShippingPrice = 15.5;
            }

            public void ShipOrder()
            {
                Console.WriteLine(Order);
                Console.WriteLine($"Order is being shipped to {Order.ShippingAddress}");
                Console.WriteLine($"Thank you for using {Name} express");
            }
        }
    }
}
