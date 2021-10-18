using System.Collections.Generic;
using System;
using System.IO;
namespace Assignment2
{
    public static class Logic
    {
        static List<(string filename, string text)> Texts = new();
        public static void Start()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            for (int i = 1; i < arguments.Length; i++)
            {
                if (!String.IsNullOrEmpty(arguments[i]))
                {
                    Texts.Add(ReadDocument(arguments[i]));
                }
            }
            foreach (var item in Texts)
            {
                Console.WriteLine(item.filename);
                Console.WriteLine(item.text);
            }
            Console.ReadLine();

        }

        /// <summary>
        /// Reads a .txt-file. Returns a tuple of the filename and file content.
        /// </summary>
        /// <returns>A tuple of filename and the file's content</returns>
        public static (string filname, string text) ReadDocument()
        {
            (string filename, string text) result = new();
            bool ok = false;
            while (!ok)
            {
                Console.WriteLine("Please provide an exact filepath for .txt file you want to read into the program:\n\n");
                string path = Console.ReadLine();
                if (String.IsNullOrEmpty(path) || path.Length < 4 || !path.Substring(path.Length - 4).Equals(".txt") || !File.Exists(path) || new FileInfo(path).Length == 0)
                {
                    Console.WriteLine("Please enter a valid, non-empty text file.");
                }
                else
                {
                    result = (path, System.IO.File.ReadAllText(path));
                    ok = true;
                    Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1) + " has been added to the list.");
                }
                Console.WriteLine("\n\nPress enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
            return result;
        }

        /// <summary>
        /// Reads a .txt-file. Returns a tuple of the filename and file content.
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>A tuple of filename and the file's content</returns>
        public static (string filname, string text) ReadDocument(string path)
        {
            (string filename, string text) result = new();
            if (String.IsNullOrEmpty(path) || path.Length < 4 || !path.Substring(path.Length - 4).Equals(".txt") || !File.Exists(path) || new FileInfo(path).Length == 0)
            {
                Console.WriteLine($"File {path} was not a valid .txt file.");
            }
            else
            {
                result = (path, System.IO.File.ReadAllText(path));
                Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1) + " has been added to the list.");
            }
            Console.WriteLine("\n\nPress enter to continue");
            Console.ReadLine();
            Console.Clear();
            return result;
        }
    }
}