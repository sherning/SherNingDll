using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class CircularSinglyLinkedListApplication
    {
        // Solving problems is not about memorization.
        // It is about understanding the problem.
        public static void Main()
        {
            CircularSinglyLinkedList.NewList
                .AddEnd(5, 6, 7, 8)
                .AddBeginning(1, 2, 3, 4)
                .AddBeginning(0)
                .RemoveBeginning(1)
                .RemoveEnd(0)
                .Print();
        }

        class CircularSinglyLinkedList
        {
            // These properties are instance properties.
            public Node First { get; set; }
            public Node Last { get; set; }

            // Static: No need to create an instance.
            // Property Get: return new LinkedList
            public static CircularSinglyLinkedList NewList => new CircularSinglyLinkedList();

            #region Remove Methods

            // Remove first x nodes from beginning
            public CircularSinglyLinkedList RemoveEnd(int x)
            {
                RemoveEnd(First, x);
                return this;
            }
            private void RemoveEnd(Node current, int x)
            {
                if (First == null || x == 0) return;

                // Remove last element in list.
                if (First == Last)
                {
                    First = Last = null;
                    return;
                }

                if (current.Next != Last)
                {
                    RemoveEnd(current.Next, x);
                }
                else
                {
                    Last = current;
                    current.Next = First;
                    RemoveEnd(First, --x);
                }
                
            }
            public CircularSinglyLinkedList RemoveBeginning(int x)
            {
                if (First == null || x == 0) return this;

                Node temp = First;
                First = First.Next;
                temp.Next = null;
                RemoveBeginning(--x);

                return this;
            }

            #endregion

            #region Add Methods
            // Note that method loading does NOT consider return type.
            // Logically speaking, when you pass arguments into the method,
            // the compiler is unable to differentiate if both methods contain same signature.
            // https://www.geeksforgeeks.org/c-sharp-method-overloading/

            public CircularSinglyLinkedList AddBeginning(params int[] numbers)
            {
                AddBeginning(First, numbers.Length - 1, numbers);
                return this;
            }
            
            private void AddBeginning(Node current, int count, params int[] numbers)
            {
                if (count < 0) return;

                Node newNode = new Node(numbers[count]);

                if (current == null || First == null)
                {
                    First = newNode;
                    Last = First;
                    AddBeginning(First, --count, numbers);
                }
                else
                {
                    newNode.Next = First;
                    First = newNode;
                    Last.Next = First;
                    AddBeginning(First, --count, numbers);
                }
            }

            public CircularSinglyLinkedList AddEnd(params int[] numbers)
            {
                AddEnd(Last, 0, numbers);
                return this;
            }

            /// <summary>
            /// Add a series of numbers to a C.Singly List recursively
            /// </summary>
            /// <param name="Current"></param>
            /// <param name="numbers"></param>
            public void AddEnd(Node current, int count, params int[] numbers)
            {
                // Base Case
                if (count >= numbers.Length) return;

                Node temp = new Node(numbers[count]);

                if (current == null)
                {
                    First = temp;
                    Last = First;
                    AddEnd(Last, ++count, numbers);
                }
                else
                {
                    current.Next = temp;
                    temp.Next = First;
                    Last = temp;
                    AddEnd(Last, ++count, numbers);
                }
            }
            #endregion

            #region Helper Methods
            public CircularSinglyLinkedList Print()
            {
                if (First == null || Last == null)
                {
                    Console.WriteLine("List is empty.");
                    return this;
                }

                Print(First);
                return this;
            }
            private void Print(Node current)
            {
                // If list is empty or current reach to the end.
                if (current == null) return;

                // Print data
                Console.Write($"[{current.Data}] -> ");

                // Print the last data before return.
                if (current == Last)
                {
                    Console.Write($"[{First.Data}] -> Loop\n");
                    return;
                }

                // Start recursive loop.
                Print(current.Next);
            }
            #endregion
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
            public Node(int data)
            {
                Data = data;
            }
        }

    }
}
