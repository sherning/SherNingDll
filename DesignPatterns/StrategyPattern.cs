using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// side track, not, not a bad idea of using partial classes to separate static methods
    /// from normal aka instance methods. And, constructor is a special method, that runs,
    /// when the class is instantiated, which is activated by the keyword 'new'.
    /// </summary>
    class StrategyPattern
    {
        /// <summary>
        /// Method to run the code.
        /// </summary>
        public static void Main()
        {
            
            PointOfSales posOne = new PointOfSales(null);
            posOne.CurrentStrategy = new HighDiscount();
            posOne.PrintBill(1000);
        }

        #region Point of Sales
        /// <summary>
        /// The key point of using Strategy Pattern is to NOT make changes to PointOfSales class.
        /// </summary>
        class PointOfSales
        {
            public string CustomerName { get; set; }
            public int BillAmount { get; set; }
            public IStrategy CurrentStrategy { private get; set; }

            public PointOfSales(IStrategy currentStrategy)
            {
                CurrentStrategy = currentStrategy;
            }

            public void PrintBill(int billAmount)
            {
                Console.WriteLine("Total bill amount: $" + CurrentStrategy.GetFinalBill(billAmount));
            }
        }
        #endregion

        #region Discount Strategy
        public interface IStrategy
        {
            int GetFinalBill(int billAmount);
        }

        /// <summary>
        /// Returns a discount of 10% of billable amount
        /// </summary>
        private class LowDiscount : IStrategy
        {
            public int GetFinalBill(int billAmount)
            {
                return (int)(billAmount * 0.9);
            }
        }

        /// <summary>
        /// Returns a discount of 50% of billable amount
        /// </summary>
        private class HighDiscount : IStrategy
        {
            public int GetFinalBill(int billAmount)
            {
                return (int)(billAmount * 0.5);
            }
        }

        /// <summary>
        /// Returns no discount.
        /// </summary>
        private class NoDiscount : IStrategy
        {
            public int GetFinalBill(int billAmount)
            {
                return billAmount;
            }
        }
        #endregion

    }
}
