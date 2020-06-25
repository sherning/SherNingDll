using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// DuckSim default access specifier is internal.
    /// This practice session is to learn access modifiers.
    /// </summary>
    class DuckSim
    {
        public static void Main()
        {
            MallardDuck md = new MallardDuck();
            md.PerformFly();
            md.SetFlyBehavior(new FlyRocketPowered());
            md.PerformFly();
            md.flyBehavior = new FlyingWithoutWings();
            md.PerformFly();
        }

        class ModelDuck : Duck
        {
            public ModelDuck()
            {
                base.flyBehavior = new FlyWithWings();
                base.quackBehavior = new Quack();
            }
            // Overriding with new keyword.
            new public void Show()
            {

            }
            public override void Display() => Console.WriteLine("I am a model duck.");
        }

        class MallardDuck : Duck
        {
            public MallardDuck()
            {
                flyBehavior = new FlyWithWings();

                // Base in this case referes to the parent class.
                base.quackBehavior = new Quack();
            }

            public override void Display()
            {
                Console.WriteLine("I am a real Mallard Duck!");
            }
        }
        /// <summary>
        /// Abstract class, so that users will not instaniate duck class.
        /// </summary>
        private abstract class Duck
        {
            // Can only be accessed within the Duck class ONLY.
            // Private string .. 

            // This can only be accessed within child classes and the parent.
            // This information is not available to the public
            protected string ProtectedString = "This string is protected";

            // This information is available to everyone.
            public string PublicString = "This string is public";

            // Two instance variables.
            // Accessor setted to private protected, so only dervied classes can access.
            // private means, Duck object is hidden. Protected means, derived class can access.
            // Create a protected variable which will be instatiated inside the child class constructor
            public FlyBehavior flyBehavior { get; set; }
            
            private protected QuackBehavior quackBehavior;

            /// <summary>
            /// Method to change fly behavior on the fly
            /// </summary>
            /// <param name="newFlyBehavior"></param>
            public void SetFlyBehavior(FlyBehavior newFlyBehavior)
            {
                flyBehavior = newFlyBehavior;
            }

            public void SetQuackBehavior(QuackBehavior newQuackBehavior)
            {
                quackBehavior = newQuackBehavior;
            }

            protected void PerformQuack()
            {
                quackBehavior.Quacking();
            }

            public void PerformFly()
            {
                flyBehavior.Fly();
            }

            public abstract void Display();
            public void Show()
            {

            }
        }
        /// <summary>
        /// Member interface of DuckSim or nested interface.
        /// </summary>
        public interface FlyBehavior
        {
            void Fly();
        }

        /// <summary>
        /// Nested interface and classes can have all access modifiers.
        /// Which is defaulted to private.
        /// </summary>
        public interface QuackBehavior
        {
            void Quacking();
        }

        class FlyRocketPowered : FlyBehavior
        {
            public void Fly()
            {
                Console.WriteLine("Flying by rocket power");
            }
        }

        class Quack : QuackBehavior
        {
            public void Quacking()
            {
                Console.WriteLine("Quack quack quack !");
            }
        }
        class FlyWithWings : FlyBehavior
        {
            public void Fly()
            {
                Console.WriteLine("I am flying!");
            }
        }

        class FlyingWithoutWings : FlyBehavior
        {
            public void Fly()
            {
                Console.WriteLine("I'm flying without wings!");
            }
        }


    }
}
