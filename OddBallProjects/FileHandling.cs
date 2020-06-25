using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OddBallProjects
{
    class FileHandling
    {
        // Started Hello World 1/3/2018 - 2 years ago.
        // This is how far I have progressed.
        // You cannot call a non-static method from a static.
        // Because, to call a instance method, you need to create an instance first.
        // Before calling those methods.

        public static void Main()
        {
            Console.WriteLine("\nFile Handling in .NET");

            // File handling and directories.
            // Familiarize with IO namespace.
            // Can use Directory Create to create folders.
            // To learn a new class, you will need to practice
            // and apply what you learn.
            // learn useful c# shortcuts.
            // https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=netframework-4.8

            //string folderPath = @"C:\TestFolderA";
            string folderPath2 = @"C:\temp";

            //CreateNewFile(folderPath2);
            //StreamWriteTest();
            //CreateMultipleFileDirectories(folderPath2, 65, Directories.TopLevelOnly);
            //SeeFoldersInDirectory(folderPath2);
            //DeleteMultipleFileDirectories(folderPath2, "f*",SearchOption.AllDirectories);
            StringSplitMethod();
            RegexSplit();
        }

        #region Others
        private static void StringSplitMethod()
        {
            string sample = " (    BarHigh 10 )";
            char[] separator = { '(', ' ', ')' };
            string[] strSeparator = { "(", " ", ")" };
            string[] splitArr = sample.Split(strSeparator,StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine(splitArr[1]);

            //int barHigh = Convert.ToInt32(splitArr[1]);
            //Console.WriteLine(barHigh);
            foreach (var str in splitArr)
            {
                Console.WriteLine(str);
            }
        }
        private static void RegexSplit()
        {
            string value = "cat\r\ndog\r\nanimal\r\nperson";
            Console.WriteLine(value);
            // Split the string on line breaks.
            // ... The return value from Split is a string array.
            string[] lines = Regex.Split(value, "\r\n");

            foreach (string line in lines)
            {
                Console.Write(line + " ");
            }
            Console.WriteLine();

            const string sentence = "Hello, my friend";
            // Split on all non-word characters.
            // ... This returns an array of all the words.
            string[] words = Regex.Split(sentence, @"\W+");
            foreach (string word in words)
            {
                Console.WriteLine("WORD: " + word);
            }
        }
        #endregion

        #region Stream Writer / Reader
        private static void StreamWriteTest()
        {
            DirectoryInfo[] cDirs = new DirectoryInfo(@"C:\").GetDirectories();

            // using keyword is a c# construct that will call IDispose.
            // To get rid of unmanaged resources.
            using (StreamWriter sw = new StreamWriter(@"C:\temp\CDriveDirs.txt"))
            {
                foreach (DirectoryInfo dir in cDirs)
                {
                    sw.WriteLine(dir.Name);
                }
            }

            string line = "";
            using (StreamReader sr = new StreamReader("CDriveDirs.txt"))
            {
                // exit loop when there is nothing left to read.
                while ((line = sr.ReadLine()) != null)
                {
                    // can use condition as local variable.
                    Console.WriteLine(line);
                }
            }
        }
        #endregion

        #region Create Directories / Files

        private static void CreateNewFile(string path)
        {
            // Create a tuple array
            (int index, string name)[] customer =
            {
                (1, "Johnny"),
                (2, "Jane"),
                (3, "Jenny"),
                (4, "Ang Jin Yuan")
            };
            
            CreateSingleFileDirectory(path + @"\FolderA");

            // Becareful: this will override an existing file.
            File.WriteAllText(path + @"\FolderA\Customers.txt", string.Join("\n", customer));

            // Read file / Split string.
            char[] myChar = { '(', ')' };
            string[] customers = File.ReadAllLines(path + @"\FolderA\Customers.txt");
            foreach (string cust in customers)
            {
                string[] newCust = cust.Split('(', ')');
                foreach (var item in newCust)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private enum Directories
        {
            TopLevelOnly, TopAndInternal
        }
        private static void CreateSingleFileDirectory(string path = @"C:\")
        {
            // Another approach to creating a directory object.
            if (!Directory.Exists(path))
            {
                // Passing the path to dirInfo object will not create object.
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                // Create method is required to create directory.
                directoryInfo.Create();
            }
        }

        private static void CreateMultipleFileDirectories(string path, int count = 65, 
            Directories levels = Directories.TopLevelOnly)
        {
            // Last folder is FolderG
            if (count == 72) return;

            if (levels == Directories.TopLevelOnly)
            {
                // All folders created are on the same level.
                // If file exist in directory, method will do nothing by default
                Directory.CreateDirectory(path + @"\Folder" + (char)count++);
                CreateMultipleFileDirectories(path, count, levels);
            }
            else
            {
                // Arguments overrides default parameter values.
                string newPath = path + @"\Folder" + (char)count++;

                // Directory is a utility class to create DirectoryInfo objects.
                Directory.CreateDirectory(newPath);
                CreateMultipleFileDirectories(newPath, count, levels);
            }
        }

        #endregion

        #region Delete Directories / Files
        private static void DeleteFileDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, false);
            }
        }

        private static void DeleteMultipleFileDirectories(string path, string extension, 
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            // If top director is selected, but there are internal folders, it will throw an exception.
            string[] getDirectories = Directory.GetDirectories(path, extension, searchOption);
            foreach (var dir in getDirectories)
            {
                if (Directory.Exists(dir))
                {
                    Console.WriteLine(dir + "-> Deleted");

                    // Do not delete internal folders.
                    if(searchOption == SearchOption.TopDirectoryOnly)
                        Directory.Delete(dir, false);

                    if (searchOption == SearchOption.AllDirectories)
                        Directory.Delete(dir, true);
                }
            }
        }
        #endregion

        #region View Directories / Files
        /*
         Characters other than the wildcard are literal characters. 
         For example, the searchPattern string "*t" searches for all names in path ending with the letter "t". 
         The searchPattern string "s*" searches for all names in path beginning with the letter "s".
        */
        private static void SeeFilesInDirectory(string path = @"C:\",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            // Change search option to see inside sub directory.
            var files = Directory.GetFiles(path, "*", searchOption);

            foreach (var file in files)
            {
                Console.WriteLine(file);

                // Only file name without the full path.
                // Check static file Path class for more information.
                Console.WriteLine(Path.GetFileName(file));

                // File info is an instance class.
                var info = new FileInfo(file);
                Console.WriteLine($"{Path.GetFileName(file)}: {info.Length / 1000} kb");
            }
        }

        private static void SeeFoldersInDirectory(string path = @"C:\", string extensions = "*",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            // does not search sub directories.
            // directories are folders
            string[] dirs = Directory.GetDirectories(path, extensions, searchOption);

            foreach (var dir in dirs)
            {
                Console.WriteLine(dir);
            }
        }
        #endregion
    }
}
