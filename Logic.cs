using System.Collections.Generic;
using System;
using System.IO;
namespace DatalogiAssignment2
{
    public static class Logic
    {
        //TODO: Save search results in a non-linear or abstract data structure
        //TODO: Test switch statements, that they work properly
        //TODO: When above is resolved, remove method test in this file
        //TODO: 
        static List<(string filename, string text)> Texts = new();
        static List<(string filname, int count)> searchResult = new();

        static Tree tree = new Tree();
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

            List<string> menuOptions = new()
            {
                "Läs in ett txt-dokument i programmet",
                "Sök efter ett antal förekomster av ord i texter.",
                "Sortera orden i dokumenten i bokstavsordning.",
                "Skriv ut de X antal första orden",
                "Skriv ut alla sökningar du gjort hittills",
                "Avsluta programmet"
            };
            Menu menu = new Menu(menuOptions);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                menu.CreateMenu();
                switch (menu.Choice)
                {
                    case 1:
                        Texts.Add(ReadDocument());
                        break;
                    case 2:
                        if (Texts.Count > 0)
                        {
                            SearchAndPrint();
                        }
                        else
                        {
                            Console.WriteLine("Please input at least one .txt-file before using this function.");
                        }
                        break;
                    case 3:
                        if (Texts.Count > 0)
                        {
                            foreach (var item in Texts)
                            {
                                Algorithm.Sort(SplitStrings(item.text));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please input at least one .txt-file before using this function.");
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        if (Texts.Count > 0)
                        {
                            int count = InputInt();
                            foreach (var item in Texts)
                            {
                                Console.Clear();
                                Console.WriteLine("The first " + count + " words in " + item.filename.Substring(item.filename.LastIndexOf("\\") + 1) + "\n");
                                var temp = item.text.Split(" ");
                                for (int i = 0; i < count && i < temp.Length; i++)
                                {
                                    Console.Write(temp[i] + " ");
                                }
                                Console.WriteLine("\n\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please input at least one .txt-file before using this function.");
                        }
                        Console.ReadLine();
                        break;
                    case 5:
                        GetSearchResults();
                        break;
                    case 6:
                        exit = true;
                        Console.WriteLine("Thank you for using this program! :-)");
                        Console.ReadLine();
                        break;
                }
            }
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
                if (String.IsNullOrEmpty(path) || path.Length < 5 || !path.Substring(path.Length - 4).Equals(".txt") || !File.Exists(path) || new FileInfo(path).Length == 0)
                {
                    Console.WriteLine("Please enter a valid, non-empty text file.");
                }
                else
                {
                    result = (path, File.ReadAllText(path));
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
                result = (path, File.ReadAllText(path));
                Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1) + " has been added to the list.");
            }
            Console.WriteLine("\n\nPress enter to continue");
            Console.ReadLine();
            Console.Clear();
            return result;
        }

        /// <summary>
        /// This method removes unnecessary characters.
        /// </summary>
        /// <param name="text"><c>text</c> is the string to remove characters
        /// from.</param>
        /// <returns>The splitted string.</returns>
        public static string[] SplitStrings(string text)
        {
            string[] splittedString = text.Split(
                new string[] {",", ".", " ", ";", ":", "\"", "(", ")", "[", "]",
                "-", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries
                );
            return splittedString;
        }


        /// <summary>
        /// Prompts user for a single word input.
        /// </summary>
        /// <returns>A string consisting of a single word.</returns>
        public static string Input()
        {
            string result;

            do
            {
                Console.WriteLine("Please enter a single word");
                result = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result));

            return result.Split(" ")[0];
        }

        /// <summary>
        /// Prompts user to input an integer
        /// </summary>
        /// <returns>An integer greater than 0</returns>
        public static int InputInt()
        {
            int result;
            bool ok = false;
            do
            {
                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out result) || result < 1)
                {
                    Console.WriteLine("Invalid input. Enter a number greater than 0");
                }
                else
                {
                    ok = true;
                }
            } while (!ok);
            return result;
        }

        /// <summary>
        /// Lets the user search for the occurence of a word in the texts, and prints to screen the result
        /// </summary>
        public static void SearchAndPrint()
        {
            string searchWord = Input();
            List<(string str, int count)> searchResult = Algorithm.Search(searchWord, Texts);
            foreach (var item in searchResult)
            {
                Console.WriteLine(item);
                Logic.searchResult.Add(item);
            }

            if (tree.Add(searchWord, searchResult))
            {
                Console.WriteLine("\n\nThis search result was added to the search result tree");
            }
            else
            {
                Console.WriteLine("\n\nThis search result was not added to the search result tree.\n" +
                "Probably you searched it before.");
            }
            Console.WriteLine("\n\nPlease press enter to continue");
            Console.ReadLine();
        }
        /// <summary>
        /// Presents the searches that a user has made during run-time.
        /// </summary>
        public static void GetSearchResults()
        {
            if (tree.Root is null)
            {
                Console.WriteLine("You need to make some searches before using this option.\n\n");
                Console.WriteLine("\n\nPlease press enter to continue");
                Console.ReadLine();
                return;
            }

            var result = tree.GetNodes(tree.Root);
            Console.WriteLine("Presenting search results:");
            foreach (var item in result)
            {
                Console.WriteLine(item.SearchWord);
                foreach (var item2 in item.SearchResult)
                {
                    Console.WriteLine(item2.Filename + " counted " + item2.count + " times.");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\nPlease press enter to continue");
            Console.ReadLine();
        }
    }
}