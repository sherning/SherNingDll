using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class CommandDesignPattern
    {
        // Behavioral Design Pattern
        // Turns a request into an object 
        // which contains all the information about the request
        public static void Main()
        {
            // Command design is used when we want to delay or queue
            // a request execution or when we want to keep track of our operations.
            Client();
        }

        private static void Client()
        {
            ModifyPrice modifyPrice = new ModifyPrice();
            Product product = new Product { Name = "Phone", Price = 500 };

            // Fluent API
            ProductCommand command1 = ProductCommand
                .Command
                .SetProduct(product)
                .SetPriceAction(PriceAction.Increase)
                .SetAmount(100);

            ProductCommand command2 = ProductCommand
                .Command
                .SetProduct(product)
                .SetPriceAction(PriceAction.Increase)
                .SetAmount(50);

            ProductCommand command3 = ProductCommand
               .Command
               .SetProduct(product)
               .SetPriceAction(PriceAction.Decrease)
               .SetAmount(25);

            // Extract all the request details into a special class - Command.
            Execute(modifyPrice, command1, command2, command3);

            // The actual object product is modified, properties changed.
            Console.WriteLine(product);
            Console.WriteLine();

            modifyPrice.UndoActions();
            Console.WriteLine(product);
        }

        // This method works like a Facade method, to simplify the interface.
        private static void Execute(ModifyPrice modifyPrice, params ICommand[] commands)
        {
            foreach (var command in commands)
            {
                modifyPrice.SetCommand(command);
                modifyPrice.Invoke();
            }
        }

        interface ICommand
        {
            void ExecuteAction();
            void UndoAction();
        }

        enum PriceAction
        {
            Increase, Decrease
        }

        /// <summary>
        /// This class will act as Invoker, and save all the past commands.
        /// </summary>
        class ModifyPrice
        {
            // Decouple classes that invoke operations from clases that perform operations.
            private readonly List<ICommand> Commands;
            private ICommand Command;
            public ModifyPrice()
            { 
                Commands = new List<ICommand>();
            }

            /// <summary>
            /// Alternatively, you can use a property to set the command.
            /// </summary>
            /// <param name="command"></param>
            public void SetCommand(ICommand command)
            {
                Command = command;
            }

            public void Invoke()
            {
                Commands.Add(Command);

                // Invoke the command.
                Command.ExecuteAction();
            }
            public void UndoActions()
            {
                // Note that LINQ will mutate the list. Whereas, Enumerable will preserve state.
                foreach (var command in Enumerable.Reverse(Commands))
                {
                    command.UndoAction();
                }
            }
        }

        /// <summary>
        /// productCommand has all the information about the request and based on that 
        /// executes required action.
        /// </summary>
        class ProductCommand : ICommand
        {
            // Class fields and readonly, to be initialized during constructor call.
            private Product Product;
            private PriceAction PriceAction;
            private int Amount;
            public bool IsCommandExecuted { get; private set; }

            // Each time get is called, it returns a new ProductCommand.
            public static ProductCommand Command => new ProductCommand();
            public ProductCommand()
            {

            }

            public ProductCommand SetProduct(Product product)
            {
                Product = product;
                return this;
            }
            public ProductCommand SetPriceAction(PriceAction action)
            {
                PriceAction = action;
                return this;
            }
            public ProductCommand SetAmount(int amount)
            {
                Amount = amount;
                return this;
            }
            public ProductCommand(Product product, PriceAction priceAction, int amount)
            {
                Product = product;
                PriceAction = priceAction;
                Amount = amount;
            }
            public void ExecuteAction()
            {
                if (PriceAction == PriceAction.Increase)
                {
                    Product.IncreasePrice(Amount);
                    IsCommandExecuted = true;
                }
                else
                {
                    IsCommandExecuted = Product.DecreasePrice(Amount);
                }
            }
            public void UndoAction()
            {
                if (IsCommandExecuted == false)
                {
                    return;
                }

                if (PriceAction == PriceAction.Increase)
                {
                    Product.DecreasePrice(Amount);
                }
                else
                {
                    Product.IncreasePrice(Amount);
                }
            }
        }
    }
}
