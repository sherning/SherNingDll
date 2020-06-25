using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class FacetedBuilderConcept
    {
        public static void Main()
        {
            // builder concept is to build up the main object in steps.
            // There are a number of ways to use builder. extension methods.
            // recursive generic, faceted builder, fluent builder ...

            // building a car object with builder.
            Car car = new CarBuilderFacade()
                .Info
                .WithType("BMW")
                .WithColor("Portimao Blue")
                .WithNumberOfDoors(4)
                .Built
                .InCity("Leipzig")
                .AtAddress("God knows where")
                .Build();

            Console.WriteLine(car);
        }

        class Car
        {
            public string Type { get; set; }
            public string Color { get; set; }
            public int Doors { get; set; }

            public string City { get; set; }
            public string Address { get; set; }

            /// <summary>
            /// reason why you can print the car, is to use the override here.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                // format for currency. 
                return $"Car Type: {Type}\n" +
                         $"Color: {Color}\n" +
                         $"Doors: {Doors}\n" +
                         $"Manufactured in {City} {Address}";
            }
        }

        class CarBuilderFacade
        {
            protected Car Car { get; set; }

            public CarBuilderFacade()
            {
                Car = new Car();
            }

            // Method to expose protected Car property OR use implicit operator.
            public Car Build() => Car;

            // Use property to expose CarInfoBuilder. then why we need to inherit facade ?
            // Reason to use the property. 
            public CarInfoBuilder Info => new CarInfoBuilder(Car);
            public CarAddressBuilder Built => new CarAddressBuilder(Car);
        }

        class CarInfoBuilder : CarBuilderFacade
        {
            // receive an object we want to build and use through the 
            // constructor.
            public CarInfoBuilder(Car car)
            {
                Car = car;
            }

            public CarInfoBuilder WithType(string type)
            {
                Car.Type = type;
                return this;
            }
            public CarInfoBuilder WithColor(string color)
            {
                Car.Color = color;
                return this;
            }
            public CarInfoBuilder WithNumberOfDoors(int doors)
            {
                Car.Doors = doors;
                return this;
            }
        }

        class CarAddressBuilder : CarBuilderFacade
        {
            public CarAddressBuilder(Car car)
            {
                Car = car;
            }

            public CarAddressBuilder InCity(string city)
            {
                Car.City = city;
                return this;
            }

            public CarAddressBuilder AtAddress(string address)
            {
                Car.Address = address;
                return this;
            }
        }

    }
}
