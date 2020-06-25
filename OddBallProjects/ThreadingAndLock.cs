using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OddBallProjects
{
    class ThreadingAndLock
    {
        public static void Main()
        {
            //UnderstandingBackgroundThread();
            //ThreadSync2();
            //DivideAndConquerTest();
            //PC_Test();
            //TestLock();
            //TestReaderWriterLock();
        }

        #region Introduction to Threading
        private static void UnderstandingBackgroundThread()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Thread thread = new Thread(id =>
                {
                    while (true)
                        Console.WriteLine("Hello from thread" + id);
                });

                thread.IsBackground = true;
                thread.Start(i);
            }

            // although thread is running an infinite loop, if set as background.
            // once foreground is terminated, the loop will end.
            Console.WriteLine("Hello from Main");
        }
        private static void UnderstandConcurrency()
        {
            // Concurrency - doing more than one thing at a time.

            // Multithreading- a form of currency that uses multiple threads of execution

            // Parallel processing 
            // Doing a lot of work by dividing it up among multiple threads that run concurrently.

            // Asynchronous Programming
            // A form of concurrency that uses future or callbacks to avoid unnecessary threads

            // Reactive Programming
            // A declarative style of programming where the application reacts to events.


        }
        #endregion

        #region Why is a Lock Needed

        private static int Count;

        private static Object Baton = new object();

        private static void ThreadSync()
        {
            Thread thread1 = new Thread(CountIncrementWithLock);
            Thread thread2 = new Thread(CountIncrementWithLock);
            //Thread thread1 = new Thread(CountIncrement);
            //Thread thread2 = new Thread(CountIncrement);
            thread1.Start();
            Thread.Sleep(500);
            thread2.Start();
        }
        private static void CountIncrement()
        {
            while (true)
            {
                int temp = Count;
                Thread.Sleep(1000);
                Count = temp + 1;
                Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId} incremented count to {Count}");
                Thread.Sleep(1000);
            }
        }
        private static void CountIncrementWithLock()
        {
            while (true)
            {
                // prevent people from buying two people from buying the same movie ticket.
                lock (Baton)
                {
                    int temp = Count;
                    Thread.Sleep(1000);
                    Count = temp + 1;
                    Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId} incremented count to {Count}");
                    Thread.Sleep(1000);
                }
            }
        }
        private static void ThreadSync2()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
                new Thread(BathRoomStalL).Start();
        }
        private static void BathRoomStalL()
        {
            Random random = new Random();
            bool lockTaken = false;

            Console.WriteLine($"Person {Thread.CurrentThread.ManagedThreadId} is trying to obtain the lock");

            // Under the hood ... or just use lock.
            //lock (Baton)
            Monitor.Enter(Baton, ref lockTaken);
            try
            {
                int thread = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"Person {thread} obtained lock and doing his business...");
                Thread.Sleep(random.Next(200, 3000));
                Console.WriteLine($"Person {thread} returns the lock and is leaving the toilet");
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(Baton);
            }
            Console.WriteLine($"Person {Thread.CurrentThread.ManagedThreadId} is leaving the restrauant");
        }

        #endregion

        #region Divide and Conquer

        private static void DivideAndConquerTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Generating Ints");
            byte[] ints = GenerateIntsSingleThread();
            Console.WriteLine("Summing up ...");
            long total = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                total += ints[i];
            }
            Console.WriteLine("Total value is: " + total);
            stopWatch.Stop();
            Console.WriteLine("Time taken to complete this task: " + stopWatch.Elapsed);

            // reset data
            stopWatch.Reset();
            total = 0;

            // with multi threading
            stopWatch.Start();
            Console.WriteLine($"\nGenerate Ints");
            GenerateIntsMultiThreadsStart();
            Console.WriteLine("Summing up ...");
            for (int i = 0; i < Ints.Length; i++)
            {
                total += Ints[i];
            }
            Console.WriteLine("Total value is: " + total);
            stopWatch.Stop();
            Console.WriteLine("Time taken to complete this task: " + stopWatch.Elapsed);
        }
        private static byte[] GenerateIntsSingleThread()
        {
            byte[] ints = new byte[500_000_000];
            Random random = new Random();
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = (byte)random.Next(10);
            }

            return ints;
        }

        private static byte[] Ints = new byte[500_000_000];
        private const int ProcessorCount = 2;

        // this is the signature for parameterized start
        private static void GenerateIntsMultiThreads(object processNum)
        {
            int portionSize = Ints.Length / ProcessorCount;
            int baseIndex = (int)processNum * portionSize;
            Random random = new Random();

            // no need to lock, as each partition is sandboxed without overlap.
            for (int i = baseIndex; i < baseIndex + portionSize; i++)
                Ints[i] = (byte)random.Next(10);
        }

        private static void GenerateIntsMultiThreadsStart()
        {
            Thread[] threads = new Thread[ProcessorCount];

            for (int i = 0; i < ProcessorCount; i++)
            {
                threads[i] = new Thread(GenerateIntsMultiThreads);

                // start is to pass the arguments and invoke the method.
                threads[i].Start(i);
            }

            // need to call join to wait for all threads to finish before exit method.
            for (int i = 0; i < ProcessorCount; i++)
                threads[i].Join();
        }
        #endregion

        #region Producing vs Consuming Threads

        private static QueueSync<int> Numbers = new QueueSync<int>();
        //private static Queue<int> Numbers = new Queue<int>();
        private static int[] SumArr = new int[ProcessorCount];
        private static void ProduceNumbers()
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int numEnqueue = random.Next(10);
                Console.WriteLine("Producing thread adding " + numEnqueue + " to the queue.");

                lock(Numbers)
                    Numbers.Enqueue(numEnqueue);

                Thread.Sleep(random.Next(1000));
            }
        }

        private static void SumNumbersCorrupted(object threadNumber)
        {
            DateTime startTime = DateTime.Now;
            int total = 0;

            while ((DateTime.Now - startTime).Seconds < 11)
            {
                if (Numbers.Count != 0)
                {
                    // there is a race condition here. to get the number.
                    // the losing thread will cause the exception.
                    try
                    {
                        // the winner gets the number.
                        int numDequeue = Numbers.Dequeue();
                        total += numDequeue;
                        Console.WriteLine($"Consuming thread {(int)threadNumber} adding {numDequeue} to its total is {total}");
                    }
                    catch (Exception)
                    {
                        // the loser will throw an exception.
                        Console.WriteLine($"Thread {threadNumber} having an issue.");

                        // throw means put exception on Console.
                        throw;
                    }

                }
            }

            SumArr[(int)threadNumber] = total;
        }

        private static void SumNumbers(object threadNumber)
        {
            DateTime startTime = DateTime.Now;
            int total = 0;

            while ((DateTime.Now - startTime).Seconds < 11)
            {
                int numDequeue = -1;
                // lock on the dangerous stuff, and not everything
                // think of going to the toilet and when you are done with your business
                // you are still hogging the toilet.
                // why lock(Numbers) ? because it is the thing that is going to be affected.

                lock (Numbers) 
                {
                    // key concept.. only LOCK what is needed !!!!
                    if (Numbers.Count != 0)
                        numDequeue = Numbers.Dequeue();
                }

                if (numDequeue != -1)
                {
                    total += numDequeue;
                    Console.WriteLine($"Consuming thread {(int)threadNumber} adding {numDequeue} to its total is {total}");
                }
            }

            SumArr[(int)threadNumber] = total;
        }

        private static void PC_Test()
        {
            Thread producingThread = new Thread(ProduceNumbers);
            producingThread.Start();

            Thread[] threads = new Thread[ProcessorCount];
            for (int i = 0; i < ProcessorCount; i++)
            {
                // with QueueSync it will work not work ...
                //threads[i] = new Thread(SumNumbersCorrupted);
                threads[i] = new Thread(SumNumbers);
                threads[i].Start(i);
            }

            foreach (Thread thread in threads) thread.Join();

            int total = 0;
            foreach (int num in SumArr) total += num;

            Console.WriteLine($"Done adding... Total is {total}");
        }

        // my own synchronized queue
        class QueueSync<T>
        {
            private object Baton = new object();
            private Queue<T> Queue = new Queue<T>();
            public object SyncRoot { get { return Baton; } }
            public int Count { get { lock(Baton) return Queue.Count; } }
            public void Enqueue(T item)
            {
                lock(Baton) Queue.Enqueue(item);
            }

            public T Dequeue()
            {
                lock(Baton) return Queue.Dequeue();
            }
        }
        #endregion

        #region Problem with lock Keyword.
        // lock is just simply are you going to respect my privacy or not? LOL
        class BathRoomStall
        {
            public void BeUsed(int userNum)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Currently being used by thread " + userNum);
                    Thread.Sleep(500);
                }
            }
        }

        private static BathRoomStall stall = new BathRoomStall();
        private static void TestLock()
        {
            for (int i = 0; i < 3; i++) new Thread(TheGoodGuys).Start();

            Thread badGuy = new Thread(TheBadGuys);
            badGuy.Start();
        }

        private static void TheGoodGuys()
        {
            lock (stall)
            {
                int thread = Thread.CurrentThread.ManagedThreadId;
                stall.BeUsed(thread);
            }
        }

        private static void TheBadGuys() => stall.BeUsed(99);
        #endregion

        #region Reader Writer Lock Slim
        private static bool Writer_Thread(int i)
        {
            return (i == 0) ? true : false;
        }

        private static void TestReaderWriterLock()
        {
            int total_tasks = 5;
            int tasks = 0;

            ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

            Parallel.For(0, total_tasks, (i, state) =>
            {
                Thread.CurrentThread.Name = "Thread- " + i;
                while (true)
                {
                    try
                    {
                        if (Writer_Thread(i) == false)
                        {
                            rwLock.EnterReadLock();
                            Thread.Sleep(200);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Readers " + Thread.CurrentThread.Name);
                            rwLock.ExitReadLock();
                            break;
                        }
                        else if (Writer_Thread(i) && rwLock.TryEnterWriteLock(100))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Working " + Thread.CurrentThread.Name);
                            tasks++;
                            Thread.Sleep(100);
                            rwLock.ExitWriteLock();
                            break;
                        }
                        else
                        {
                            Thread.Sleep(200);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Sleeping " + Thread.CurrentThread.Name);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            );

            if (tasks != 1)
            {
                throw new Exception("Weird some tasks haven't done the work " + tasks);
            }
        }
       
        #endregion

    }
}
