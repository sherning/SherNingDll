using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterSpace;

namespace OddBallProjects
{
    /// <summary>
    /// Understanding this for design patterns to understand MC.
    /// </summary>
    public class UnderstandingInterface
    {
        public static void Main()
        {
            iMathBasics mB = MathFactory.LaunchMathComponents();
            Console.WriteLine(mB.Add(10, 10));

            // Example of abstraction.
            iMathAdvance mA = MathFactory.LaunchMathComponents();
            Console.WriteLine(mA.divi(10,10));

            // Example of multiple inheritance for upgrade.
            iMathVersion2 mV = MathFactory.LaunchMathComponents();
            mV.Log(10);
        }
    }
}

namespace InterSpace
{
    public class MathFactory
    {
        // Example of decoupling software.
        public static iMathVersion2 LaunchMathComponents()
        {
            return new MathLib();
        }
    }

    class MathLib : iMathAdvance, iMathVersion2
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int divi(int x, int y)
        {
            return x / y;
        }

        public int Log(int x)
        {
            return (int)Math.Log(x);
        }

        public int multi(int x, int y)
        {
            return x * y;
        }

        // accepts lambda expression.
        public int Sub(int x, int y) => x - y;
    }

    /// <summary>
    /// Use interface as an explicit contract where the caller and callee are decoupled from each other.
    /// Impact analysis - all the red lines if you change something or,
    /// Change detection - compiler will go crazy with all the red lines
    /// Runtime will have less issues.
    /// 
    /// Another use is abstraction, show only what is needed
    /// 
    /// Multiple inheritence is a by product or improving a version.
    /// </summary>
    public interface iMathBasics
    {
        // Implementing a contract between the user and the software.
        // what user wants to use, it what you MUST implement.
        int Add(int x, int y);
        int Sub(int x, int y);
    }
    public interface iMathAdvance : iMathBasics
    {
        int multi(int x, int y);
        int divi(int x, int y);
    }

    public interface iMathVersion2 : iMathAdvance
    {
        int Log(int x);
    }
}

