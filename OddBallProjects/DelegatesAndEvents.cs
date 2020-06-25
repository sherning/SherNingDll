using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OddBallProjects
{
    class DelegatesAndEvents
    {
        // static vs non-static
        // Static means to share. Called without instanitation.
        // Need to separate static components from non-static components.

        public static void Main()
        {
            //IntroductionToDelegates();
            //MultiChainingDelegates();
            //StoringDelegateValues();
            //AddingAnActionToAnAction();
            //UnderstandingClosure();
            //IntroToBasicObserverPatterns();
            //DifferenceDelegatesAndEvents();
            //FunWithCows();
            //RealApplicationEvents();
        }

        #region Concepts of Covariance and ContraVariance
        // Contra Variance for parameters

        // Covariance for return type
        #endregion
        private static bool IsClicked = false;
        private static void RealApplicationEvents()
        {
            Button button = new Button();

            // what happens when Click is invoked ....
            button.Click += (s, e) => MessageBox.Show($"You clicked the button", "What is caption?");
            button.Text = "Button 1";

            Button button2 = new Button();
            button2.Text = "Button 2";

            // a mechanical click on the mouse will Click?.Invoke(sender,e) to trigger click ..
            button2.Click += (s, e) =>
            { 
                if (IsClicked == false)
                {
                    button2.Text = "Shit";
                    IsClicked = true;
                }
                else
                {
                    button2.Text = "Button 2";
                    IsClicked = false;
                }
            };

            button2.Left = 100;

            Form form = new Form();
            form.Controls.Add(button);
            form.Controls.Add(button2);
            form.Text = "Sher Ning Form";

            Application.Run(form);
        }
        private static void UnderstandingClosure()
        {
            Action GiveMeAction()
            {
                int i = 0;
                return () => i++;
            }

            Action a = GiveMeAction();

            // in other words, closure does not reset i to 0.
            a(); a(); a(); a();

            int GiveMeInt()
            {
                int i = 0;
                return i++;
            }

            // unlike this method.
            GiveMeInt();
            GiveMeInt();
            GiveMeInt();

            Action GiveMeMoreAction()
            {
                int i = 0;
                Action action = null;

                // Scope of i is retained, and not reset to 0
                action += () => Console.WriteLine("First Method Call: " + i++);
                action += () => Console.WriteLine("Second Method Call: " + i++);

                return action;
            }

            // value of i will retain its state in closure. so long as you keep invoking the 
            // same Action.
            Action giveMeAction = GiveMeMoreAction();
            giveMeAction();
            giveMeAction();
            giveMeAction();
            giveMeAction();

            // i is reset to 0 in this example. 
            Action giveMeMoreMoreAction = GiveMeMoreAction();
            giveMeMoreMoreAction();
            giveMeMoreMoreAction();
            giveMeMoreMoreAction();
            giveMeMoreMoreAction();

            // under the hood, the compiler does this. no need CompilerGenerated(). as we are not invoking.
            Action GiveMeUnderTheHoodAction() => new Action(new DisplayClass().CompilerGenerated);

            Action underTheHood = GiveMeUnderTheHoodAction();
            underTheHood();
            underTheHood();
            underTheHood();

            Action GiveMeMoreUnderTheHood()
            {
                DisplayClass2 display = new DisplayClass2();
                Action action = null;
                action += display.CompilerGenerated;
                action += display.CompilerGenerated2;
                return action;
            }

            Action underTheHood2 = GiveMeMoreUnderTheHood();
            underTheHood2();
            underTheHood2();
            underTheHood2();

            Action InterestingAction()
            {
                Action ret = null;
                int i = 0;
                int j = 0;
                ret += () => i++;
                ret += () => j++;
                ret += () => { i++; j++; };
                return ret;
            }

            Action interestingAction = InterestingAction();
            interestingAction();
            interestingAction();
            interestingAction();
            interestingAction();
        }
        private static void IntroductionToDelegates()
        {
            // delegate is a way to pass methods/functions as arguments.
            // this is simply just passing a delegate.
            TestDelegate test = new TestDelegate(TestMethod);
            Console.WriteLine(test.Method);
            Console.WriteLine(test.Target); // return null for static methods.

            // this is to Invoke a delegate, which is reference type
            test();

            // watch each video to the end or pause first before creating notes.
            TestDelegate test2 = TestMethod; test2.Invoke();

            // same results, different styles.
            InvokeDelegate(TestMethod); InvokeDelegate(new TestDelegate(TestMethod));

            TestDelegate test3 = new TestDelegate(new DelegatesAndEvents().TestMethod2);
            Console.WriteLine(test3.Method);
            Console.WriteLine(test3.Target); // if method is non-static.

            foreach (int number in FilterNumbers(n => n > 5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
            {
                Console.WriteLine(number);
            }
        }
        private static void MultiChainingDelegates()
        {
            // you can do chaining. basically, the delegate += METHOD.
            Action action = () => Console.WriteLine("action!"); ;
            action += () => Console.WriteLine("Action !!");

            // delegate is like test() {method code goes in here}
            action += delegate () { Console.WriteLine("Action"); };

            // without a method name, you CANNOT remove a method. LOL
            action -= () => Console.WriteLine("Action !!");

            // you can use combine. Need to cast it back to Action, as Delegate is the Base type.
            action = (Action)Delegate.Combine(new Action(TestMethod),
                new Action(new DelegatesAndEvents().TestMethod2));

            // multicast delegate uses a linked list like structure
            action.Invoke();

            // action with no method names will not be called.
            foreach (var item in action.GetInvocationList())
                Console.WriteLine("invocation list: " + item.Method);
        }
        private static void StoringDelegateValues()
        {
            int Return5() => 5;
            int Return10() => 10;
            int Return20() => 20;

            Func<int> function = Return5;
            function += Return10;
            function += Return20;

            // this is how to store variables return from delegate function call.
            foreach (int i in GetReturnValues(function)) Console.WriteLine(i);
        }
        private static void AddingAnActionToAnAction()
        {
            void Moo() => Console.WriteLine("Moo");
            void Goo() => throw new Exception();
            void Foo() => Console.WriteLine("Foo");

            // Moo needs to be wrap in an action for this to work.
            Action actions = new Action(Moo) + Goo + Foo;
            Action action2 = (Action)Moo + Goo + Foo;

            foreach (Action action in actions.GetInvocationList())
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    Console.WriteLine("Caught an exception.");
                }
            }
        }
        private static void DifferenceDelegatesAndEvents()
        {
            // Event is a delegate reference with two restrictions on it
            // one cannot invoke the delegate reference directly
            // two cannot assign to it directly
            // other than that it is a delegate by nature.

            // Think of a newspaper subscription.
            // one subscribers should not affect the others.

            EventTest eventTest = new EventTest();
            eventTest.Action += () => Console.WriteLine("Test");
            eventTest.Action -= () => Console.WriteLine("Test");
            eventTest.InvokeEventTest();
        }
        class EventTest
        {
            // eventhandler is like action EXCEPT it takes two arguments.
            // dont confuse it with event keyword.
            private Action _Action;
            public event Action Action
            {
                add
                {
                    // additional control.
                    _Action += value;
                    Console.WriteLine("Adding");
                }

                // Trace statements / like debug statements
                remove => Console.WriteLine("Removing");
            }

            // if action is not null, then invoke.
            public void InvokeEventTest() => _Action?.Invoke();
        }; // no problem to leave semi-colon, from c++

        #region Cow Example
        // object sender, will identify which cow object is the SENDER.
        enum CowState { Awake, Sleeping, Playing }

        // this is for the argument.
        class CowStateEventArgs : EventArgs
        {
            public CowState CurrentCowState { get; set; }

            public CowStateEventArgs(CowState cowState)
            {
                CurrentCowState = cowState;
            }
        }

        class Cow
        {
            public string Name { get; set; }

            // event key word is just to adjust access
            // EventHandler is just a delegate with a special signature of void(sender, e)
            // add and remove by multicast delegate.
            public event EventHandler<CowStateEventArgs> CowEventHandler;

            public Cow(string name)
            {
                Name = name;
            }

            // remember, pass the method to the delegate first. then, invoke it.
            // You cannot invoke events directly. When you invoke, you will need to pass arguments.
            public void InvokeCowEventHandler()
            {
                CowStateEventArgs cowStateEventArgs = null;

                switch (new Random().Next(1, 4))
                {
                    case 1:
                        cowStateEventArgs = new CowStateEventArgs(CowState.Awake);
                        break;

                    case 2:
                        cowStateEventArgs = new CowStateEventArgs(CowState.Playing);
                        break;

                    case 3:
                        cowStateEventArgs = new CowStateEventArgs(CowState.Sleeping);
                        break;
                }

                // this keyword refers to betsy, george, patsy ...
                CowEventHandler?.Invoke(this, cowStateEventArgs);
            }
        }

        private static void FunWithCows()
        {
            // three ways to do this

            // method 1:
            Cow betsy = new Cow("Betsy");
            betsy.CowEventHandler += Giggle;

            // method 2:
            Cow george = new Cow("George");
            george.CowEventHandler += delegate
            {
                Console.WriteLine("Mooooo!!!");
            };
            george.CowEventHandler += delegate (object sender, CowStateEventArgs e)
            {
                if (sender is Cow cow)
                {
                    Console.WriteLine($"{cow.Name} is {e.CurrentCowState}");
                }
            };

            // method 3:
            Cow patsy = new Cow("Patsy");
            patsy.CowEventHandler += (sender, e) =>
            {
                Console.WriteLine($"{(Cow)sender} is {e.CurrentCowState}");
            };
            patsy.CowEventHandler += (sender, e) => Console.WriteLine("Mooooo");
            patsy.CowEventHandler += (sender, e) =>
            {
                if (sender is Cow cow)
                {
                    switch (e.CurrentCowState)
                    {
                        case CowState.Awake:
                            Console.WriteLine($"{cow.Name} is awake and angry !!!");
                            break;

                        case CowState.Sleeping:
                            Console.WriteLine($"{cow.Name} is sleeeeeeeping... shhhh...");
                            break;

                        case CowState.Playing:
                            Console.WriteLine($"{cow.Name} is playing and it wants to play with you too !!");
                            break;
                    }
                }
            };

            // method 4, EventHandler is a delegate. += means multicast delegate.
            Cow panda = new Cow("Panda");
            panda.CowEventHandler += new EventHandler<CowStateEventArgs>((sender, e) => 
            {
                if (sender is Cow cow)
                {
                    Console.WriteLine($"{cow.Name} is in the state of {e.CurrentCowState}");
                }
            });

            // this is syntatic sugar. it makes it look like it is taking a method, but it it not. 
            // what is it is doing is CHAINING delegates like a linked list.
            panda.CowEventHandler += Giggle;

            panda.CowEventHandler += new EventHandler<CowStateEventArgs>(Giggle);

            panda.CowEventHandler += new EventHandler<CowStateEventArgs>(delegate 
            {
                Console.WriteLine($"Cow has no sender or state");
            });

            // invoke all the cows.
            betsy.InvokeCowEventHandler();
            george.InvokeCowEventHandler();
            patsy.InvokeCowEventHandler();
            panda.InvokeCowEventHandler();

            // event word prevents you from directly invoking the delegate.
            //betsy.CowEventHandler.Invoke(george, new CowStateEventArgs(CowState.Playing));
        }

        private static void Giggle(object sender, CowStateEventArgs e)
        {
            if (sender is Cow cow)
            {
                switch (e.CurrentCowState)
                {
                    case CowState.Awake:
                        Console.WriteLine($"{cow.Name} is awake and angry !!!");
                        break;

                    case CowState.Sleeping:
                        Console.WriteLine($"{cow.Name} is sleeeeeeeping... shhhh...");
                        break;

                    case CowState.Playing:
                        Console.WriteLine($"{cow.Name} is playing and it wants to play with you too !!");
                        break;
                }
            }
        }
        #endregion

        #region Basic Observer Pattern with Delegates

        private static void IntroToBasicObserverPatterns()
        {
            TrainSignal trainSignal = new TrainSignal();
            new Train(trainSignal);
            new Train(trainSignal);
            new Train(trainSignal);
            new Train(trainSignal);
            trainSignal.HereComesTheTrains();


            Train train = new Train(trainSignal);

            // they are the same thing. but with events, you cannot directly invoke it.
            // trainSignal.TrainsAreComing();
            // trainSignal.TrainsAreComing = null;
        }
        class TrainSignal
        {
            // linked list , the first node on linked list.
            public event Action TrainsAreComing;

            // TrainsAreComing() -> invoke the linked list of delegates.
            public void HereComesTheTrains() => TrainsAreComing();
        }

        class Train
        {
            public Train(TrainSignal trainSignal)
            {
                // Add to the list of delegates
                trainSignal.TrainsAreComing += StopTrainCar;
            }

            private void StopTrainCar()
            {
                int i = 0;
                Console.WriteLine("Screeeeech!!!! " + ++i);
            }
        }


        #endregion

        #region Help functions
        private delegate void TestDelegate();

        // this is what it does under the hood for closure.
        class DisplayClass2
        {
            private int i;
            public void CompilerGenerated()
            {
                Console.WriteLine("First Method Call: " + i++);
            }
            public void CompilerGenerated2()
            {
                Console.WriteLine("Second Method Call: " + i++);
            }
        }

        class DisplayClass
        {
            private int i;
            public void CompilerGenerated()
            {
                i++;
                Console.WriteLine("Under the hood: " + i);
            }
        }

        private static IEnumerable<T> GetReturnValues<T>(Func<T> function)
        {
            // with yield return, i dont need to create a new list and return that list.
            foreach (Func<T> item in function.GetInvocationList())
                yield return item.Invoke();
        }

        private static void InvokeDelegate(TestDelegate test) => test.Invoke();
        private static void TestMethod() => Console.WriteLine("Test");
        private void TestMethod2() => Console.WriteLine("Test2");

        // use Func instead of creating a new delegate object, like test delegate.
        private static IEnumerable<int> FilterNumbers(Func<int, bool> filter, params int[] numbers)
        {
            foreach (int number in numbers)
                if (filter(number)) yield return number;
        }
        #endregion

    }
}
