using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureLib
{
    class TrieApplication
    {
        public static void Main()
        {
            Trie trie = new Trie();
            //trie.Insert("Cat");
            //trie.Insert("Catherine");
            //trie.Insert("SherNing");
            //trie.Search("Cat");
            //trie.Search("Catherine");
            //trie.Insert("Sherring");
            //trie.Insert("Son");
            trie.Insert("aBc");
            trie.Insert("abcAb");
            trie.Insert("deF");
            trie.Print();

         
        }

        class Trie
        {
            private TrieNode Root;
            public Trie() 
            {
                Root = new TrieNode();
            }
            public void Print()
            {
                Print(Root, new StringBuilder(), 0);
            }
            public void Print(TrieNode current, StringBuilder sb, int level)
            {
                if (current.IsWord)
                {
                    if (level != sb.Length) sb.Remove(level, sb.Length - level);
                    Console.WriteLine(sb.ToString());
                }

                for (int i = 0; i < current.Children.Length; i++)
                {
                    if (current.Children[i] != null)
                    {
                        sb.Insert(level, GetChar(i));
                        Print(current.Children[i], sb, level + 1); 
                    }
                }
            }
            

            public void Search(string word)
            {
                if (IsFound(word))
                    Console.WriteLine($"{word} is found !");
                else
                    Console.WriteLine($"{word} is not found !");
            }
            public bool IsFound(string word)
            {
                TrieNode current = Root;
                foreach (char character in word.ToLower())
                {
                    int index = GetIndex(character);

                    if (current.Children[index] != null) 
                        current = current.Children[index];
                    else
                        return false;
                }

                return current.IsWord == true ? true : false;
            }

            public void Insert(string word)
            {
                TrieNode current = Root;
                foreach (char character in word.ToLower())
                {
                    int index = GetIndex(character);

                    if (current.Children[index] == null)
                    {
                        TrieNode node = new TrieNode();
                        current.Children[index] = node;
                        current = node;
                    }
                    else
                        current = current.Children[index];
                }

                current.IsWord = true;
            }
        

            // Get the index location.
            private int GetIndex(char character) => character - 'a';

            // Get the character back.
            private char GetChar(int index) => (char)(index + 'a');
        }

        class TrieNode
        {
            // What is a trieNode
            // Represents a single alphabet of the word
            // Mainly use for auto-complete word
            // Root is empty.

            // Implicit character to index mapping.
            // a -> 0, b -> 1

            public TrieNode[] Children { get; set; }
            public bool IsWord { get; set; }
            public TrieNode()
            {
                // 26 alphabet inside english dictionary.
                Children = new TrieNode[26];
                IsWord = false;
            }
        }

    }
}
