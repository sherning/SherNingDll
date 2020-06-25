using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class GraphApplication
    {
        public static void Main()
        {
            // Tree only one path.
            // directed (with direction) vs undirected (2-way connection / no direction)
            // weighted (with value) vs unweighted graphy
            // Vertex is a node

            // non linear data structure
            //AdjacencyMatrixTest();
            //Console.WriteLine();
            //AdjacencyListTest();
            //GraphTest();
            WeightedGraphTest();

        }

        private static void WeightedGraphTest()
        {
            WeightedGraph<string> map = new WeightedGraph<string>()
                .AddEdge("New York", new Edge<string>("London", 5567))
                .AddEdge("New York", new Edge<string>("Tokyo", 10840))
                .AddEdge("London", new Edge<string>("New York", 5567))
                .AddEdge("Tokyo", new Edge<string>("New York", 10840))
                .Print()
                .RemoveVertex("New York")
                .Print();

            WeightedGraph<int> numMap = new WeightedGraph<int>()
                .AddEdgeTwoWay(1, new Edge<int>(2, 0.0))
                .AddEdgeTwoWay(1, new Edge<int>(3, 0.0))
                .AddEdgeTwoWay(2, new Edge<int>(5, 0.0))
                .AddEdgeTwoWay(2, new Edge<int>(4, 0.0))
                .AddEdgeTwoWay(3, new Edge<int>(6, 0.0))
                .AddEdgeTwoWay(3, new Edge<int>(5, 0.0))
                .AddEdgeTwoWay(4, new Edge<int>(7, 0.0))
                .AddEdgeTwoWay(6, new Edge<int>(8, 0.0))
                .AddEdgeTwoWay(5, new Edge<int>(7, 0.0))
                .AddEdgeTwoWay(5, new Edge<int>(8, 0.0))
                .AddEdgeTwoWay(7, new Edge<int>(9, 0.0))
                .AddEdgeTwoWay(8, new Edge<int>(9, 0.0))
                .Print()
                .DepthFirstSearch(5)
                .BreathFirstSearch(5);

            WeightedGraph<char> alphaNumeric = new WeightedGraph<char>()
                .AddEdge('A', new Edge<char>('B', 4))
                .AddEdge('B', new Edge<char>('E', 3))
                .AddEdge('A', new Edge<char>('B', 4))
                .AddEdge('A', new Edge<char>('B', 4))
                .AddEdge('A', new Edge<char>('B', 4))
                .AddEdge('A', new Edge<char>('B', 4))
                .RemoveVertex('B')
                .Print();

            WeightedGraph<int> numMap2 = new WeightedGraph<int>()
               .AddEdge(1, new Edge<int>(2, 50))
               .AddEdge(1, new Edge<int>(3, 45))
               .AddEdge(1, new Edge<int>(4, 10))
               .AddEdge(2, new Edge<int>(3, 10))
               .AddEdge(2, new Edge<int>(4, 15))
               .AddEdge(3, new Edge<int>(5, 30))
               .AddEdge(4, new Edge<int>(1, 10))
               .AddEdge(4, new Edge<int>(5, 15))
               .AddEdge(5, new Edge<int>(2, 20))
               .AddEdge(5, new Edge<int>(3, 35))
               .AddEdge(6, new Edge<int>(5, 10))
               .Print()
               .DepthFirstSearch(1)
               .BreathFirstSearch(1)
              // .DijkstraAlgorithm(1, 3)
               .DijkstraAlgorithmQ(1, 3);

            WeightedGraph<char> udemy = new WeightedGraph<char>()
                .AddEdgeTwoWay('A', new Edge<char>('B', 4))
                .AddEdgeTwoWay('A', new Edge<char>('C', 2))
                .AddEdgeTwoWay('B', new Edge<char>('E', 3))
                .AddEdgeTwoWay('C', new Edge<char>('D', 2))
                .AddEdgeTwoWay('C', new Edge<char>('F', 4))
                .AddEdgeTwoWay('D', new Edge<char>('F', 1))
                .AddEdgeTwoWay('D', new Edge<char>('E', 3))
                .AddEdgeTwoWay('F', new Edge<char>('E', 1))
                .Print()
                .DijkstraAlgorithm('A', 'E')
                .DijkstraAlgorithmQ('A', 'E');
        }
        /// <summary>
        /// Weighted and Directional Graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class WeightedGraph<T>
        {
            public int NumOfVertices { get; private set; }

            // T vertice, List of Edge Objects.
            private Dictionary<T, List<Edge<T>>> VerticeList;

            public WeightedGraph()
            {
                VerticeList = new Dictionary<T, List<Edge<T>>>();
            }

            #region Dijkstra Algorithm
            // minimization problem is an optimization problem
            // Greedy method.
            // cannot use negative weight
            // Relaxation

            /// <summary>
            /// 
            /// </summary>
            /// <param name="start"> Starting Vertex </param>
            /// <param name="end"> Ending Vertex </param>
            public WeightedGraph<T> DijkstraAlgorithm_Legacy(T start, T end)
            {
                // Need more than one point for calculation.
                if (VerticeList.Count <= 1)
                {
                    Console.WriteLine("Insufficient vertice for calcuations.");
                    return this;
                }

                // Dictionary to store Vertex/bool
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // Initialize visited, set to all false.
                foreach (T vertex in VerticeList.Keys) visited[vertex] = false;

                // vertex vs shortest distance from start.
                Dictionary<T, double> distanceTable = new Dictionary<T, double>();

                // Initialize columns, set each vertex to positive infinity.
                foreach (T vertex in VerticeList.Keys) distanceTable.Add(vertex, double.PositiveInfinity);

                // vertex vs prev vertex travelled (<current,from>)
                Dictionary<T, T> previous = new Dictionary<T, T>();

                // Initialize previousVertex tracker, set it to default value.
                foreach (T vertex in VerticeList.Keys) previous[vertex] = default(T);

                // initialize source point.
                distanceTable[start] = 0;

                // Selected variable
                T selected = start;

                // previous
                previous[start] = start;

                // still have remaining nodes
                bool notAllNodesVisited = true;

                // once allNodesVisited == false, exit loop.
                while (notAllNodesVisited = visited.ContainsValue(false))
                {
                    // updated visited list for the next node.
                    visited[selected] = true;

                    // for each edge inside Graph
                    foreach (Edge<T> edge in VerticeList[selected])
                    {
                        // if visited? can skip.
                        if (visited[edge.Path] == false)
                        {
                            // Update distance table by relaxation.
                            // if (d[u] + c(u,v) < d[v]) then d[v] = d[u] + c(u,v)
                            if (distanceTable[previous[selected]] + edge.Weight < distanceTable[edge.Path])
                            {
                                // Update the distance table, d[v] = d[u] + c(u,v)
                                distanceTable[edge.Path] = distanceTable[previous[selected]] + edge.Weight;
                                previous[edge.Path] = edge.Path;
                            }
                        }
                    }

                    // find the smallest key inside distance table, by ascending.
                    // this also fixes the dead end problem.
                    var smallestVertex = distanceTable
                        .Where(x => visited[x.Key] == false)
                        .OrderBy(x => x.Value)
                        .FirstOrDefault();

                    selected = smallestVertex.Key;
                }

                // After completing distance table.
                Console.WriteLine($"\nShortest distance from start [{start}] -> [{end}] is {distanceTable[end]}");

                return this;
            }

            /// <summary>
            /// Better version using Priority Queue.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            public WeightedGraph<T> DijkstraAlgorithmQ(T start, T end)
            {
                // Use a priority queue instead of my original recipe.
                PriorityQueue<T> queue = new PriorityQueue<T>();

                // Distance
                Dictionary<T, double> distance = new Dictionary<T, double>();

                // Previous
                Dictionary<T, T> previous = new Dictionary<T, T>();

                // Store the shortest path
                List<T> paths = new List<T>();

                // Initialization of containers
                foreach (T vertex in VerticeList.Keys)
                {
                    // Check if vertex == start
                    if (EqualityComparer<T>.Default.Equals(vertex, start))
                    {
                        distance[vertex] = 0;
                        queue.Enqueue(vertex, 0);
                    }
                    else
                    {
                        distance[vertex] = double.PositiveInfinity;
                        queue.Enqueue(vertex, double.PositiveInfinity);
                    }

                    // Set previous to default, istead of null for generic T.
                    previous[vertex] = default;
                }

                Console.WriteLine("\nAfter initialization: ");
                queue.Print();

                /* ---------------------------------- Main Logic ---------------------------------- */

                while (queue.Count != 0)
                {
                    // start will be the very first node to be dequeued.
                    T smallest = queue.Dequeue().Data;

                    // If smallest == end, will end the logic.
                    if (EqualityComparer<T>.Default.Equals(smallest, end))
                    {
                        // we are done, build path to return.
                        while (true)
                        {
                            // Keep track of the path.
                            paths.Add(smallest);

                            // Check if path is connected to the start, yes break out of loop.
                            if (EqualityComparer<T>.Default.Equals(smallest, start)) break;

                            // else, continue looping.
                            smallest = previous[smallest];
                        }
                        break;
                    }

                    // if (distance[smallest] != double.PositiveInfinity) or smallest != end
                    else
                    {
                        //explore each vertex path inside WeightedGraph<T> index : smallest
                        foreach (Edge<T> edge in VerticeList[smallest])
                        {
                            // Calculate new distance to neighbouring edge.
                            double selected = distance[smallest] + edge.Weight;

                            // if (d[u] + c(u,v) < d[v]) then d[v] = d[u] + c(u,v)
                            if (selected < distance[edge.Path])
                            {
                                // Update distance table by relaxation.
                                distance[edge.Path] = selected;

                                // update previous table
                                previous[edge.Path] = smallest;

                                // update the queue.
                                queue.Enqueue(edge.Path, selected);
                            }
                        }

                        queue.Print();
                    }
                }

                // Print out the path.
                Console.WriteLine($"\nThe shortest path from [{start}] to [{end}] is: ");
                foreach (T path in paths.Reverse<T>()) Console.Write($"[{path}] -> ");

                // Print 'end'
                Console.WriteLine("End\n");

                return this;
            }

            public WeightedGraph<T> DijkstraAlgorithm(T start, T end)
            {
                // Need more than one point for calculation.
                if (VerticeList.Count <= 1)
                {
                    Console.WriteLine("Insufficient vertice for calcuations.");
                    return this;
                }

                // Dictionary to store data
                Dictionary<T, bool> visited = new Dictionary<T, bool>();
                Dictionary<T, double> distanceTable = new Dictionary<T, double>();
                Dictionary<T, T> previous = new Dictionary<T, T>();
                List<T> shortestPath = new List<T>();

                // initialize all the data.
                foreach (T vertex in VerticeList.Keys)
                {
                    if (EqualityComparer<T>.Default.Equals(vertex, start))
                    {
                        distanceTable[vertex] = 0;
                    }
                    else
                    {
                        distanceTable[vertex] = double.PositiveInfinity;
                    }

                    visited[vertex] = false;
                    previous[vertex] = default;
                }

                while (true)
                {
                    // this LINQ + visited table replaces priority queue.
                    T selected = distanceTable
                        .Where(x => visited[x.Key] == false)
                        .OrderBy(x => x.Value)
                        .FirstOrDefault()
                        .Key;

                    // Doing so i assume the logic will always calculate the shortest path.
                    if (EqualityComparer<T>.Default.Equals(selected, end))
                    {
                        while (true)
                        {
                            // Use a list to store the shortest path.
                            shortestPath.Add(selected);
                            if (EqualityComparer<T>.Default.Equals(selected, start)) break;
                            selected = previous[selected];
                        }
                        break;
                    }
                    else
                    {
                        // for each edge inside List<Edge<T>> of selected
                        foreach (Edge<T> edge in VerticeList[selected])
                        {
                            // if (d[u] + c(u,v) < d[v]) then d[v] = d[u] + c(u,v)
                            if (distanceTable[selected] + edge.Weight < distanceTable[edge.Path])
                            {
                                // Update the distance table, d[v] = d[u] + c(u,v)
                                distanceTable[edge.Path] = distanceTable[selected] + edge.Weight;
                                previous[edge.Path] = selected;
                            }
                        }
                    }

                    visited[selected] = true;
                }

                // After completing distance table.
                Console.WriteLine($"\nShortest distance from start [{start}] -> [{end}] is {distanceTable[end]}");

                foreach (T vertex in shortestPath.Reverse<T>()) Console.Write($"[{vertex}] -> ");
                Console.WriteLine("End\n");

                return this;
            }

            #endregion

            #region Add Methods
            public WeightedGraph<T> AddEdge(T vertex1, Edge<T> vertex2)
            {
                // If DicList does not contain the vertex1 then add.
                if (!VerticeList.ContainsKey(vertex1)) AddVertex(vertex1);

                // Ensure that vertex2 key exists
                if (!VerticeList.ContainsKey(vertex2.Path)) AddVertex(vertex2.Path);

                // Check if path exists in list
                foreach (var edge in VerticeList[vertex1])
                {
                    // edge is the edge object stored in ListOfEdges, 
                    // vertex2 is the new edge you want to add.
                    if (EqualityComparer<T>.Default.Equals(edge.Path, vertex2.Path))
                    {
                        Console.WriteLine("Edge already exist.");
                        return this;
                    }
                }

                // For directional graph this is sufficient
                VerticeList[vertex1].Add(vertex2);

                // Make it directional
                // VerticeList[vertex2.Path].Add(new Edge<T>(vertex1, vertex2.Weight));

                return this;
            }

            public WeightedGraph<T> AddEdgeTwoWay(T vertex1, Edge<T> vertex2)
            {
                // If DicList does not contain the vertex1 then add.
                if (!VerticeList.ContainsKey(vertex1)) AddVertex(vertex1);

                // Ensure that vertex2 key exists
                if (!VerticeList.ContainsKey(vertex2.Path)) AddVertex(vertex2.Path);

                // For directional graph this is sufficient
                VerticeList[vertex1].Add(vertex2);

                // Make it directional
                VerticeList[vertex2.Path].Add(new Edge<T>(vertex1, vertex2.Weight));

                return this;
            }

            public WeightedGraph<T> AddVertex(T vertex)
            {
                // VerticeList[vertex] = new List<T>();
                // both methods work. Dictionary with key or add.
                VerticeList.Add(vertex, new List<Edge<T>>());
                NumOfVertices++;

                return this;
            }
            #endregion

            #region Remove Methods
            public WeightedGraph<T> RemoveEdge(T vertex, T edge)
            {
                // If it does not contain the current vertex and destination vertex.
                // '|' instead of "||" means it evaluates both.
                if (!VerticeList.ContainsKey(vertex) | !VerticeList.ContainsKey(edge))
                {
                    Console.WriteLine("\nThe vertex does not exist.\n");
                    return this;
                }

                // VerticeList[vertex] return a list of edges
                foreach (var edgePath in VerticeList[vertex])
                {
                    if (EqualityComparer<T>.Default.Equals(edgePath.Path, edge))
                    {
                        VerticeList[vertex].Remove(edgePath);

                        // After finding one path, return to caller.
                        return this;
                    }
                }

                return this;
            }

            public WeightedGraph<T> RemoveVertex(T vertex)
            {
                if (VerticeList.ContainsKey(vertex) == false)
                {
                    Console.WriteLine("The vertex does not exist.\n");
                    return this;
                }

                // remove all edges
                foreach (var edgeList in VerticeList.Values)
                {
                    for (int i = 0; i < edgeList.Count; i++)
                    {
                        if (EqualityComparer<T>.Default.Equals(edgeList[i].Path, vertex))
                        {
                            edgeList.Remove(edgeList[i]);
                        }
                    }
                }

                // Remove vertex
                VerticeList.Remove(vertex);
                NumOfVertices--;

                return this;
            }
            #endregion

            #region Print and Transversal
            public List<T> CheckForDeadEnds(T source)
            {
                List<T> ret = new List<T>();

                // basic checks
                if (VerticeList.Count == 0)
                {
                    Console.WriteLine("List is empty.");
                    return null;
                }

                if (VerticeList.ContainsKey(source) == false)
                {
                    Console.WriteLine($"Source \"{source}\" does not exist.");
                    return null;
                }

                // Create a dictionary to store key value pairs
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // initialize each vertex to false.
                foreach (var vertex in VerticeList.Keys)
                {
                    // add key and value.
                    visited.Add(vertex, false);
                }

                // Use a queue to save and remove each visited node.
                Queue<T> queue = new Queue<T>();

                // source node visited.
                visited[source] = true;

                // Add source node to queue.
                queue.Enqueue(source);

                // Since source node is in queue, Count while != 0
                while (queue.Count != 0)
                {
                    // Remove the node saved in queue
                    source = queue.Dequeue();

                    // Print the node.
                    Console.Write($"[{source}] -> ");
                    ret.Add(source);

                    // explore each List stored in Dictionary. Iterate through.
                    foreach (var path in VerticeList[source])
                    {
                        // if Dic<T,bool> path has not been visited then
                        if (visited[path.Path] == false)
                        {
                            visited[path.Path] = true;
                            queue.Enqueue(path.Path);
                        }
                    }
                }

                // Every single node has been visited = true, exit.
                Console.WriteLine("End\n");

                return ret;
            }
            public WeightedGraph<T> BreathFirstSearch(T source)
            {
                // basic checks
                if (VerticeList.Count == 0)
                {
                    Console.WriteLine("List is empty.");
                    return this;
                }

                if (VerticeList.ContainsKey(source) == false)
                {
                    Console.WriteLine($"Source \"{source}\" does not exist.");
                    return this;
                }

                Console.WriteLine("\nBreath First Search: Source " + source);

                // Create a dictionary to store key value pairs
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // initialize each vertex to false.
                foreach (var vertex in VerticeList.Keys)
                {
                    // add key and value.
                    visited.Add(vertex, false);
                }

                // Use a queue to save and remove each visited node.
                Queue<T> queue = new Queue<T>();

                // source node visited.
                visited[source] = true;

                // Add source node to queue.
                queue.Enqueue(source);

                // Since source node is in queue, Count while != 0
                while (queue.Count != 0)
                {
                    // Remove the node saved in queue
                    source = queue.Dequeue();

                    // Print the node.
                    Console.Write($"[{source}] -> ");

                    // explore each List stored in Dictionary. Iterate through.
                    foreach (var path in VerticeList[source])
                    {
                        // if Dic<T,bool> path has not been visited then
                        if (visited[path.Path] == false)
                        {
                            visited[path.Path] = true;
                            queue.Enqueue(path.Path);
                        }
                    }
                }

                // Every single node has been visited = true, exit.
                Console.WriteLine("End\n");

                return this;
            }

            public WeightedGraph<T> DepthFirstSearch(T source)
            {
                Console.WriteLine("\nDepth First Search: Source " + source);
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // Store each vertex key and default it to false.
                foreach (var vertex in VerticeList.Keys)
                {
                    // initialize each element in visited to default
                    visited.Add(vertex, false);
                }

                if (VerticeList.Count == 0)
                {
                    Console.WriteLine("List is empty.");
                    return this;
                }

                if (!VerticeList.ContainsKey(source))
                {
                    Console.WriteLine($"Source \"{source}\" does not exist.");
                    return this;
                }

                // run depth first search recursively.
                DepthFirstSearch(ref visited, source);

                // write end of the statement and print line.
                Console.WriteLine("End\n");
                return this;
            }

            private void DepthFirstSearch(ref Dictionary<T, bool> visited, T source)
            {
                // store key: vertice, value: bool in dictionary.
                visited[source] = true;

                // print the source. 
                Console.Write($"[{source}] -> ");

                // explore each path inside list, check to see if visited.
                // The Dictionary is a reference type, so it will hold during recursive return.
                // It did NOT pass a copy rather it pass the reference. ref for illustration.
                foreach (Edge<T> path in VerticeList[source])
                    if (visited[path.Path] == false) DepthFirstSearch(ref visited, path.Path);
            }
            public WeightedGraph<T> Print()
            {
                Console.WriteLine($"\nGraph Representation with {NumOfVertices} vertices");
                foreach (var vertice in VerticeList)
                {
                    // For each vertice (key)
                    Console.Write($"Vertice [{vertice.Key}] => ");

                    // For each List of Edges, print each edge
                    foreach (var edge in vertice.Value)
                    {
                        Console.Write($"{edge}");
                    }

                    Console.WriteLine("end");
                }

                return this;
            }
            #endregion
        }

        #region Edge Class
        /// <summary>
        /// Edge class to store the path and weight.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class Edge<T> : IComparable<Edge<T>>
        {
            public T Path { get; private set; }
            public double Weight { get; private set; }

            public Edge(T path, double weight)
            {
                Path = path;
                Weight = weight;
            }

            public int CompareTo(Edge<T> other)
            {
                if (Weight < other.Weight) return -1;

                else if (Weight > other.Weight) return +1;

                else return 0;
            }

            public override string ToString()
            {
                return string.Format($"[{Path}, {Weight} km] -> ");
            }

        }
        #endregion

        #region Priority Queue
        class PriorityQueue<T>
        {
            // priority 1 is the highest priority.
            // MinBinaryHeap.
            // Enqueue accepts a value and priority
            // Dequeue remove root element
            // Change the > < operators to reflect min Binary Heap.

            private Node<T>[] Nodes;
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

            public PriorityQueue()
            {
                // Initialize the array.
                Nodes = new Node<T>[2];
                Count = 0;
                Index = -1;
                Length = 0;
            }

            #region Add Methods
            public PriorityQueue<T> Enqueue(T data, double priority)
            {
                Insert(data, priority);
                return this;
            }

            private void Insert(T data, double priority)
            {
                Insert(new Node<T>(data, priority));
            }

            private void Insert(Node<T> data)
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
                        Node<T> temp = Nodes[current];
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
                Node<T>[] temp;

                // Number of elements = lenght of array
                if (Count == Nodes.Length)
                {
                    // Expand array.
                    Length = Nodes.Length * 2;
                    temp = new Node<T>[Length];

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
                            Node<T> temp = Nodes[parent];
                            Nodes[parent] = Nodes[leftChild];
                            Nodes[leftChild] = temp;

                            // Assign parent to left child
                            parent = leftChild;
                        }
                        else
                        {
                            // swap right child with parent
                            Node<T> temp = Nodes[parent];
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
            public PriorityQueue<T> PrintInfo()
            {
                Console.WriteLine("Number of elements in heap: " + Count);
                Console.WriteLine("Length of heap: " + Length);
                return this;
            }
            public PriorityQueue<T> Print()
            {
                for (int i = 0; i < Count; i++)
                {
                    if (i == Count - 1)
                        Console.Write($"[{Nodes[i].Data},{Nodes[i].Priority}]");
                    else
                        Console.Write($"[{Nodes[i].Data},{Nodes[i].Priority}] -> ");
                }

                // Insert blank line
                Console.WriteLine();
                return this;
            }
            #endregion

            #region Remove Methods
            public Node<T> Dequeue()
            {
                Node<T> min = ExtractMin(0);
                if (min != null)
                {
                    Console.WriteLine("Extracted Min value: " + $"[{min.Data}, {min.Priority}]");
                }
                return min;
            }

            // Overload requires different parameter signature.
            private Node<T> ExtractMin(int x = 0)
            {
                // There are no elements in the array.
                if (Index < 0)
                {
                    Console.WriteLine("The heap is empty");
                    return null;
                }

                // return the root
                Node<T> root = Nodes[0];

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
            public PriorityQueue<T> Clear()
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
            public class Node<U>
            {
                public U Data { get; set; }
                public double Priority { get; set; }
                public Node(U data, double priority)
                {
                    Data = data;
                    Priority = priority;
                }
            }
        }
        #endregion

        /// <summary>
        ///  Non-directional Adjacency List. Generic Version
        /// </summary>
        /// 
        private static void GraphTest()
        {
            string search = "New York";

            Graph<string> maps = new Graph<string>()
                .AddVertex("Tokyo")
                .AddVertex("New York")
                .AddVertex("London")
                .AddEdge("Tokyo", "New York")
                .AddEdge("London", "New York")
                .AddEdge("Singapore", "London")
                .AddEdge("New York", "Seoul")
                .Print()
                //.RemoveVertex("New York")
                //.Print()
                .DepthFirstSearch(search)
                .BreadthFirstSearch(search);

            Graph<int> num = new Graph<int>()
                .AddEdge(1, 2)
                .AddEdge(1, 4)
                .AddEdge(1, 5)
                .AddEdge(2, 7)
                .AddEdge(2, 6)
                .AddEdge(2, 3)
                .Print()
                .BreadthFirstSearch(1)
                .DepthFirstSearch(1);

            Graph<int> binaryTree = new Graph<int>()
                .AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(2, 4)
                .AddEdge(2, 5)
                .AddEdge(3, 6)
                .AddEdge(3, 7)
                .Print()
                .BreadthFirstSearch(1)
                .DepthFirstSearch(1);

            Graph<int> diamond = new Graph<int>()
                .AddEdge(1, 2)
                .AddEdge(1, 3)
                .AddEdge(2, 5)
                .AddEdge(2, 4)
                .AddEdge(3, 6)
                .AddEdge(3, 5)
                .AddEdge(4, 7)
                .AddEdge(6, 8)
                .AddEdge(5, 7)
                .AddEdge(5, 8)
                .AddEdge(7, 9)
                .AddEdge(8, 9)

                // Watch this video to understand BFS and DFS.
                // https://www.youtube.com/watch?v=pcKY4hjDrxk
                // Excellent video to understand how graph traverse.

                .BreadthFirstSearch(8)
                .DepthFirstSearch(7)
                .Print();
        }
        class Graph<T>
        {
            // Traversing graph DFS BFS
            // use List of array with ensure capacity.
            // List to expend the number of vertex.
            // Array to store the edge.
            public int NumOfVertices { get; private set; }

            // Dictionary(keys,values) : keys are vertex, values are the lists of edges
            private Dictionary<T, List<T>> VerticeList;

            public Graph()
            {
                VerticeList = new Dictionary<T, List<T>>();
            }

            #region Add Methods
            // Add an edge between two vertices
            public Graph<T> AddEdge(T vertex1, T vertex2)
            {
                // If DicList does not contain the vertex1 then add.
                if (!VerticeList.ContainsKey(vertex1)) AddVertex(vertex1);

                // Ensure that vertex2 key exists
                if (!VerticeList.ContainsKey(vertex2)) AddVertex(vertex2);

                // For directional graph this is sufficient
                VerticeList[vertex1].Add(vertex2);

                // Add this for two way, non-directional graph
                VerticeList[vertex2].Add(vertex1);

                return this;
            }

            public Graph<T> AddVertex(T vertex)
            {
                // VerticeList[vertex] = new List<T>();
                // both methods work. Dictionary with key or add.
                VerticeList.Add(vertex, new List<T>());
                NumOfVertices++;

                return this;
            }
            #endregion

            #region Remove Methods
            public Graph<T> RemoveVertex(T vertex)
            {
                // ensure that the dic contains the vertex.
                if (VerticeList.ContainsKey(vertex) == false) return this;

                // remove all the edges first.
                foreach (var vertice in VerticeList)
                {
                    // use a for loop instead, foreach will have null problems
                    for (int i = 0; i < vertice.Value.Count; i++)
                    {
                        // if does not contain vertex break
                        if (!vertice.Value.Contains(vertex)) break;

                        // each vertice.Value is a list of paths.
                        if (vertice.Value[i].Equals(vertex))
                        {
                            vertice.Value.Remove(vertex);
                        }
                    }
                }

                // finally then remove vertex.
                VerticeList.Remove(vertex);
                NumOfVertices--;

                return this;
            }

            public Graph<T> RemoveEdge(T vertex1, T vertex2)
            {
                // Check if the list contains these two vertices.
                if (VerticeList.ContainsKey(vertex1) && VerticeList.ContainsKey(vertex2))
                {
                    VerticeList[vertex1].Remove(vertex2);
                    VerticeList[vertex2].Remove(vertex1);
                }

                return this;
            }
            #endregion

            #region Traversal and Print
            public Graph<T> BreadthFirstSearch(T source)
            {
                // basic checks
                if (VerticeList.Count == 0)
                {
                    Console.WriteLine("List is empty.");
                    return this;
                }

                if (VerticeList.ContainsKey(source) == false)
                {
                    Console.WriteLine($"Source \"{source}\" does not exist.");
                    return this;
                }

                Console.WriteLine("\nBreath First Search: Source " + source);

                // Create a dictionary to store key value pairs
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // initialize each vertex to false.
                foreach (var vertex in VerticeList.Keys)
                {
                    // add key and value.
                    visited.Add(vertex, false);
                }

                // Use a queue to save and remove each visited node.
                Queue<T> queue = new Queue<T>();

                // source node visited.
                visited[source] = true;

                // Add source node to queue.
                queue.Enqueue(source);

                // Since source node is in queue, Count while != 0
                while (queue.Count != 0)
                {
                    // Remove the node saved in queue
                    source = queue.Dequeue();

                    // Print the node.
                    Console.Write($"[{source}] -> ");

                    // explore each List stored in Dictionary. Iterate through.
                    foreach (var path in VerticeList[source])
                    {
                        // if Dic<T,bool> path has not been visited then
                        if (visited[path] == false)
                        {
                            visited[path] = true;
                            queue.Enqueue(path);
                        }
                    }
                }

                // Every single node has been visited = true, exit.
                Console.WriteLine("End\n");

                return this;
            }

            public Graph<T> DepthFirstSearch(T source)
            {
                Console.WriteLine("\nDepth First Search: Source " + source);
                Dictionary<T, bool> visited = new Dictionary<T, bool>();

                // Store each vertex key and default it to false.
                foreach (var vertex in VerticeList.Keys)
                {
                    // initialize each element in visited to default
                    visited.Add(vertex, false);
                }

                if (VerticeList.Count == 0)
                {
                    Console.WriteLine("List is empty.");
                    return this;
                }

                if (!VerticeList.ContainsKey(source))
                {
                    Console.WriteLine($"Source \"{source}\" does not exist.");
                    return this;
                }

                // run depth first search recursively.
                DepthFirstSearch(visited, source);

                // write end of the statement and print line.
                Console.WriteLine("End\n");

                return this;
            }

            // Where is the base case for this ?
            // Where is the point to call the next iteration ?
            // Recursive is using stack.
            private void DepthFirstSearch(Dictionary<T, bool> visited, T source)
            {
                // store key: vertice, value: bool in dictionary.
                visited[source] = true;

                // print the source. 
                Console.Write($"[{source}] -> ");

                // Temp list to store path list for each vertice.
                List<T> verticeList = VerticeList[source];

                // explore each path inside list, check to see if visited.
                foreach (T path in verticeList)
                {
                    // If path has not been visited, start recursice call.
                    // this is the base case. when visited all == true, exit.
                    if (visited[path] == false) DepthFirstSearch(visited, path);
                }
            }

            public Graph<T> Print()
            {
                Console.WriteLine($"\nGraph Representation with {NumOfVertices} vertices");
                foreach (var vertice in VerticeList)
                {
                    // For each vertice (key)
                    Console.Write($"Vertice [{vertice.Key}] => ");

                    // For each vertice, print all the paths
                    foreach (var path in vertice.Value)
                    {
                        Console.Write($"[{path}] -> ");
                    }

                    Console.WriteLine("end");
                }

                return this;
            }
            #endregion
        }

        #region Adjacency Matrix
        private static void AdjacencyMatrixTest()
        {
            // for undirected graph. No specific directions indicated.
            AdjacencyMatrix matrix = new AdjacencyMatrix(4);
            matrix.AddEdge(0, 1);
            matrix.AddEdge(1, 2);
            matrix.AddEdge(2, 3);
            matrix.AddEdge(3, 0);
            matrix.Print();
        }

        class AdjacencyMatrix
        {
            // Multi Dimension Array
            private int[,] Matrix;
            public AdjacencyMatrix(int nodes)
            {
                Matrix = new int[nodes, nodes];
            }

            public void AddEdge(int row, int col)
            {
                Matrix[row, col] = 1;
                Matrix[col, row] = 1;
            }

            public void Print()
            {

                // Visual Representation of a Matrix.
                // GetLength(0) == [x,y] returns x
                Console.WriteLine("\t 0\t 1\t 2\t 3");
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    // Rank is dimensions, our case is 2-D array, it will return 2.
                    Console.Write(i + "\t");

                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        // returns the value stored in each matrix location.
                        Console.Write($"[{Matrix[i, j]}]" + "\t");
                    }

                    Console.WriteLine();
                }
            }


        }
        private static void GetLengthTest()
        {
            // To understand GetLength see this example.
            int[,,] matrix = new int[3, 4, 5];
            Console.WriteLine(matrix.GetLength(0));
            Console.WriteLine(matrix.GetLength(1));
            Console.WriteLine(matrix.GetLength(2));

            // where as rank returns the number of dimensions
            Console.WriteLine("Rank: " + matrix.Rank);
        }
        #endregion

        #region Adjacency List
        private static void AdjacencyListTest()
        {
            AdjacencyList list = new AdjacencyList(4);
            // From node 0 to node 1
            list.AddEdge(0, 1);

            // from node 1 to node 2
            list.AddEdge(1, 2);

            // from node 2 to node 3
            list.AddEdge(2, 3);

            // from node 3 to node 0
            list.AddEdge(3, 0);

            // Breath first search
            list.BreadthFirstSearch(0);

            // Print
            list.Print();


            Console.WriteLine("\nTest List 2");
            AdjacencyList list2 = new AdjacencyList(4);
            list2.AddEdge(0, 1);
            list2.AddEdge(0, 2);
            list2.AddEdge(1, 2);
            list2.AddEdge(2, 0);
            list2.AddEdge(2, 3);
            list2.AddEdge(3, 3);
            list2.BreadthFirstSearch(2);
            list2.DepthFirstSearch(0);
            list2.Print();
        }

        class AdjacencyList
        {
            // an array of linked list.
            private LinkedList<int>[] List;
            private int Vertices;

            public AdjacencyList(int nodes)
            {
                Vertices = 0;
                List = new LinkedList<int>[nodes];
                for (int i = 0; i < nodes; i++)
                {
                    // initialize each linked list
                    List[i] = new LinkedList<int>();
                }
            }

            /// <summary>
            /// Iterate the entire graph starting with souce vertice.
            /// </summary>
            /// <param name="source"></param>
            public void DepthFirstSearch(int source)
            {
                Console.WriteLine("\nDepth First Search: Source " + source);

                // Total number of unique nodes
                bool[] visited = new bool[Vertices];

                // Create a stack, Last in First out
                Stack<int> stack = new Stack<int>();

                // put the source, the first node into the stack
                stack.Push(source);

                // so long as stack is not empty continue looping
                while (stack.Count != 0)
                {
                    // Remove the node from the stack
                    int temp = stack.Pop();

                    // if not has not been visited then
                    if (visited[temp] == false)
                    {
                        // set the node to visited
                        visited[temp] = true;

                        // print the node
                        Console.Write($"[{temp}] -> ");

                        // since it is a array of linked list, you need to use enumerator.
                        // Foreach will call it's own Enumerator.MoveNext
                        foreach (int node in List[temp])
                        {
                            int n = node;

                            if (visited[n] == false)
                            {
                                stack.Push(n);
                            }
                        }
                    }
                }

                Console.WriteLine("null\n");
            }

            /// <summary>
            /// Iterate the entire graph once.
            /// </summary>
            /// <param name="source">starting node to begin</param>
            public void BreadthFirstSearch(int source)
            {
                Console.WriteLine("\nBreath First Search: Source " + source);

                // Create an array to mark which nodes have been visited.
                bool[] visited = new bool[Vertices];

                // Use a queue to save and remove each visited node.
                Queue<int> queue = new Queue<int>();

                // source node is visited.
                visited[source] = true;

                // Add source node to queue.
                queue.Enqueue(source);

                // Since source node is in queue, Count while != 0
                while (queue.Count != 0)
                {
                    // Remove the node saved in queue
                    source = queue.Dequeue();

                    // Print the node.
                    Console.Write($"[{source}] -> ");

                    // Use an enumerator to iterate the array inside the list.
                    IEnumerator<int> rator = List[source].GetEnumerator();

                    // IEnumerator interface has movenext() method.
                    while (rator.MoveNext() == true)
                    {
                        // use this method if you dont know the length
                        int n = rator.Current;


                        if (visited[n] == false)
                        {
                            visited[n] = true;
                            queue.Enqueue(n);
                        }
                    }
                }

                // Every single node has been visited = true, exit.
                Console.WriteLine("null\n");
            }

            /// <summary>
            /// Add Edge between current and destination vertex.
            /// </summary>
            /// <param name="vertex"> current vertex</param>
            /// <param name="desVertex">destination vertex</param>
            public void AddEdge(int vertex, int desVertex)
            {
                // From node x to node y for directed. only need this.
                List[vertex].AddFirst(desVertex);

                // From node y to node x - is required for non-directed.
                List[desVertex].AddFirst(vertex);

                Vertices++;
            }

            public void Print()
            {
                // Visual representation of an Array of LinkedList.
                Console.WriteLine("\nVisual Representation of an Array of LinkedList");
                for (int i = 0; i < List.Length; i++)
                {
                    // The head is stored here.
                    Console.Write($"[{i}] -> ");

                    foreach (var node in List[i])
                    {
                        Console.Write($"[{node}] -> ");
                    }

                    // End of the list is pointing to null.
                    Console.WriteLine("null");
                }
            }
        }
        #endregion
    }
}
