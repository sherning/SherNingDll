using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// https://www.tutorialsteacher.com/csharp/csharp-covariance-and-contravariance
    /// </summary>
    class CovarianceAndContraVariance
    {
        public static void Main()
        {
            Covariance();
            ContraVariance();
        }

        // Super class for this class hierarchy
        class Small { }
        // Base class for Big is small
        class Big : Small { }
        // Base class for Bigger is Big
        class Bigger : Big { }

        // Basic concept to understanding covariance and contra variance.
        private static void ClassInitialization()
        {
            // Base class can hold derived class but derived class cannot hold base class.
            Small smsm = new Small();
            Small smbg = new Big();
            Small smbgg = new Bigger();
            Big bgbgg = new Bigger();
            // Big bgsm = new Small(); // this will be an error.
        }

        // does this work with interface?
        interface A { }
        interface B : A { }
        interface C : B { }
        class X : A { }
        class Y : B { }
        class Z : C { }
        private static void InterfaceInitialization()
        {
            A ax = new X();
            A ay = new Y();
            A az = new Z();
            B bz = new Z();
        }

        #region Understanding Covariance
        private static void Covariance()
        {
            // **Covariance enables you to pass a derived type 
            //   when a base type is expected.
            Console.WriteLine("Covariance Test");
            covarDel del = Method1;
            Small sm1 = del(new Big());

            del = Method2;
            Small sm2 = del(new Big());

            del = Method3;
            Small bg1 = del(new Big());

            // in conclusion, convariance allows you to assign a method 
            // to the delegate that has a less derived return type
        }

        /// <summary>
        /// Rule: Can accept Big or Bigger if small is expected.
        /// </summary>
        /// <param name="mc"></param>
        /// <returns></returns>
        delegate Small covarDel(Big mc);

        // Notice that Method 1 returns Big(derived class of Small)
        // whereas Method 2 has the same signature as the delegate.
        static Big Method1(Big big)
        {
            Console.WriteLine("Method 1");
            return new Big();
        }
        static Small Method2(Big big)
        {
            Console.WriteLine("Method 2");
            return new Small();
        }
        static Bigger Method3(Big big)
        {
            Console.WriteLine("Method 3");
            return new Bigger();
        }
        #endregion

        #region Understanding Contra Variance
        // Method 4 method parameter expects a Small.
        // But delegate signature expects a Big
        static Small Method4(Small sml)
        {
            Console.WriteLine("Method 4");
            return new Small();
        }
        static Big Method5(Small sml)
        {
            Console.WriteLine("Method 5");
            return new Big();
        }
        private static void ContraVariance()
        {
            // Contra variance is applied to parameters. 
            // Contravariance allows a method with the parameter of a base class to
            // be assigned to a delegate that expects the parameter of a derived class.

            Console.WriteLine("\nContra Variance test");
            covarDel del = Method1;
            del += Method2;
            del += Method3;
            del += Method4;
            del += Method5;

            Small sm = del(new Big());
        }
        #endregion
    }
}
