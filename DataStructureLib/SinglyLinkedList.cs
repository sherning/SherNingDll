using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class SinglyLinkedListApplication
    {
        // Convert any while loop into recursion
        // While( this will be the base case for recusion )
        // the think that moves the while loop is the parameter for recursion.
        // In a singly linked list, it must start with a head and end with a null.

        public static void Main()
        {
            // One key things to note, if there is no reference pointing to a node,
            // it will be garage collected.
            SinglyLinkedList myList = new SinglyLinkedList();

            Console.WriteLine("Add a new series of numbers: ");

            // using fluent interface with list is very good.
            myList
                .AddSeriesOfNewData(4, 3, 2, 1, 5)
                .Print()
                .AddSeriesOfNewData(6, 7, 8, 9, 10)
                .Push(20)
                .AddSeriesOfNewData(1, 2, 3, 4, 5, 6)
                .AddToEnd(30)
                .GetLength()
                .Print();

            myList.SearchData(10);
            myList.GetLength();
            myList.Print();

            Console.WriteLine("\nTesting various sorting and removing algorithms\n");
            // Extension method, but requires an instance of THIS object.
            // Whereas extension is only limited to the (this type) i.e IEnumerable
            myList
                .MergeSort()
                .Print()
                .RemoveDuplicate()
                .GetLength()
                .InsertNodeSortedList(0)
                .RemoveNode(0)          
                .Print()
                .CreateLoopAt(3)
                .RemoveLoop()
                .Print();
        }
    }

    /// <summary>
    /// Custom Singly Linked List for Numeric Values
    /// </summary>
    class SinglyLinkedList
    {
        // Without head, all the nodes will be garbage collected.
        public Node Head;
        public SinglyLinkedList()
        {
            Head = null;
        }

        #region Merge Sort Singly Linked List
        public SinglyLinkedList MergeSort()
        {
            Head = MergeSort(Head);
            return this;
        }
        public Node MergeSort(Node startingNode)
        {
            // Base case, check if Head is null
            if (startingNode == null || startingNode.Next == null)
            {
                //Console.WriteLine("This linked list does not contain any data.");
                return startingNode;
            }

            // Get the middle of the list.
            Node middle = FindMiddle(startingNode);

            // Set the Next to middle to null
            Node nextToMiddle = middle.Next;

            // Set next of middle to null.
            // Split the Linked List into half
            middle.Next = null;

            // Apply MergeSort on left list
            Node left = MergeSort(startingNode);

            // Apply MergeSort on right list
            Node right = MergeSort(nextToMiddle);

            Node sortedList = Merge(left, right);

            return sortedList;
        }

        private Node Merge(Node a, Node b)
        {
            Node result = null;

            // Base cases
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            // Start with either a or b, and recur
            if (a.Data <= b.Data)
            {
                result = a;
                result.Next = Merge(a.Next, b);
            }
            else
            {
                result = b;
                result.Next = Merge(a, b.Next);
            }

            return result;
        }

        /// <summary>
        /// Return the number of elements inside a Link-List
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int Transverse()
        {
            if (Head == null)
            {
                return 0;
            }

            Node current = Head;
            int length = 0;

            while (current != null)
            {
                current = current.Next;
                length++;
            }

            return length;
        }

        public int Transverse(Node startingNode)
        {
            if (startingNode == null)
            {
                return 0;
            }

            Node current = startingNode;
            int length = 0;

            while (current != null)
            {
                current = current.Next;
                length++;
            }

            return length;
        }
        public Node FindMiddle(Node startingNode)
        {
            // Check if Head is null
            if (startingNode == null)
            {
                //Console.WriteLine("The list contains nothing!");
                return null;
            }

            // Define the first node.
            Node current = startingNode;

            // The distance from head to middle.
            // Cannot use length, because of recursive function
            int middle = Transverse(startingNode) / 2;
            int count = 1;

            while (count < middle)
            {
                current = current.Next;
                count++;
            }

            return current;
        }

        /// <summary>
        /// Return the reference to the node in the middle.
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public Node GetMiddle(Node head)
        {
            // base case
            if (head == null)
            {
                Console.WriteLine("This link list does not contain any data.");
                return head;
            }

            Node fastPointer = head.Next;
            Node slowPointer = head;

            // Floyd Cycle Detection Method.
            // Move fast pointer by 2 and slow pointer by 1
            // Finally slow pointer will point to middle node.
            while (fastPointer != null)
            {
                // Extra step to make fast pointer move 2 steps
                // Alternatively, we can rebuilt the while loop
                // to utilize fastPointer.Next.Next
                fastPointer = fastPointer.Next;

                if (fastPointer != null)
                {
                    slowPointer = slowPointer.Next;
                    fastPointer = fastPointer.Next;
                }
            }

            return slowPointer;
        }
        #endregion

        #region Reverse Singly Linked List
        public void ReverseLinkedList()
        {
            Node current = Head;
            Node previous = null;
            Node next = null;

            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            Head = previous;
        }
        public void ReverseLinkedListPractice()
        {

        }
        public void ReverseLinkedListRecursion()
        {
            Head = ReverseLinkedListRecursion(Head);
        }
        public Node ReverseLinkedListRecursion(Node current)
        {
            // Define base cases
            if (current == null || current.Next == null)
            {
                Console.WriteLine("Return New Head Node from base case: [" + current.Data + "] ->\n");
                return current;
            }

            // creates a new reference node to point to the tail of current.
            Node newHeadNode = ReverseLinkedListRecursion(current.Next);

            // current is pointing to itself.
            current.Next.Next = current;
            current.Next = null;

            return newHeadNode;
        }
        #endregion

        #region Create and Detect Loop
        public SinglyLinkedList RemoveLoop()
        {
            RemoveLoop(Head);
            return this;
        }
        private void RemoveLoop(Node head)
        {
            if (head == null)
            {
                Console.WriteLine("Link list is empty");
                return;
            }

            // both fast and slow start at head pointer.
            Node fast = head;
            Node slow = head;

            while (true)
            {
                // check if there is a loop.
                // Or is evaluatef from left to right.
                // fast.Next must come after fast.
                if (fast == null || fast.Next == null)
                {
                    Console.WriteLine("No loop detected in linked list.");
                    return;
                }

                // detect and remove circular linked loop
                if (fast.Next == Head)
                {
                    Console.WriteLine("Circular Loop detected and removed.");
                    fast.Next = null;
                    return; 
                }
                else if (slow.Next == Head)
                {
                    Console.WriteLine("Circular Loop detected and removed.");
                    slow.Next = null;
                    return;
                }

                // fast moves two steps
                fast = fast.Next.Next;

                // slow moves one step
                slow = slow.Next;

                // if fast and slow meet, means there is a loop.
                if (fast == slow)
                {
                    break;
                }
            }

            // move slow to start of loop.
            slow = head;

            // until their nexts meets.
            while (true)
            {
                // they move one step at at time.
                fast = fast.Next;
                slow = slow.Next;

                if (fast.Next == slow.Next) 
                {
                    // set the last node next pointer to null.
                    fast.Next = null;
                    Console.WriteLine("RemoveLoop : Done");
                    return;
                }
            }
        }
        public void FindAndBreakLoop()
        {
            if (DetectLoop(Head, Head) == true)
            {
                FindAndBreakLoop(Head, Head);
            }
            else
            {
                Console.WriteLine("No loop detected. No need to break loop.");
            }
        }
        /// <summary>
        /// We are assuming there is confirm a loop.
        /// </summary>
        /// <param name="slow"></param>
        /// <param name="fast"></param>
        private void FindAndBreakLoop(Node slow, Node fast)
        {
            if (slow == null)
            {
                Console.WriteLine("Loop found and broken up");
                return;
            }

            fast = fast.Next;

            if (slow == fast)
            {
                //BreakLoop(Head, FindLoopLength(slow, fast));
                BreakLoop(Head, Head, FindLoopLength(slow, fast));
                return;
            }

            FindAndBreakLoop(slow.Next, fast.Next);
        }
        private int FindLoopLength(Node first, Node second)
        {
            second = second.Next;

            if (first == second)
            {
                return 0;
            }

            return FindLoopLength(first, second) + 1;
        }
        private void BreakLoop(Node head, int loopLength)
        {
            Node ptr1 = head;
            Node ptr2 = head;

            while (loopLength > 0)
            {
                loopLength--;
                ptr2 = ptr2.Next;
            }
            while (ptr2.Next != ptr1)
            {
                ptr2 = ptr2.Next;
                ptr1 = ptr1.Next;
            }

            // Pointer 1 and 2 will be poped off the stack.
            // List begins with Head. So long as a node has a reference pointer
            // to the node, it will not be garage collected.
            ptr2.Next = null;
            Console.WriteLine("Loop breaks");
        }
        private void BreakLoop(Node fast, Node slow, int loopLength)
        {
            if (loopLength > 0 && fast.Next != null)
            {
                BreakLoop(fast.Next, slow, --loopLength);
            }

            if (fast.Next != slow && fast.Next != null)
            {
                BreakLoop(fast.Next, slow.Next, 0);
            }
            else
            {
                fast.Next = null;
                Console.WriteLine("Loop breaks");
                return;
            }
        }
        public SinglyLinkedList CreateLoopAt(int index)
        {
            if (DetectLoop(Head, Head))
            {
                Console.WriteLine("There is an existing loop in Linked List.");
            }
            else
            {
                if (index < 0)
                {
                    Console.WriteLine("Please enter a positve integer value.");
                    return this;
                }
                CreateLoopAt(index, Head, Head);
            }

            return this;
        }
        private void CreateLoopAt(int index, Node fastPointer, Node slowPointer)
        {
            if (fastPointer.Next == null)
            {
                if (index == 1)
                {
                    fastPointer.Next = slowPointer;
                    Console.WriteLine($"Loop Created at [{slowPointer.Data}]");
                    return;
                }

                // or you hit the end of the list.
                // if index is greater than the num of Nodes in list.
                if (slowPointer.Next == null)
                {
                    // Create a circular list.
                    fastPointer.Next = slowPointer;
                    Console.WriteLine($"Loop Created at [{slowPointer.Data}]");
                    return;
                }
                CreateLoopAt(--index, fastPointer, slowPointer.Next);
            }
            else
            {
                CreateLoopAt(index, fastPointer.Next, slowPointer);
            }
        }
        public void BreakLoop()
        {
            if (DetectLoop(Head, Head) == true)
            {
                BreakCircularLoop(Head);
            }
            else
            {
                Console.WriteLine("No loop detected - no need to break link list.");
            }
        }
        private void BreakCircularLoop(Node current)
        {
            if (current == null || current.Next == null)
            {
                return;
            }

            if (current.Next == Head)
            {
                current.Next = null;
                Console.WriteLine("Linked list loop broken.");
            }

            BreakCircularLoop(current.Next);
        }
        public void DetectLoop()
        {
            DetectLoop(Head, Head);
        }
        private bool DetectLoop(Node fastPointer, Node slowPointer)
        {
            if (fastPointer == null || fastPointer.Next == null)
            {
                // No loop detected.
                return false;
            }

            // If arguments fastPointer and slowPointer are pointing to the same Node.
            // DetectLoop will always output true.
            // And fastPointer will move two steps each recursive call.
            fastPointer = fastPointer.Next;

            if (fastPointer == slowPointer)
            {
                // loop detected
                Console.WriteLine("Loop detected in Linked List");
                return true;
            }

            return DetectLoop(fastPointer.Next, slowPointer.Next);
        }
        public void CreateCircularLoop()
        {
            Console.WriteLine("Creating link list loop.");
            CreateCircularLoop(Head);
        }
        private void CreateCircularLoop(Node current)
        {
            if (current.Next == null)
            {
                current.Next = Head;
                //Console.WriteLine($"Last node [{current.Data}].Next points to [{Head.Data}]");
                //Console.WriteLine($"Current.Next = [{current.Next.Data}]");
                //Console.WriteLine($"Head = [{Head.Data}]");
                return;
            }

            CreateCircularLoop(current.Next);
        }
        #endregion

        #region Insertion
        public SinglyLinkedList InsertNodeSortedList(int data)
        {
            // Node is a private class. 
            Node newNode = new Node(data);

            Console.WriteLine($"Inserting new node [{newNode.Data}] into sorted list.");
            InsertNodeSortedList(Head, newNode, null);

            return this;
        }
        private void InsertNodeSortedList(Node current, Node newNode, Node prev)
        {
            // Insert at the end of the list
            // without this statement, it will not insert.
            if (current == null)
            {
                newNode.Next = null;
                prev.Next = newNode;
                return;
            }

            // Insert at beginning of the list
            // Special case, where inserted data is smaller than Head
            if (prev == null && Head.Data > newNode.Data)
            {
                newNode.Next = Head;
                Head = newNode;
                return;
            }

            // Insert in the middle of the list
            // Find data to slot in.
            if (current.Data > newNode.Data)
            {
                newNode.Next = current;
                prev.Next = newNode;
                return;
            }

            // Pass in the value to the next recursive call
            // current = current.Next 
            // prev = current
            InsertNodeSortedList(current.Next, newNode, current);
        }
        #endregion

        #region Add Methods
        public SinglyLinkedList AddSeriesOfNewData(params int[] numArr)
        {
            // Check if head is null.
            if (Head == null)
            {
                // Assign the first node address to head.
                Head = new Node(numArr[0]);
                Node current = Head;

                // Create new node reference
                int pointer = 1;

                while (pointer < numArr.Length)
                {
                    current.Next = new Node(numArr[pointer++]);
                    current = current.Next;
                }
            }
            else
            {
                // Create new node reference
                Node current = Head;
                int pointer = 0;

                // Transverse till last node
                while (current.Next != null)
                {
                    current = current.Next;
                }

                // Add the rest of the data from the last node.
                while (pointer < numArr.Length)
                {
                    // the current.Next address is null, before the statement below.
                    current.Next = new Node(numArr[pointer++]);

                    // After creating the next node, then and only then can you move current.
                    current = current.Next;
                }
            }

            return this;
        }

        public SinglyLinkedList Push(int data)
        {
            // newNode variable will be pop off the stack at the end of the method.
            // whereas Head is a class member, which will be stored in the heap.
            Node newNode = new Node(data);

            newNode.Next = Head;

            // Point the Head to the new node
            Head = newNode;

            return this;
        }
        public SinglyLinkedList AddToEnd(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                Head.AddToEnd(data);
            }

            return this;
        }
        public void AddSorted(int data, bool sort)
        {
            if (sort == true)
            {
                this.MergeSort();
            }

            AddSorted(data);
        }
        public void AddSorted(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else if (data < Head.Data)
            {
                AddToBeginning(data);
            }
            else
            {
                Head.AddSorted(data);
            }
        }

        public void AddToBeginning(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                // Create new temp node.
                Node temp = new Node(data);

                // Temp.Next takes the Head's address.
                temp.Next = Head;

                // Head takes the temp's address
                Head = temp;

                // Drop the temp variable, BUT
                // the new node is already created on the heap
                // and the temp address is copied into the Head.
                // so temp is ready for garage collection.
            }
        }
        #endregion

        #region Remove Methods
        public SinglyLinkedList RemoveNode(int key)
        {
            if (RemoveNode(Head, null, key))
            {
                Console.WriteLine($"[{key}] Node is removed.");
            }
            else
            {
                Console.WriteLine($"[{key}] Node is not found.");
            }

            return this;
        }
        private bool RemoveNode(Node current, Node prev, int key)
        {
            if (current == null) return false;

            if (Head.Data == key)
            {
                Head = Head.Next;
                return true;
            }

            if (current.Data == key)
            {
                prev.Next = current.Next;
                return true;
            }

            return RemoveNode(current.Next, current, key);
        }
        public void RemoveFirst()
        {
            // if list is not empty
            if (ListEmpty() == true)
            {
                Console.WriteLine("List is empty");
            }
            else if (Head.Next != null)
            {
                // Point the head to the next node.
                Head = Head.Next;
            }
        }

        public void RemoveLast()
        {
            CheckBeforeRemoving();

            Node current = Head;
            while (current.Next != null)
            {
                if (current.Next.Next == null)
                {
                    current.Next = null;
                    return;
                }
                else
                {
                    current = current.Next;
                }
            }
        }

        public void RemoveData(int data)
        {
            CheckBeforeRemoving();

            Node current = Head;
            while (current.Next != null)
            {
                if (current.Next.Data == data)
                {
                    current.Next = current.Next.Next;
                    return;
                }
                current = current.Next;
            }

            Console.WriteLine("Data not found in List.");
        }

        public void CheckBeforeRemoving()
        {
            // If head is empty return
            if (Head == null)
            {
                return;
            }

            if (Head.Next == null)
            {
                Head = null;
                return;
            }
        }
        #endregion

        #region Helper Methods
        public Node FindNNodeFromEnd(int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("Please enter a positve integer value");
                return null;
            }

            Console.WriteLine($"The data {n} values from the end is [{FindNNodeFromEnd(Head, Head, n).Data}]");
            return FindNNodeFromEnd(Head, Head, n);
        }
        private Node FindNNodeFromEnd(Node mainPointer, Node refPointer, int n)
        {
            // No need to worry if n > number of Nodes.
            // refPointer will be null and exit.
            if (refPointer == null)
            {
                return mainPointer;
            }

            // Move refPointer n steps ahead before moving main pointer.
            if (n <= 0)
            {
                mainPointer = mainPointer.Next;
            }

            return FindNNodeFromEnd(mainPointer, refPointer.Next, --n);
        }
        public Node FindMiddle()
        {
            Console.WriteLine($"The middle node is [{FindMiddle(Head, Head).Data}].");
            return FindMiddle(Head, Head);
        }
        public Node FindMiddle(Node fastPointer, Node slowPointer)
        {
            // Base case
            if (fastPointer == null || fastPointer.Next == null)
            {
                return slowPointer;
            }

            return FindMiddle(fastPointer.Next.Next, slowPointer.Next);
        }
        public bool SearchData(int searchKey)
        {
            if (SearchData(Head, searchKey))
            {
                Console.WriteLine($"The data [{searchKey}] exist in the list.");
            }
            else
            {
                Console.WriteLine($"The data [{searchKey}] is not found.");
            }

            return SearchData(Head, searchKey);
        }
        public bool SearchData(Node current, int searchKey)
        {
            // Check for null reference first.
            // Otherwise, when you access current below,
            // Compiler will throw and exception.
            if (current == null)
            {
                return false;
            }

            // Data found.
            if (current.Data == searchKey)
            {
                return true;
            }

            return SearchData(current.Next, searchKey);
        }
        public SinglyLinkedList GetLength()
        {
            Console.WriteLine("There are " + GetLength(Head) + " Nodes inside the list");

            // console.writeline will still print, you need not need to deal return value.
            //return GetLength(Head);

            return this;
        }
        public int GetLength(Node current)
        {
            if (current == null) return 0;
            return GetLength(current.Next) + 1;
        }
        public void RemoveDuplicates()
        {
            Node current = Head;

            while (current != null && current.Next != null)
            {
                // duplicate found.
                if (current.Data == current.Next.Data)
                {
                    current.Next = current.Next.Next;
                }
                else
                {
                    current = current.Next;
                }
            }
        }
        public SinglyLinkedList RemoveDuplicate()
        {
            Console.WriteLine("Duplicates removed");
            RemoveDuplicate(Head);
            return this;
        }
        /// <summary>
        /// Remove duplicates recursively.
        /// </summary>
        /// <param name="current"></param>
        private void RemoveDuplicate(Node current)
        {
            // Recursion can be void return typed.
            // But, recursion must require a parameter.
            if (current == null || current.Next == null)
            {
                return;
            }

            if (current.Data == current.Next.Data)
            {
                current.Next = current.Next.Next;
            }
            else
            {
                // This will move current to next element.
                current = current.Next;
            }

            // Pass current and not current.Next, as the current needs to be re-evaluated.
            RemoveDuplicate(current);
        }
        public void PrintReverse()
        {
            PrintReverse(Head);
        }
        public void PrintReverse(Node head)
        {
            if (head == null)
            {
                return;
            }

            PrintReverse(head.Next);

            Console.Write("[" + head.Data + "] -> ");
        }
        public void Print(Node head)
        {
            if (head != null)
            {
                Console.Write("[" + head.Data + "] -> ");
            }

            Print(head.Next);
        }
        public SinglyLinkedList Print()
        {
            if (Head != null && DetectLoop(Head, Head) == false)
            {
                Head.Print();
            }

            // Add a blank row after printing.
            Console.WriteLine("\n");

            return this;
        }

        public bool ListEmpty() => Head == null ? true : false;

        #endregion

        #region Node Class
        /// <summary>
        /// Custom Node for Singly Linked List
        /// </summary>
        public class Node
        {
            public int Data { get; set; }
            // cannot use struct, as you need Node to point to something.
            public Node Next { get; set; }
            public Node(int data)
            {
                Data = data;
                Next = null;
            }
            /// <summary>
            /// Prints out all the data in each node.
            /// </summary>
            public void Print()
            {
                // Print this statement once.
                Console.Write("[" + Data + "] -> ");

                // If the next node is has data, print.
                if (Next != null)
                {
                    Next.Print();
                }
            }
            /// <summary>
            /// Create a new Node and add the data to end of the list.
            /// </summary>
            /// <param name="data"></param>
            public void AddToEnd(int data)
            {
                // if the next node does not contain an address to
                // another node. or this is the base condition.
                if (Next == null)
                {
                    // Create a new node and pass the data to the new node.
                    Next = new Node(data);
                }
                else
                {
                    // Call this method on the next node or the node 'beside' this node.
                    Next.AddToEnd(data);
                }
            }
            public void AddSorted(int data)
            {
                if (Next == null)
                {
                    Next = new Node(data);
                }
                else if (data < Next.Data)
                {
                    Node temp = new Node(data);
                    temp.Next = this.Next;
                    Next = temp;
                }
                else
                {
                    Next.AddSorted(data);
                }
            }
        }
        #endregion
    }
}
