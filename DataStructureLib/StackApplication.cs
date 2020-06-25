using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    /// <summary>
    /// Use a singly linked list to represent a stack.
    /// Last-in First-out
    /// </summary>
    class StackApplication
    {
        public static void Main()
        {
            Stack.NewStack
                .AddParams(1, 2, 3, 4)
                .Print("Adding 1, 2, 3, 4")
                .AddParams(1, 2, 3, 4)
                .Remove(4)
                .Print();
        }

        class Stack
        {
            private Node Top;
            public static Stack NewStack = new Stack();

            // Singleton
            private Stack() { }

            public Stack Remove(int x)
            {
                if (Top == null || x == 0) return this;

                Top = Top.Next;
                Remove(--x);
                return this;
            }
            public Stack Print(string message = null)
            {
                if (Top == null)
                {
                    Console.WriteLine("The list is empty.");
                }
                Console.WriteLine(message);
                Console.Write("Top -> "); 
                Print(Top);
                return this;
            }
            private void Print(Node current)
            {
                if (current == null) return;

                if (current.Next == null)
                {
                    Console.Write($"[{current.Data}]\n");
                    return;
                }

                Console.Write($"[{current.Data}] -> ");

                Print(current.Next);
            }

            public Stack AddParams(params int[] numbers)
            {
                AddParams(0, numbers);
                return this;
            }

            private void AddParams(int count, params int[] numbers)
            {
                if (count >= numbers.Length) return;

                Node newNode = new Node(numbers[count]);

                if (Top == null)
                {
                    Top = newNode;
                    AddParams(++count, numbers);
                }
                else
                {
                    newNode.Next = Top;
                    Top = newNode;
                    AddParams(++count, numbers);
                }
            }
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
