using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class BinaryTreeApplication
    {
        public static void Main()
        {
            // Binary tree, 0,1,2 children.
            // Basics to Artifical intelligence.
            // Binary Tree or Binary Search Tree

            BinarySearchTree.NewTree
                .InsertNode(40)
                .InsertNode(1, 2, 3, 4, 5)
                .InsertNode(10, 11, 12, 13, 14, 15)
                .InsertNode(50, 51, 52, 53, 54, 23)
                .InsertNode(41, 42, 43, 44, 45, 46)
                .PreOrderTraversal(0)
                .Print("\n")
                .PreOrderTraversal()
                .Print("\n")
                .InOrderTraversal()
                .Print("\n")
                .SaveToArray()
                .PrintArray()
                .Print("\n")
                .FindNode(23);
        }

        class BinarySearchTree
        {
            // Basic rules of a binary search tree.
            // Every parent node has at most 2 children.
            // Every node to the left of a parent node is always less than the parent
            // Every node to the right of a parent node is always greater than the parent.

            private TreeNode Root;

            public static BinarySearchTree NewTree = new BinarySearchTree();
            private BinarySearchTree() { }

            #region Search / Print
            // Breath first search - not possible without queue, which is the opposite of stack.
            public BinarySearchTree BreathFirstSearch()
            {
                if (Root == null) return this;

                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(Root);

                while (queue.Count != 0)
                {
                    TreeNode current = queue.Dequeue();

                    Console.Write($"[{current.Data}] -> ");

                    if (current.Left != null) queue.Enqueue(current.Left);
                    if (current.Right != null) queue.Enqueue(current.Right);
                }

                return this;
            }
            // Depth first search
            // Pre order binary tree traversal

            public BinarySearchTree Print(string message = null)
            {
                Console.WriteLine(message);
                return this;
            }

            public BinarySearchTree InOrderTraversal()
            {
                InOrderTraversal(Root);
                return this;
            }

            private void PostOrderTraversal(TreeNode current)
            {
                if (current == null) return;
                PostOrderTraversal(current.Left);
                PostOrderTraversal(current.Right);
                Console.Write($"[{current.Data}] -> ");
            }

            /// <summary>
            /// Prints all the data in numerical sequence.
            /// </summary>
            /// <param name="current"></param>
            /// <returns></returns>
            private void InOrderTraversal(TreeNode current)
            {
                // Note: Recursion is a recursive stack call.
                // A stack call is Last in First out LIFO structure.
                if (current == null) return;
                InOrderTraversal(current.Left);
                Console.Write($"[{current.Data}] -> ");
                InOrderTraversal(current.Right);
            }

            private void InOrderTraversal(int x = 0)
            {
                if (Root == null) return;

                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode current = Root;

                while (stack.Count != 0 || current != null)
                {
                    if (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }
                    else
                    {
                        current = stack.Pop();
                        Console.Write($"[{current.Data}] -> ");
                        current = current.Right;
                    }
                }
            }

            public BinarySearchTree PreOrderTraversal()
            {
                PreOrderTraversal(Root);
                return this;
            }
            private void PreOrderTraversal(TreeNode current)
            {
                if (current == null) return;

                Console.Write($"[{current.Data}] -> ");

                PreOrderTraversal(current.Left);
                PreOrderTraversal(current.Right);
            }

            public BinarySearchTree PreOrderTraversal(int iterativeMethod = 0)
            {
                if (Root == null) return this;

                // Stack is last in first out LIFO
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(Root);

                // if stack is not empty, continue looping
                while (stack.Count != 0)
                {
                    // temp is assigned to the top node of the stack.
                    // stack removed that node after assigning temp.
                    TreeNode temp = stack.Pop();
                    Console.Write($"[{temp.Data}] -> ");

                    // right first, because of LIFO structure.
                    if (temp.Right != null)
                    {
                        stack.Push(temp.Right);
                    }

                    if (temp.Left != null)
                    {
                        stack.Push(temp.Left);
                    }

                    // temp.value is retained inside the while loop until
                    // overriden by a new value.
                }

                return this;
            }
            #endregion

            #region Helper Methods

            private TreeNode[] treeArr = new TreeNode[100];

            public BinarySearchTree PrintArray()
            {
                // Good idea to have a field count
                Count = 0;
                foreach (var tree in treeArr)
                {
                    // There will be numerous cases of null.
                    if(tree != null)
                    {
                        Console.Write($"[{tree.Data}] -> ");
                        Count++;
                    }

                    // After printing 5 elements, create new line
                    if (Count % 5 == 0)
                    {
                        Console.WriteLine();
                    }
                }

                return this;
            }
            public BinarySearchTree ClearArray()
            {
                for (int i = 0; i < treeArr.Length; i++)
                {
                    treeArr[i] = null;
                }

                return this;
            }

            private int Count;
            public BinarySearchTree SaveToArray()
            {
                // Set the count as a field member.
                Count = 0;
                SaveToArray(Root);
                return this;
            }
            private void SaveToArray(TreeNode current)
            {
                if (current == null) return;

                SaveToArray(current.Left);
                treeArr[Count++] = current;
                SaveToArray(current.Right);
            }

            public BinarySearchTree FindNode(int data)
            {
                if (Root == null) Console.WriteLine("Tree is empty.");

                bool isFound = FindNode(Root, data);

                if (isFound)
                    Console.WriteLine($"\n[{data}] is found.");
                else
                    Console.WriteLine($"\n[{data}] is not found.");

                return this;
            }
            private bool FindNode(TreeNode current, int data)
            {
                // False by default
                bool isFound;

                if (current == null) return false;
                if (current.Data == data) return true;

                if (data > current.Data)
                {
                    isFound = FindNode(current.Right, data);
                }
                else
                {
                    isFound = FindNode(current.Left, data);
                }

                return isFound;
            }

            private TreeNode Search(TreeNode current, int data)
            {
                if (current == null || current.Data == data) return current;

                if (data < current.Data)
                    return Search(current.Left, data);
                else
                    return Search(current.Right, data);
            }
            #endregion

            #region Add / Insert Methods
            public BinarySearchTree InsertNode(params int[] numbers)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    Root = InsertNode(Root, numbers[i]);
                }

                return this;
            }

            // Understand why need return value. 
            // Watch video Visualizing Data Structures
            private TreeNode InsertNode(TreeNode current, int data)
            {
                if (current == null)
                {
                    current = new TreeNode(data);
                    return current;
                }

                if (data < current.Data)
                {
                    current.Left = InsertNode(current.Left, data);
                }
                else
                {
                    current.Right = InsertNode(current.Right, data);
                }

                return current;
            }

            #endregion 
        }

        #region Tree Node
        class TreeNode
        {
            public int Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode(int data)
            {
                Data = data;
            }
        }
        #endregion
    }
}
