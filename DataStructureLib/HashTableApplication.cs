using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class HashTableApplication
    {
        public static void Main()
        {
            // Convert to ASCII / unicode
            HashTable hash = new HashTable();
            hash.Set("a", "Apple");
            hash.Set("b", "bAnAna");
            hash.Set("a", "apple");
            hash.Set("a", "ApRicOt");
            hash.Set("c", "cArRot");
            hash.Print();
            Console.WriteLine(hash.Get("c"));
        }

        // Key/Value pairs container - Dictionary<>
        class HashTable
        {
            // Hash functions
            // Convert keys into valid array indices.
            // Fast - Constant time
            // Does not cluster outputs at specific indices
            // Same input to give same output.
            // Help with collision
            // separate chaining
            // Linear probing

            public static HashTable NewHashTable = new HashTable();
            public int Size { get; private set; }

            // Declare a jagged multi dimension array for separate chaining.
            // To store Both Key and Value.
            private readonly string[][,] KeyMap;
            public HashTable(int size = 53) 
            {
                Size = size;
                KeyMap = new string[Size][,];
            }

            // Print all the key value pairs inside table
            public void Print()
            {
                for (int i = 0; i < KeyMap.Length; i++)
                {
                    // Check for empty array.
                    if (KeyMap[i] != null)
                    {
                        for (int j = 0; j < KeyMap[i].GetLength(0); j++)
                        {
                            // There are only two columns, no need to use Rank.
                            for (int k = 0; k < 2; k++)
                            {
                                Console.WriteLine($"KeyMap[{i}][{j},{k}]: {KeyMap[i][j, k]} ");
                            }
                        }

                        Console.WriteLine();
                    }
                }
            }

            // Get function
            public string Get(string key)
            {
                // Retrieve the index
                int index = HashFunction(key);

                // There is no element 
                if (KeyMap[index] == null)
                {
                    Console.WriteLine("There is no entry for this key: " + key);
                    return "";
                }
                else
                {
                    for (int i = 0; i < KeyMap[index].GetLength(0); i++)
                    {
                        // Check for key
                        if (KeyMap[index][i,0] == key)
                        {
                            Console.WriteLine("Key found: " + key);

                            // This is the value store inside key.
                            return KeyMap[index][i, 1];
                        }
                    }
                }

                Console.WriteLine("There is no value for this key: " + key);
                return "";
            }

            // Set function - using separate chaining.
            public int Set(string key, string value)
            {
                // get index from hash function
                int index = HashFunction(key);

                // Test
                //int index = idx;
                
                // Check if the array slot has been taken
                // if not initialize the jagged array and put it in there.
                if (KeyMap[index] == null)
                {
                    // Create a multi-dimensional array. {key, value}
                    // [1,2] 1: number of rows, 2: number of columns.
                    KeyMap[index] = new string[1, 2]
                    {
                        { key.ToLower(), value.ToLower() }
                    };
                }
                else
                {
                    // transverse multi dimension array
                    for (int i = 0; i < KeyMap[index].GetLength(0); i++)
                    {
                        // Check if value is inside KeyMap
                        if (KeyMap[index][i, 1].Contains(value))
                        {
                            Console.WriteLine($"This \"{value}\" value is already inside the table.");
                            return index; 
                        }
                    }

                    // else, ensure sufficient capacity
                    int newRow = KeyMap[index].GetLength(0) + 1;

                    // initialize temp column, there will always be only two columns
                    // Change the number of rows.
                    string[,] temp = new string[newRow, 2];

                    // Copy everything in KeyMap[index] into temp.
                    for (int i = 0; i < newRow - 1; i++)
                    {
                        // Copy Key
                        temp[i, 0] = KeyMap[index][i, 0];

                        // Copy Value
                        temp[i, 1] = KeyMap[index][i, 1];
                    }

                    // insert value into last element of temp
                    // newLen convert to zero-index
                    temp[newRow - 1, 0] = key.ToLower();
                    temp[newRow - 1, 1] = value.ToLower();

                    // Set 
                    KeyMap[index] = temp;
                }

                return index;
            }
          
            // Basic Hash Function - can be used for cytography.
            public int HashFunction(string key)
            {
                int total = 0;

                // Helps prevent collisions.
                int prime = 31;

                if (key.Length == 0) return 0;

                // Maximum 100 characters.
                for (int i = 0; i < Math.Min(key.Length,100); i++)
                {
                    // Need to convert to lower case as unicode lower case begins at 97.
                    int value = Char.ConvertToUtf32(key.ToLower(), i) - 96;

                    // Compute each character and add to total
                    total = (total * prime + value) % KeyMap.Length;
                }

                return total;
            }

           
        }
    }
}
