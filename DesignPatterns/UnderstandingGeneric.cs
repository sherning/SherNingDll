using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class UnderstandingGeneric
    {
        // Good use of generics when you need T to be both Icomparable and IEnumerable. i.e.
        // In order to understand this keyword, you need to know the distinction between
        // local variables and class members. 
        // this can only be used to refer to the object per se. 
        public static void Main()
        {
            MyList<int> numArr = new MyList<int>();
            numArr.Add(1)
                    .Add(23)
                    .Add(25)
                    .Add(92)
                    .Display();

            MyList<string> strArr = new MyList<string>("I am");
            strArr.Add("a")
                    .Add("Quant")
                    .Display();
        }

        class MyList<T>
        {
            // cannot inherit from value types 
            // as they are struct and by default implicitly sealed.
            public T[] arr = new T[2];

            private int CurrentIndex;

            public MyList()
            {
                CurrentIndex = 0;
            }

            public MyList(T element)
            {
                // This refers to the object and its class members.
                this.Add(element);
            }

            public MyList<T> Add(T element)
            {
                if (CurrentIndex == arr.Length)
                {
                    EnsureCapacity();
                }
                
                arr[CurrentIndex++] = element;

                return this;
            }

            public void Display()
            {
                // arr[0].GetType() == typeof(T)
                Console.WriteLine($"\nDisplaying {typeof(T)} elements in my list: ");
                Console.WriteLine(string.Join(", ",arr) + "\n");
            }

            private void EnsureCapacity()
            {
                T[] temp = new T[arr.Length * 2];
                for (int i = 0; i < arr.Length; i++)
                {
                    temp[i] = arr[i];
                }

                arr = temp;
            }
        }
    }
}
