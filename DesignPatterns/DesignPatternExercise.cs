using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Learn and apply a combination of design patterns.
    /// To understand how a design pattern work, look at client method.
    /// </summary>
    class DesignPatternExercise
    {
        // In this exercise, we want to pre-order playstation 4 with games bundles
        // Together with Final fantasy 7 remake preorder.
        // Pre-order discount - Decorator Pattern
        // Bundle - Composite Pattern
        // change in price - command pattern
        // simplify interface - facade pattern
        // returns the total price and shipping information.
        // Keep track of commands.
        // Must comply with open close principle.

        public static void Main()
        {
            Client();
        }

        private static void Client()
        {
            // Factory + builder Method.
            CreateGameBundle
                .NewGameBundle // Static Factory Method.
                // Create a few game titles
                .NameOf("Most Popular Games")
                .AddNewRPG("Final Fantasy 7: Remake", Edition.Limited)
                .AddNewRPG("Final Fantasy X", Edition.Normal)
                .AddNewFPS("Medal of Honor", Edition.Normal)
                .AddNewFPS("Shadow of the Tomb Raider", Edition.Limited)
                .AddNewFPS("Half-Life 2", Edition.Normal)
                .AddConsole(Edition.Limited)
                // Put them inside bundle
                .DisplayList()
                .CalculateTotal()
                // Adjust prices
                .NewPricing
                .ChangeItemPrice(1, PriceAction.Increase, 10)
                .ChangeItemPrice(2, PriceAction.Decrease, 20)
                .DisplayList()
                .CalculateTotal()
                .BlankLine()
                .NewReleaseOrder
                .CreateRegularOrder()
                .CreatePremiumOrder()
                .CalculateOrderTotal()
                .BlankLine()
                .NewReleaseOrder 
                // Ship out
                .ChangeDiscount(PriceAction.Increase, 0.3)
                .CalculateOrderTotal();
        }

        #region Facade and Factory Method
        class GameBundleOrder : CreateGameBundle
        {
            public GameBundleOrder(Dictionary<int, IPlaystationPricing> newBundle, CustomBundle custom, 
                string name, CommandCenter cc, OrderBase newOrder)
            {
                NewOrder = newOrder;
                NewCommandCenter = cc;
                Name = name;
                NewBundle = newBundle;
                CustomBundle = custom;
            }

            public GameBundleOrder CreateRegularOrder()
            {
                NewOrder = new RegularOrder(CustomBundle);
                Console.WriteLine("New Regular Order Created");
                return this;
            }

            public GameBundleOrder CreatePreOrder(double discount = 0.1)
            {
                NewOrder = new Preorder(CustomBundle, discount);
                Console.WriteLine("New Preorder Created");
                return this;
            }

            public GameBundleOrder CreatePremiumOrder(double discount = 0.1)
            {
                if (NewOrder == null)
                {
                    Console.WriteLine("Create pre-order or regular order first.");
                    return this;
                }

                PremiumOrder newPremium = new PremiumOrder(NewOrder, discount);
                Console.WriteLine("New Premium Order Created");
                NewOrder = newPremium;
                return this;
            }

            public GameBundleOrder ChangeDiscount(PriceAction priceaction, double discount = 0.1)
            {
                if (NewOrder == null)
                {
                    Console.WriteLine("Please create a new order first");
                    return this;
                }

                if (priceaction == PriceAction.Increase)
                {
                    NewOrder.IncreaseDiscount(discount);
                }
                else
                {
                    NewOrder.DecreaseDiscount(discount);
                }
                return this;
            }
        }

        class GameBundlePricing : CreateGameBundle
        {
            public GameBundlePricing(Dictionary<int, IPlaystationPricing> newBundle, CustomBundle custom, 
                string name, CommandCenter cc, OrderBase newOrder)
            {
                NewOrder = newOrder;
                NewCommandCenter = cc;
                Name = name;
                NewBundle = newBundle;
                CustomBundle = custom;
            }

            public GameBundlePricing ChangeItemPrice(int key, PriceAction priceAction, int amount)
            {
                ICommand modifyPrice = new ModifyPrice(NewBundle[key], priceAction, amount);
                NewCommandCenter.SetCommand(modifyPrice);
                NewCommandCenter.Invoke();
                return this;
            }

            public GameBundlePricing UndoActions()
            {
                NewCommandCenter.UndoAllActions();
                return this;
            }
            
        }
        class CreateGameBundle
        {
            protected Dictionary<int, IPlaystationPricing> NewBundle;
            protected CustomBundle CustomBundle;
            protected CommandCenter NewCommandCenter;
            protected OrderBase NewOrder;
            protected int Count;
            public string Name { get; set; }
            public static CreateGameBundle NewGameBundle => new CreateGameBundle();
            public GameBundlePricing NewPricing => new GameBundlePricing(NewBundle, CustomBundle, 
                Name, NewCommandCenter, NewOrder);
            public GameBundleOrder NewReleaseOrder => new GameBundleOrder(NewBundle, CustomBundle, 
                Name, NewCommandCenter, NewOrder);
            public CreateGameBundle()
            {
                NewCommandCenter = new CommandCenter();
                NewBundle = new Dictionary<int, IPlaystationPricing>();
                CustomBundle = new CustomBundle("Default");
                Count = 0;
            }
            public CreateGameBundle NameOf(string name)
            {
                Name = name;
                return this;
            }
            public CreateGameBundle AddNewRPG(string name, Edition edition)
            {
                Game newRPG = new RolePlayingGame(name, edition);
                NewBundle.Add(++Count, newRPG);
                CustomBundle.AddGames(newRPG);
                return this;
            }
            public CreateGameBundle AddNewFPS(string name, Edition edition)
            {
                Game newFPS = new FirstPersonShooter(name, edition);
                NewBundle.Add(++Count, newFPS);
                CustomBundle.AddGames(newFPS);
                return this;
            }
            public CreateGameBundle AddConsole(Edition edition)
            {
                Playstation newConsole = new Playstation(edition);
                NewBundle.Add(++Count, newConsole);
                CustomBundle.AddMachine(newConsole);
                return this;
            }
            public CreateGameBundle DisplayList()
            {
                Console.WriteLine($"\n====== {Name} Bundle ======");
                foreach (var item in NewBundle)
                {
                    Console.WriteLine($"Item {item.Key}: {item.Value}");
                }

                return this;
            }
            public CreateGameBundle CalculateTotal()
            {
                CustomBundle.CalculateTotal();
                return this;
            }

            public CreateGameBundle CalculateOrderTotal()
            {
                if (NewOrder == null)
                {
                    return this;
                }
                else
                {
                    NewOrder.CalculateTotalPrice();
                    return this;
                }
            }

            public CreateGameBundle BlankLine()
            {
                Console.WriteLine(Environment.NewLine);
                return this;
            }
        }
        #endregion

        #region Command to control price and discounts
        class CommandFacade
        {
            private CommandCenter Modifications;
            private ICommand[] Commands;
            public CommandFacade(CommandCenter modifications, params ICommand[] commands)
            {
                Modifications = modifications;
                Commands = commands;

                foreach (var command in Commands)
                {
                    Modifications.SetCommand(command);
                    Modifications.Invoke();
                }
            }
        }
       
        /// <summary>
        /// Takes a ICommand and executes it.
        /// </summary>
        class CommandCenter
        {
            private readonly List<ICommand> Commands;
            private ICommand Command;
            public CommandCenter()
            {
                Commands = new List<ICommand>();
            }

            public void SetCommand(ICommand command)
            {
                Command = command;
            }

            public void Invoke()
            {
                Commands.Add(Command);
                Command.ExecuteCommand();
            }

            public void UndoAllActions()
            {
                foreach (var command in Enumerable.Reverse(Commands))
                {
                    command.UndoCommands();
                }
            }
        }

        /// <summary>
        /// Make changes to bundle discounts
        /// </summary>
        class ModifyDiscount : ICommand
        {
            private IBaseOrderDiscount DiscountCommand;
            private PriceAction PriceAction;
            private double Discount;
            public bool IsCommandExecuted { get; private set; }
            public ModifyDiscount(IBaseOrderDiscount commands, PriceAction action, double discount)
            {
                DiscountCommand = commands;
                PriceAction = action;
                Discount = discount;
            }
            public void ExecuteCommand()
            {
                if (PriceAction == PriceAction.Increase)
                {
                    IsCommandExecuted = DiscountCommand.IncreaseDiscount(Discount);
                }
                else
                {
                    IsCommandExecuted = DiscountCommand.DecreaseDiscount(Discount);
                }
            }

            public void UndoCommands()
            {
                if (IsCommandExecuted == false) return;

                if (PriceAction == PriceAction.Decrease)
                {
                    IsCommandExecuted = DiscountCommand.IncreaseDiscount(Discount);
                }
                else
                {
                    IsCommandExecuted = DiscountCommand.DecreaseDiscount(Discount);
                }
            }
        }

        /// <summary>
        /// Make changes to games or machine pricing
        /// </summary>
        class ModifyPrice : ICommand
        {
            private IPlaystationPricing PlaystationItem;
            private PriceAction PriceAction;
            private int Amount;
            public bool IsCommandExecuted { get; private set; }
            public ModifyPrice(IPlaystationPricing psItems, PriceAction priceAction, int amount)
            {
                PlaystationItem = psItems;
                PriceAction = priceAction;
                Amount = amount;
            }
            public void ExecuteCommand()
            {
                if (PriceAction == PriceAction.Increase)
                {
                    IsCommandExecuted = PlaystationItem.IncreasePrice(Amount);
                }
                else
                {
                    IsCommandExecuted = PlaystationItem.DecreasePrice(Amount);
                }
            }

            public void UndoCommands()
            {
                if (IsCommandExecuted == false)
                {
                    return;
                }

                if (PriceAction == PriceAction.Increase)
                {
                    PlaystationItem.DecreasePrice(Amount);
                }
                else
                {
                    PlaystationItem.IncreasePrice(Amount);
                }
            }
        }

        interface ICommand
        {
            void ExecuteCommand();
            void UndoCommands();
        }
        interface IPlaystationPricing
        {
            bool IncreasePrice(int amount);
            bool DecreasePrice(int amount);
        }
        interface IBaseOrderDiscount
        {
            bool IncreaseDiscount(double discount);
            bool DecreaseDiscount(double discount);
        }
        enum PriceAction
        {
            Increase, Decrease
        }
        #endregion

        #region Create Preorder and Regular Bundles
        enum Order
        {
            PreOrder, Regular
        }

        // Wrapper for all orders for premium customers.
        class PremiumOrder : OrderDecorator
        {
            public PremiumOrder(OrderBase orderbase, double discount = 0.1) : base(orderbase)
            {
                Discount = 1.0 - discount;
            }

            public override double CalculateTotalPrice()
            {
                Console.WriteLine($"Additional Discount to a premium {OrderBase.GetType().Name} price");
                double total = base.CalculateTotalPrice() * Discount;

                Console.WriteLine($"Total Price after Premium Discount: {total:C2}");
                // Use command interface to change the discount.
                return total;
            }
        }

        abstract class OrderDecorator : OrderBase
        {
            protected OrderBase OrderBase;
            public OrderDecorator(OrderBase orderBase)
            {
                OrderBase = orderBase;
            }

            public override double CalculateTotalPrice()
            {
                double total = OrderBase.CalculateTotalPrice();
                Console.WriteLine($"Order before discount: {total:C2}");
                return total;
            }
        }

        abstract class OrderBase : IBaseOrderDiscount
        {
            protected IGameBundle OrderBundle;
            protected Order Order;
            protected double Discount;
            public abstract double CalculateTotalPrice();

            public bool DecreaseDiscount(double discount)
            {
                Console.WriteLine($"Current Discount: {(1 - Discount):P0}");
                if (discount > 0 && discount < 1.0)
                {
                    Discount += discount;
                    Console.WriteLine($"Decrease NET discount by {discount:P0} to {1-Discount:P0}");
                    return true;
                }
                return false;
            }

            public bool IncreaseDiscount(double discount)
            {
                Console.WriteLine($"Current Discount: {1-Discount:P0}");
                if (discount > 0 && discount < 1.0)
                {
                    Discount -= discount;
                    Console.WriteLine($"Increase NET discount by {discount:P0} to {1-Discount:P0}");
                    return true;
                }
                return false;
            }
        }

        class RegularOrder : OrderBase
        {
            public RegularOrder(IGameBundle basicBundle)
            {
                OrderBundle = basicBundle;
                Order = Order.Regular;
            }

            public override double CalculateTotalPrice()
            {
                double total = OrderBundle.CalculateTotalPrice();
                Console.WriteLine($"Total cost for Regular Orders: {total:C2}");
                return total;
            }
        }

        class Preorder : OrderBase
        {
            public Preorder(IGameBundle basicBundle, double discount = 0.1)
            {
                OrderBundle = basicBundle;
                Discount = 1.0 - discount;
                Order = Order.PreOrder;
            }

            public override double CalculateTotalPrice()
            {
                double total = OrderBundle.CalculateTotalPrice() * Discount;
                Console.WriteLine($"Total cost for Pre-Order after discount: {total:C2}");
                return total;
            }
        }
        #endregion

        #region Create Bundle with Composite Design
        // for this demo, abstract is just to prevent users from instaniating this base class.
        abstract class BasicBundle : IGameBundle
        {
            protected string Name;
            protected List<IPlayStation> BundleSet;

            //public abstract double CalculateTotalPrice();
            public double CalculateTotalPrice()
            {
                double total = 0.0;
                foreach (var item in BundleSet)
                {
                    total += item.CalculatePrice();
                    Console.WriteLine(item);
                }

                //Console.WriteLine($"Total Price for {Name} is {total:C2}\n");
                return total;
            }

            // no choice ???
            // public List<IPlayStation> GetBundleSet() => BundleSet;
        }

        interface IGameBundle
        {
            // Because base class has this method, child class need not implement.
            double CalculateTotalPrice();
        }


        class CustomBundle : BasicBundle
        {
            public CustomBundle(string nameOfBundle)
            {
                Name = nameOfBundle;
                BundleSet = new List<IPlayStation>();
            }

            public void AddMachine(Playstation machine)
            {
                BundleSet.Add(machine);
            }

            public void AddGames(params Game[] games)
            {
                foreach (var game in games)
                {
                    BundleSet.Add(game);
                }
            }

            public void Remove(IPlayStation item)
            {
                BundleSet.Remove(item);
            }

            public void CalculateTotal()
            {
                double total = 0.0;
                foreach (var item in BundleSet)
                {
                    total += item.CalculatePrice();
                }
                Console.WriteLine($"Total cost for custom bundle: {total:C2}");
            }
        }

        class FinalFantasyBundle : BasicBundle
        {
            public FinalFantasyBundle()
            {
                Name = "Final Fantasy Playstation 4 Bundle";
                BundleSet = new List<IPlayStation>
                {
                    new Playstation(Edition.Limited),
                    new RolePlayingGame("Final Fantasy VII",Edition.Limited)
                };
            }
        }

        // So long as it inherits the method of the interface.
        class TombRaiderBundle : BasicBundle, IGameBundle
        {
            public TombRaiderBundle()
            {
                Name = "Tomb Raider Playstation 4 Bundle";
                BundleSet = new List<IPlayStation>
                {
                    new Playstation(Edition.Limited),
                    new FirstPersonShooter("Shadow of the Tomb Raider", Edition.Limited)
                };
            }
        }
        #endregion

        #region Create Products - 2 Game Machines and 4 Games.
        /// <summary>
        /// Base class for creating a game.
        /// </summary>
        abstract class Game : IPlayStation, IPlaystationPricing
        {
            protected string Name;
            protected double Price;
            protected Edition Edition;

            public Game(string name, Edition edition)
            {
                Name = name;
                Edition = edition;
            }

            public abstract double CalculatePrice();

            public bool DecreasePrice(int amount)
            {
                if (amount < Price)
                {
                    Price -= amount;
                    Console.WriteLine($"The price for {Edition} edition {Name} game " +
                        $"has been decreased by {amount:C2} to {Price:C2}");
                    return true;
                }
                return false;
            }

            public bool IncreasePrice(int amount)
            {
                if (amount < Price)
                {
                    Price += amount;
                    Console.WriteLine($"The price for {Edition} edition {Name} game " +
                        $"has been increased by {amount:C2} to {Price:C2}");
                    return true;
                }
                return false;
            }

            // Child classes can use this concrete method.
            // Unless, the child class override ToString()
            public override string ToString()
            {
                return $"This {Name} game cost {Price:C2}";
            }
        }

        class RolePlayingGame : Game, IPlayStation
        {
            public RolePlayingGame(string name, Edition edition)
                : base(name, edition)
            {
                switch (Edition)
                {
                    default:
                    case Edition.Normal:
                        Price = 59;
                        break;

                    case Edition.Limited:
                        Price = 69;
                        break;
                }
            }
            public override double CalculatePrice()
            {
                return Price;
            }
        }

        class FirstPersonShooter : Game, IPlayStation
        {
            public FirstPersonShooter(string name, Edition edition)
                : base(name, edition)
            {
                switch (Edition)
                {
                    default:
                    case Edition.Normal:
                        Price = 49;
                        break;

                    case Edition.Limited:
                        Price = 59;
                        break;
                }
            }

            public override double CalculatePrice()
            {
                return Price;
            }
        }

        enum Edition
        {
            Normal, Limited
        }
        /// <summary>
        /// Interface for creating Playstation
        /// </summary>
        interface IPlayStation
        {
            double CalculatePrice();
        }

        class Playstation : IPlayStation, IPlaystationPricing
        {
            private Edition Edition;
            private double Price;

            public Playstation(Edition edition)
            {
                Edition = edition;
                switch (Edition)
                {
                    default:
                    case Edition.Normal:
                        Price = 399;
                        break;

                    case Edition.Limited:
                        Price = 499;
                        break;
                }
            }

            public double CalculatePrice() => Price;

            public bool DecreasePrice(int amount)
            {
                if (amount > Price)
                {
                    return false;
                }

                Price -= amount;
                Console.WriteLine($"The price for {Edition} Playstation 4 " +
                    $"has been decreased by {amount:C2} to {Price:C2}");
                return true;
            }

            public bool IncreasePrice(int amount)
            {
                if (amount < Price)
                {
                    Price += amount;
                    Console.WriteLine($"The price for {Edition} Playstation 4 " +
                        $"has been increased by {amount:C2} to {Price:C2}");
                    return true;
                }
                return false;
            }

            public override string ToString()
            {
                return $"This {Edition} Playstation cost {Price:C2}";
            }
        }
        #endregion
    }
}
