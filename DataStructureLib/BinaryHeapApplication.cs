using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class BinaryHeapApplication
    {
        public static void Main()
        {
            MaxBinaryHeap.NewBinaryHeap
                .Insert(41, 39, 33, 18, 27, 12)
                .PrintHeap()
                //.Print(MaxBinaryHeap.NewBinaryHeap.Count.ToString())
                .Insert(55)
                .PrintHeap()
                .PrintInfo()
                .ExtractMax()
                .PrintHeap()
                .PrintInfo();

            PriorityQueue.NewPriorityQueue
                .Enqueue(41, 39, 33, 18, 27, 12)
                .PrintQueue()
                .Enqueue(55)
                .PrintQueue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .Dequeue()
                .PrintQueue();
        }

        // Max binary Heap vs Min Binary Heap
        // for priority queue
        // Left child 2n + 1
        // Right child 2n + 2
        // using an array to determine an array. 

        class PriorityQueue
        {
            // priority 1 is the highest priority.
            // MinBinaryHeap.
            // Enqueue accepts a value and priority
            // Dequeue remove root element
            // Change the > < operators to reflect min Binary Heap.

            private Node[] Nodes;
            /// <summary>
            /// Actual Number of elements in array.
            /// </summary>
            public int Count { get; private set; }
            /// <summary>
            /// Actual length of the array, including empty spaces.
            /// </summary>
            private int Length;
            /// <summary>
            /// The next empty index position
            /// </summary>
            private int Index;

            #region Singleton Construct
            public static PriorityQueue NewPriorityQueue = new PriorityQueue();
            private PriorityQueue()
            {
                // Initialize the array.
                Nodes = new Node[2];
                Count = 0;
                Index = -1;
                Length = 0;
            }
            #endregion

            #region Add Methods
            public PriorityQueue Enqueue(params int[] data)
            {
                Random random = new Random();
                for (int i = 0; i < data.Length; i++)
                {
                    // 1 <= priority < 6
                    Insert(data[i], random.Next(1, 6));
                }

                return this;
            }

            private void Insert(int data, int priority)
            {
                Insert(new Node(data, priority));
            }

            private void Insert(Node data)
            {
                EnsureCapacity();

                // Add data to end of array.
                Nodes[++Index] = data;

                // Optimization: If only one element
                if (Index == 0) return;

                // Initial position of data
                int current = Index;

                while (true)
                {
                    // By default round down.
                    int parent = (current - 1) / 2;

                    // Change to Min Binary <
                    if (Nodes[current].Priority < Nodes[parent].Priority)
                    {
                        // Swap
                        Node temp = Nodes[current];
                        Nodes[current] = Nodes[parent];
                        Nodes[parent] = temp;
                    }
                    else
                    {
                        break;
                    }

                    current = parent;
                }

                // Index is zero-based
                Count = Index + 1;
            }
            #endregion

            #region Helper
            private void EnsureCapacity()
            {
                // Array is full.
                Node[] temp;

                // Number of elements = lenght of array
                if (Count == Nodes.Length)
                {
                    // Expand array.
                    Length = Nodes.Length * 2;
                    temp = new Node[Length];

                    for (int i = 0; i < Nodes.Length; i++)
                    {
                        temp[i] = Nodes[i];
                    }

                    Nodes = temp;
                }
            }
            private void SinkDown(int parentIdx = 0)
            {
                int parent = parentIdx;

                while (true)
                {
                    // Define left and right child.
                    int leftChild = (2 * parent) + 1;
                    int rightChild = (2 * parent) + 2;

                    // if array is out of index return, in precedence
                    // if parent is smaller than left or right child
                    // Change to Min Binary >
                    if (leftChild <= Index && (Nodes[parent].Priority > Nodes[leftChild].Priority) ||
                        rightChild <= Index && (Nodes[parent].Priority > Nodes[rightChild].Priority))
                    {
                        // check which child is the larger value of the two.
                        if (Nodes[leftChild].Priority < Nodes[rightChild].Priority)
                        {
                            // swap left child with parent
                            Node temp = Nodes[parent];
                            Nodes[parent] = Nodes[leftChild];
                            Nodes[leftChild] = temp;

                            // Assign parent to left child
                            parent = leftChild;
                        }
                        else
                        {
                            // swap right child with parent
                            Node temp = Nodes[parent];
                            Nodes[parent] = Nodes[rightChild];
                            Nodes[rightChild] = temp;

                            // Assign parent to right child
                            parent = rightChild;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion

            #region Print
            public PriorityQueue Print(string message = null)
            {
                Console.WriteLine(message);
                return this;
            }
            public PriorityQueue PrintInfo()
            {
                Console.WriteLine("Number of elements in heap: " + Count);
                Console.WriteLine("Length of heap: " + Length);
                return this;
            }
            public PriorityQueue PrintQueue()
            {
                for (int i = 0; i < Count; i++)
                {
                    if (i == Count - 1)
                        Console.Write($"[{Nodes[i].Priority},{Nodes[i].Data}]");
                    else
                        Console.Write($"[{Nodes[i].Priority},{Nodes[i].Data}] -> ");
                }

                // Insert blank line
                Console.WriteLine();
                return this;
            }
            #endregion

            #region Remove Methods
            public PriorityQueue Dequeue()
            {
                Node max = ExtractMin(0);
                if (max != null)
                {
                    Console.WriteLine("Extracted Max value: " + $"[{max.Priority},{max.Data}]");
                }
                return this;
            }

            // Overload requires different parameter signature.
            private Node ExtractMin(int x = 0)
            {
                // There are no elements in the array.
                if (Index < 0)
                {
                    Console.WriteLine("The heap is empty");
                    return null;
                }

                // return the root
                Node root = Nodes[0];

                // Replace the first value in array with last value.
                Nodes[0] = Nodes[Index];

                // Remove last element in Array
                Index--;

                // Update count with one element less
                Count--;

                // Sink down.
                SinkDown();

                return root;
            }
            public PriorityQueue Clear()
            {
                Console.WriteLine("Clearing data inside Heap.");

                for (int i = 0; i < Count; i++)
                {
                    // Set each array element to 0
                    Nodes[i] = default;
                }

                // reset count and index to 0.
                // but keep length the same.
                Count = 0;
                Index = -1;

                return this;
            }
            #endregion
            class Node
            {
                public int Data { get; set; }
                public int Priority { get; set; }
                public Node(int data, int priority)
                {
                    Data = data;
                    Priority = priority;
                }
            }
        }

        class MaxBinaryHeap
        {
            private int[] Numbers;
            /// <summary>
            /// Actual Number of elements in array.
            /// </summary>
            public int Count { get; private set; }
            /// <summary>
            /// Actual length of the array, including empty spaces.
            /// </summary>
            private int Length;
            /// <summary>
            /// The next empty index position
            /// </summary>
            private int Index;

            #region Singleton Construct
            public static MaxBinaryHeap NewBinaryHeap = new MaxBinaryHeap();
            private MaxBinaryHeap()
            {
                // Initialize the array.
                Numbers = new int[2];
                Count = 0;
                Index = -1;
                Length = 0;
            }
            #endregion

            #region Add Methods
            public MaxBinaryHeap Insert(params int[] data)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Insert(data[i]);
                }

                return this;
            }

            private void Insert(int data)
            {
                EnsureCapacity();

                // Add data to end of array.
                Numbers[++Index] = data;

                // Optimization: If only one element
                if (Index == 0) return;

                // Initial position of data
                int current = Index;

                while (true)
                {
                    // By default round down.
                    int parent = (current - 1) / 2;

                    if (Numbers[current] > Numbers[parent])
                    {
                        // Swap
                        int temp = Numbers[current];
                        Numbers[current] = Numbers[parent];
                        Numbers[parent] = temp;
                    }
                    else
                    {
                        break;
                    }

                    current = parent;
                }

                // Index is zero-based
                Count = Index + 1;
            }
            #endregion

            #region Helper
            private void EnsureCapacity()
            {
                // Array is full.
                int[] temp;

                // Number of elements = lenght of array
                if (Count == Numbers.Length)
                {
                    // Expand array.
                    Length = Numbers.Length * 2;
                    temp = new int[Length];

                    for (int i = 0; i < Numbers.Length; i++)
                    {
                        temp[i] = Numbers[i];
                    }

                    Numbers = temp;
                }
            }
            private void SinkDown(int parentIdx = 0)
            {
                int parent = parentIdx;

                while (true)
                {
                    // Define left and right child.
                    int leftChild = (2 * parent) + 1;
                    int rightChild = (2 * parent) + 2;

                    // if array is out of index return, in precedence
                    // if parent is smaller than left or right child
                    if (leftChild <= Index && (Numbers[parent] < Numbers[leftChild]) ||
                        rightChild <= Index && (Numbers[parent] < Numbers[rightChild]))
                    {
                        // check which child is the larger value of the two.
                        if (Numbers[leftChild] > Numbers[rightChild])
                        {
                            // swap left child with parent
                            int temp = Numbers[parent];
                            Numbers[parent] = Numbers[leftChild];
                            Numbers[leftChild] = temp;

                            // Assign parent to left child
                            parent = leftChild;
                        }
                        else
                        {
                            // swap right child with parent
                            int temp = Numbers[parent];
                            Numbers[parent] = Numbers[rightChild];
                            Numbers[rightChild] = temp;

                            // Assign parent to right child
                            parent = rightChild;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion

            #region Print
            public MaxBinaryHeap Print(string message = null)
            {
                Console.WriteLine(message);
                return this;
            }
            public MaxBinaryHeap PrintInfo()
            {
                Console.WriteLine("Number of elements in heap: " + Count);
                Console.WriteLine("Length of heap: " + Length);
                return this;
            }
            public MaxBinaryHeap PrintHeap()
            {
                for (int i = 0; i < Count; i++)
                {
                    if (i == Count - 1)
                        Console.Write(Numbers[i] + "");
                    else
                        Console.Write(Numbers[i] + ", ");
                }

                // Insert blank line
                Console.WriteLine();
                return this;
            }
            #endregion

            #region Remove Methods
            public MaxBinaryHeap ExtractMax()
            {
                int max = ExtractMax(0);
                Console.WriteLine("Extracted Max value: " + max);
                return this;
            }

            // Overload requires different parameter signature.
            private int ExtractMax(int x = 0)
            {
                // There are no elements in the array.
                if (Index < 0)
                {
                    Console.WriteLine("The heap is empty");
                    return 0;
                }

                // return the root
                int root = Numbers[0];

                // Replace the first value in array with last value.
                Numbers[0] = Numbers[Index];

                // Remove last element in Array
                Index--;

                // Update count with one element less
                Count--;

                // Sink down.
                SinkDown();

                return root;
            }
            public MaxBinaryHeap Clear()
            {
                Console.WriteLine("Clearing data inside Heap.");

                for (int i = 0; i < Count; i++)
                {
                    // Set each array element to 0
                    Numbers[i] = default;
                }

                // reset count and index to 0.
                // but keep length the same.
                Count = 0;
                Index = -1;

                return this;
            }
            #endregion
        }
    }
}
