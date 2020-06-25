using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{

    class DoubleLinkedListApplication
    {
        public static void Main()
        {
            DoubleLinkedList numList = new DoubleLinkedList()

                // Methods to test on Num List
                .AddParams_R(6, 7, 8, 9, 10)
                .Print()
                .AddParamsFromStart_R(0, 1, 2, 3, 4, 5)
                .Print("Linked list before removing")
                .Print()
                .Print("Linked list after removing")
                .RemoveLast_X_Nodes()

                // Check if methods are good.
                .Print()
                .Print("Linked List Printed in Reverse")
                .PrintReverse();
        }

        class DoubleLinkedList
        {
            // Default constructor will set all properties to null.
            public DNode Head { get; set; }
            public DNode Tail { get; set; }
            public int Length { get; set; }

            #region Constructor
            public DoubleLinkedList()
            {
                // need this as I have a parameterized constructor.
            }

            public DoubleLinkedList(params int[] numArr)
            {
                AddParams_R(numArr);
            }
            #endregion

            #region Remove Methods
            public DoubleLinkedList RemoveLast_X_Nodes(int x = 1)
            {
                RemoveLast_X_Nodes(Tail, x);
                return this;
            }
            private void RemoveLast_X_Nodes(DNode current, int x)
            {
                // List is empty
                if (Head == null || Tail == null)
                {
                    Console.WriteLine("The list is empty.\n");
                    return;
                }

                // Remove x number of nodes.
                if (x == 0) return;

                // User inputs a negative number.
                if (x < 0)
                {
                    Console.WriteLine("Please enter a positive non-zero integer value");
                    return;
                }

                // Last node on the list.
                if (Head != null && Tail != null && Head == Tail)
                {
                    Head = Tail = null;
                    Console.WriteLine("The list is now empty.\n");
                    return;
                }
                else
                {
                    current.Prev.Next = null;
                    Tail = current.Prev;
                    Tail.Next = null;
                    RemoveLast_X_Nodes(Tail, --x);
                }
            }

            // Set default parameter value.
            public DoubleLinkedList RemoveFirst_X_Nodes(int x = 1)
            {
                RemoveFirst_X_Nodes(Head, x);
                return this;
            }
            private void RemoveFirst_X_Nodes(DNode current, int x)
            {
                // List is empty
                if (Head == null || Tail == null)
                {
                    Console.WriteLine("The list is empty.\n");
                    return;
                }

                // Remove x number of nodes.
                if (x == 0) return;

                // User inputs a negative number.
                if (x < 0)
                {
                    Console.WriteLine("Please enter a positive non-zero integer value");
                    return;
                }

                // Last node on the list.
                if (Head != null && Tail != null && Head == Tail)
                {
                    Head = Tail = null;
                    Console.WriteLine("The list is now empty.\n");
                    return;
                }
                else
                {
                    current.Next.Prev = null;
                    Head = Head.Next;
                    current.Next = null;
                    RemoveFirst_X_Nodes(Head, --x);
                }
            }

            #endregion

            #region Add Methods
            public DoubleLinkedList AddParamsFromStart_R(params int[] numArr)
            {
                AddParamsFromStart_R(Head, numArr.Length - 1, numArr);
                return this;
            }

            /// <summary>
            /// Think of recursion as opening folders within a folder, and to get back to the top folder. 
            /// You have to close those in the inner folders first. Not the directions of the arrow.
            /// </summary>
            /// <param name="current"></param>
            /// <param name="len"></param>
            /// <param name="numArr"></param>
            private void AddParamsFromStart_R(DNode current, int len, params int[] numArr)
            {
                // numArr is empty OR iteration of numArr completed.
                if (numArr.Length == 0 || len < 0) return;
              
                DNode newNode = new DNode(numArr[len]);

                if (Head == null)
                {
                    // Need to set Tail reference to Head initially.
                    Tail = Head = newNode;
                    AddParamsFromStart_R(Head, --len, numArr);
                }
                else
                {
                    current.Prev = newNode;
                    newNode.Next = current;
                    Head = newNode; // Head --> newNode. graphically.
                    AddParamsFromStart_R(current.Prev, --len, numArr);
                }
            }

            /// <summary>
            /// Insert an array of number integers in order at the start of the List.
            /// </summary>
            /// <param name="numArr"></param>
            /// <returns></returns>
            public DoubleLinkedList AddParamsFromStart(params int[] numArr)
            {
                if (numArr.Length == 0)
                {
                    return this; ;
                }

                int len = numArr.Length - 1;

                // Check if Head is null.
                if (Head == null)
                {
                    // Head will have a non-null reference
                    // Set the Tail to the last element.
                    Tail = Head = new DNode(numArr[len]);
                    len--;
                }

                // Assign the pointer to Head.
                DNode current = Head;

                while (len >= 0)
                {
                    DNode newNode = new DNode(numArr[len]);
                    current.Prev = newNode;
                    newNode.Next = current;
                    Head = newNode;

                    // Increment
                    current = current.Prev;
                    len--;
                }

                // Fluent interface.
                return this;
            }
            public DoubleLinkedList AddParams_R(params int[] numArr)
            {
                AddParams_R(Head, 0, numArr);
                return this;
            }
            private void AddParams_R(DNode current, int index, int[] numArr)
            {
                if (numArr.Length == 0 || index == numArr.Length) return;

                DNode newNode = new DNode(numArr[index]);

                if (Head == null)
                {
                    Head = Tail = newNode;
                    AddParams_R(Tail, ++index, numArr);
                }
                else
                {
                    Tail.Next = newNode;
                    newNode.Prev = Tail;
                    Tail = newNode;
                    AddParams_R(Tail, ++index, numArr);
                }
            }
            public DoubleLinkedList AddParams(params int[] numArr)
            {
                // Check if input array is empty
                if (numArr.Length == 0)
                {
                    return this;
                }

                // Check if there if list is empty
                if (IsEmpty())
                {
                    // If there is only one node, both the head and tail points 
                    // to the first node.
                    DNode firstNode = new DNode(numArr[0]);
                    Head = firstNode;
                    Tail = firstNode;

                    // If there is more than one value in numArr.
                    for (int i = 1; i < numArr.Length; i++)
                    {
                        DNode newNode = new DNode(numArr[i]);

                        // these are new steps for Double Linked List.
                        Tail.Next = newNode;
                        newNode.Prev = Tail;
                        Tail = newNode;
                    }

                    // Set the last Node.Next to null.
                    Tail.Next = null;
                    return this;
                }
                else
                {
                    for (int i = 0; i < numArr.Length; i++)
                    {
                        DNode newNode = new DNode(numArr[i]);
                        Tail.Next = newNode;
                        newNode.Prev = Tail;
                        Tail = newNode;
                    }

                    // Set the last Node.Next to null.
                    Tail.Next = null;

                    // for fluent api call.
                    return this;
                }
            }
            #endregion

            #region Helper Methods
            /// <summary>
            /// Returns true if there is no nodes in the linked list.
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty() => Head == null;
            public DoubleLinkedList Print(string message)
            {
                Console.WriteLine("\n" + message);
                return this;
            }
            public DoubleLinkedList Print()
            {
                Print(Head);
                Console.Write("null\n");
                return this;
            }
            // Recursion - Local variables put into arguments
            private void Print(DNode current)
            {
                // While loop exit condition here.
                if (current == null)
                {
                    return;
                }
                else
                {
                    // While loop main function.
                    Console.Write($"[{current.Data}] -> ");

                    // while loop increment into recursive function
                    Print(current.Next);
                }
            }
            public DoubleLinkedList PrintReverse()
            {
                Console.Write("null");
                PrintReverse(Tail);
                Console.WriteLine("\n");
                return this;
            }
            private void PrintReverse(DNode current)
            {
                if (current == null)
                {
                    return;
                }
                else
                {
                    Console.Write($" <- [{current.Data}]");
                    PrintReverse(current.Prev);
                }
            }
            #endregion
        }

        #region Double Node
        class DNode
        {
            public int Data { get; set; }
            public DNode Prev { get; set; }
            public DNode Next { get; set; }
            public DNode(int data)
            {
                Data = data;
            }
        }
        #endregion
    }
}
