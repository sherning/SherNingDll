using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    /// <summary>
    /// A custom list similar to .NET list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CustomList<T> : IEnumerable<T>
    {
        #region Events
        public event EventHandler<CustomListEventArg> CapacityChanged;
        #endregion
        #region Properties and Fields
        /// <summary>
        /// Number of items inside List
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Actual size of the Custom List
        /// </summary>
        public int Capacity { get; private set; }
        /// <summary>
        /// Private array to hold objects
        /// </summary>
        private T[] Container;
        #endregion

        #region Contructors and Indexer
        public CustomList()
        {
            Capacity = 1;
            InitializeConstructor();
        }
        /// <summary>
        /// Define the size of the list
        /// </summary>
        /// <param name="capacity"> List Size </param>
        public CustomList(int capacity)
        {
            Capacity = capacity;
            InitializeConstructor();
        }
        /// <summary>
        /// Takes in a series of objects and add them to the container
        /// </summary>
        /// <param name="items"> Enter a list of objects seperated with a ',' </param>
        public CustomList(IEnumerable<T> items)
        {
            Capacity = items.Count();
            InitializeConstructor();
            foreach (var item in items)
            {
                Container[Count++] = item;
            }
        }
        public T this[int index]
        {
            get
            {
                CheckIfIndexIsValid(index);
                return Container[index];
            }
            set
            {
                // Set can only be used to change values in a list, 
                // and not for adding.
                // Think of set as replacing rather than adding.
                // Therefore the count is the same.
                CheckIfIndexIsValid(index);
                Container[index] = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Convert List to an Array.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] ret = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                ret[i] = Container[i];
            }
            return ret;
        }

        public CustomList<U> ConvertAll<U>(Converter<T, U> converter)
        {
            // Another Generic U type, cannot use back T
            CustomList<U> ret = new CustomList<U>(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                ret.Container[i] = converter(this.Container[i]);
            }
            ret.Count = this.Count;
            return ret;
        }
        public void ForEach(Action<T> action)
        {
            foreach (var item in this)
            {
                action(item);
            }
        }

        /// <summary>
        /// If Predicate is met, return true for all.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool TrueForAll(Predicate<T> match)
        {
            for (int i = 0; i < Count; i++)
            {
                if (match(Container[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void Add(T element)
        {
            // Ensure that Capacity is sufficient.
            EnsureCapacity();

            if (Count == Container.Length)
            {
                // Resize if necessary
                Resize();
            }

            // Add new element into container and increase Count.
            Container[Count++] = element;
        }

        // Addrange does not require shifting. 
        public void AddRange(params T[] objs)
        {
            int j = 0;
            int objectsCount = objs.Length;
            int totalCount = Count + objectsCount;

            // Check and make sure capacity is sufficient
            EnsureCapacity(Count + objectsCount);
            if (Count == Capacity)
            {
                // Resize container with new capacity value
                // Existing values will be retained in new container 
                // after resizing.
                Resize();
            }

            for (int i = Count; i < totalCount; i++)
            {
                Add(objs[j++]);
            }
        }

        /// <summary>
        /// Takes a method that returns boolean to evaluate what to remove
        /// </summary>
        /// <param name="index"> a method that takes a <T> parameter and returns bool </param>
        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index cannot be more than Count and Less than 0");
            }

            // Temp Array, since we are removing, 
            // the new array will be one count less than the previous.
            T[] temp = new T[Count - 1];

            // Trim Excess
            int j = 0;
            for (int i = 0; i < Count; i++)
            {
                // skip the index and fill in the rest.
                if (i != index)
                {
                    temp[j] = Container[i];
                    j++;
                }
            }

            Container = temp;

            // Capacity = Capacity - 1
            // Count = Count - 1
            Capacity = --Count;

            EventHandler<CustomListEventArg> handler = CapacityChanged;
            if (handler != null)
            {
                handler(this, new CustomListEventArg());
            }
        }

        /// <summary>
        /// Remove a Count of numbers in order from index positions given.
        /// </summary>
        /// <param name="index"> Zero based index. Remove at index position </param>
        /// <param name="count"> How many elements to be removed from index position </param>
        public void RemoveRange(int index, int count)
        {
            // Check for index out of range exception
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index cannot be more than Count and Less than 0");
            }

            // Total number of elements - the number of elements we want to remove.
            T[] temp = new T[Count - count];
            int j = 0;

            for (int i = 0; i < Count; i++)
            {
                // Copy values within this boundary to temp array
                if (i < index || i >= index + count)
                {
                    temp[j++] = Container[i];
                }
            }

            // Copy back temp to Container.
            Container = temp;

            // Check to resize Count and Capacity!
            Capacity = Capacity - count;
            Count = Count - count;
        }

        public void RemoveAll(Predicate<T> ifTrueRemoveElement)
        {
            for (int i = 0; i < Count; i++)
            {
                // Consider predicate like implementing a method.
                if (ifTrueRemoveElement(Container[i]))
                {
                    RemoveAt(i);
                    i--;
                }
            }
        }

        public bool Remove(T obj)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Container[i].Equals(obj))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int LastEmptyCell()
        {
            int lastEmptyCell = 0;
            for (int i = 0; i < Capacity; i++)
            {
                if (Container[i] != null || Container[i] != default)
                {
                    lastEmptyCell = i;
                }
            }
            return lastEmptyCell;
        }

        public bool isEmpty()
        {
            // Check if container is empty
            return Count == 0;
        }

        public bool Contains(T obj)
        {
            // if indexOf returns negative, the element cannot be located.
            // therefore, contains will return false.
            return IndexOf(obj) > 0 ? true : false;
        }

        public bool Exists(Predicate<T> ifTrueReturnTrue)
        {
            for (int i = 0; i < Count; i++)
            {
                if (ifTrueReturnTrue(Container[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < Count; i++)
            {
                if (match(Container[i]))
                {
                    return Container[i];
                }
            }
            Console.WriteLine("\nFind(Could not find what you are looking for!)\n");
            return default;
        }

        public IEnumerable<T> FindAll(Predicate<T> match)
        {
            T[] ret = new T[Count];
            int j = 0;

            for (int i = 0; i < Count; i++)
            {
                if (match(Container[i]))
                {
                    ret[j] = Container[i];
                    j++;
                }
            }

            // New container size is the number of matches above.
            // Prevent having many blank space.
            T[] temp = new T[j];

            // Resize return array container.
            for (int i = 0; i < j; i++)
            {
                temp[i] = ret[i];

                //if (ret[i] != default || ret[i] != null)
                //{
                //    temp[i] = ret[i];
                //}
            }

            return temp;
        }
        public void Insert(int index, T element)
        {
            if (Count == Container.Length)
            {
                Resize();
            }
            // Shuffle everyone down the array.
            Container = Copy(Container, index, index + 1);
            Container[index] = element;
            Count++;
        }

        public void InsertRange(int index, params T[] objs)
        {
            int j = 0;
            int objectsCount = objs.Length;

            // Check and make sure capacity is sufficient
            EnsureCapacity(Count + objectsCount);
            if (Count == Capacity)
            {
                // Resize container with new capacity value
                Resize();
            }
            Container = Copy(Container, index, index + objectsCount);
            for (int i = index; i < index + objectsCount; i++)
            {
                Container[i] = objs[j++];
                Count++;
            }

            // alternatively, this will work as well compared to Count++
            // Count = Count + objs.Length; 
        }

        public void InsertRange(int index, IEnumerable<T> objs)
        {
            int j = 0;
            T[] temp = new T[objs.Count()];
            temp = objs.ToArray();

            int objectsCount = temp.Length;

            // Check and make sure capacity is sufficient
            EnsureCapacity(Count + objectsCount);
            if (Count == Capacity)
            {
                // Resize container with new capacity value
                Resize();
            }
            Container = Copy(Container, index, index + objectsCount);
            for (int i = index; i < index + objectsCount; i++)
            {
                Container[i] = temp[j++];
                Count++;
            }

            // alternatively, this will work as well compared to Count++
            // Count = Count + objs.Length; 
        }

        public IEnumerable<T> GetRange(int index, int count)
        {
            T[] temp = new T[count];
            int j = 0;

            for (int i = index; i < index + count; i++)
            {
                temp[j++] = Container[i];
            }

            return temp;
        }

        public void TrimExcessCapacity()
        {
            T[] temp = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                temp[i] = Container[i];
            }

            Container = temp;
            Capacity = Container.Length;
        }
        public void Clear()
        {
            //if (typeof(T).BaseType.Equals(typeof(ValueType)))
            //{
            //    return;
            //}
            for (int i = 0; i < Count; i++)
            {
                Container[i] = default(T);
            }
            Count = 0;
        }
        /// <summary>
        /// Returns the index where the object is located. Returns -1 if object not found
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int IndexOf(T obj)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Container[i].Equals(obj))
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        #region Private Methods

        private void EnsureCapacity()
        {
            EnsureCapacity(Count + 1);
        }
        private void EnsureCapacity(int capacityRequired)
        {
            CustomListEventArg arg = new CustomListEventArg();
            arg.Capacity = this.Capacity;
            arg.Count = this.Count;

            if (capacityRequired > Capacity)
            {
                int targetSize = Capacity * 2;
                if (targetSize < capacityRequired)
                {
                    targetSize = capacityRequired;
                }
                if (Capacity == 0)
                {
                    Capacity = 1;
                }
                else
                {
                    Capacity = targetSize;
                }

                //CapacityChanged?.Invoke(this, arg);
            }

            //if (capacityRequired == Container.Length)
            //{
            //    if (Capacity == 0)
            //    {
            //        Capacity = 1;
            //    }
            //    else
            //    {
            //        Capacity *= 2;
            //    }
            //}
        }
        /// <summary>
        /// Copy all elements from starting index to ending index
        /// </summary>
        /// <param name="container"> Pass the container to be copied </param>
        /// <param name="startingIndex"> the starting index of the elements to be copied </param>
        /// <param name="endingIndex"> where you want the copied elements to start </param>
        /// <returns></returns>
        private T[] Copy(T[] container, int startingIndex, int endingIndex)
        {
            int z = 0;
            int y = 0;

            T[] returnContainer;
            int newArrayLength = container.Length + (endingIndex - startingIndex);

            T[] tempStorage;
            int tempStorageLength = container.Length - startingIndex;

            // initialize return Container
            returnContainer = new T[newArrayLength];

            // initalize temp Container
            tempStorage = new T[tempStorageLength];

            // copy items that needs to move
            for (int i = startingIndex; i < container.Length; i++)
            {
                tempStorage[y++] = container[i];
            }

            // Fill everything before the start of the startingindex
            for (int i = 0; i < startingIndex; i++)
            {
                returnContainer[i] = container[i];
            }

            // Fill the empty spaces in the middle
            for (int i = startingIndex; i < endingIndex; i++)
            {
                returnContainer[i] = default(T);
            }

            // fill the copied data into the array
            for (int i = endingIndex; i < newArrayLength; i++)
            {
                returnContainer[i] = tempStorage[z++];
            }

            return returnContainer;
        }
        private void Resize()
        {
            CustomListEventArg arg = new CustomListEventArg();
            arg.Capacity = this.Capacity;
            arg.Count = this.Count;

            T[] temp = new T[Capacity];
            for (int i = 0; i < Count; i++)
            {
                temp[i] = Container[i];
            }

            Container = temp;

            CapacityChanged?.Invoke(this, arg);
        }

        private void CheckIfIndexIsValid(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Please enter a non-negative number for the index");
            }
            if (index >= Capacity)
            {
                throw new IndexOutOfRangeException("Please enter a number that is less than the capacity");
            }
        }
        private void InitializeConstructor()
        {
            // Initialize the container
            Container = new T[Capacity];
        }
        #endregion

        #region Optimization Methods
        /// <summary>
        /// Optimised version of InsertRange Method
        /// </summary>
        /// <param name="index"></param>
        /// <param name="objs"></param>
        public void InsertRange_Optimised(int index, params T[] objs)
        {
            int j = 0;
            int objectsCount = objs.Length;

            // Check and make sure capacity is sufficient
            EnsureCapacity(Count + objectsCount);
            if (Count == Capacity)
            {
                // Resize container with new capacity value
                Resize();
            }
            Container = Copy(Container, index, index + objectsCount);
            for (int i = index; i < index + objectsCount; i++)
            {
                Container[i] = objs[j++];
                Count++;
            }
        }
        #endregion

        #region Implementing IEnumerable Interface
        public IEnumerator<T> MyCustomEnumerator()
        {
            return new CustomEnumerator(this);
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return Container[i];
            }
        }
        /// <summary>
        /// Return non-generic version of Enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Custom Enumerator
        private class CustomEnumerator : IEnumerator<T>
        {
            private int index = -1;
            private CustomList<T> MyList;

            public CustomEnumerator(CustomList<T> myList)
            {
                MyList = myList;
            }

            public T Current
            {
                get
                {
                    if (index < 0 || MyList.Count <= index)
                    {
                        return default(T);
                    }

                    return MyList[index];
                }
            }
            object IEnumerator.Current => Current;
            public void Dispose()
            {
                Console.WriteLine("Dispose() Method being called");
            }
            public bool MoveNext()
            {
                return index++ < MyList.Count - 1;
            }
            public void Reset()
            {
                index = -1;
            }
        }

        #endregion

        #region Custom EventArg
        public class CustomListEventArg : EventArgs
        {
            public int Capacity { get; set; }
            public int Count { get; set; }
        }

        #endregion
    }
}
