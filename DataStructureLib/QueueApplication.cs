using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class QueueApplication 
    {
        public static void Main()
        {
            Queue.NewQueue
                .Enqueue(1, 2, 3, 4)
                .Enqueue(5, 6, 7, 8)
                .Dequeue(20)
                .Print();
        }

        class Queue
        {
            private Node Front;
            private Node Rear;
            public static Queue NewQueue = new Queue();
            private Queue() { }

            public Queue Dequeue(int x)
            {
                if (x == 0 || Front == null) return this;
                if (Front == Rear)
                {
                    Front = Rear = null;
                    Console.WriteLine("All elements Dequeued.");
                    return this;
                }

                Front = Front.Next;
                Dequeue(--x);
                return this;
            }

            public Queue Print(string message = null)
            {
                if(message != null) Console.WriteLine(message);

                Print(Front);
                return this;
            }
            private void Print(Node current)
            {
                if (current == null) return;
                    
                if (Rear == null) Console.WriteLine("List is empty.");

                if (current.Next == null)
                {
                    Console.Write($"[{current.Data}] -> null\n");
                    return;
                }

                if (current == Front)
                    Console.Write($"Front -> [{current.Data}] -> ");
                else
                   Console.Write($"[{current.Data}] -> ");

                Print(current.Next);
            }

            public Queue Enqueue(params int[] numbers)
            {
                Enqueue(0, numbers);
                return this;
            }
            private void Enqueue(int count, params int[] numbers)
            {
                // First in (from rear) - First out (from front)
                if (count >= numbers.Length) return;

                Node temp = new Node(numbers[count]);

                if (Rear == null)
                {
                    Rear = temp;
                    Front = Rear;
                    Enqueue(++count, numbers);
                }
                else
                {
                    Rear.Next = temp;
                    Rear = temp;
                    Enqueue(++count, numbers);
                }
            }
        }

        class Node
        {
            public Node Next { get; set; }
            public int Data { get; set; }
            public Node(int data)
            {
                Data = data;
            }
        }

    }
}
