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
            // foreach (var item in Texts)
            // {
            //     Console.WriteLine(item.filename);
            //     Console.WriteLine(item.text);
            // }
            // Console.ReadLine();

            List<string> menuOptions = new()
            {
                "Läs in ett txt-dokument i programmet",
                "Sök efter ett antal förekomster av ord i texter.",
                "Sortera orden i dokumenten i bokstavsordning och skriv ut de" +
                " X antal första orden.",
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
                        Console.ReadLine();
                        break;
                    case 2:
                        if (Texts.Count > 0)
                        {
                            SearchAndPrint();
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Please input at least one .txt-file before using this function.");
                        }
                        break;
                    case 3:
                        if (Texts.Count > 0)
                        {
                            int choice = DocumentMenu();
                            string document = Texts[choice - 1].text;
                            string[] text = SplitStrings(document);
                            Algorithm.Sort(text);
                            Console.Write("How many words should be printed?\n> ");
                            int numOfWords = InputInt();
                            for (int i = 0; i < numOfWords && i < text.Length; i++)
                            {
                                Console.WriteLine(text[i]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please input at least one .txt-file before using this function.");
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        exit = true;
                        Console.WriteLine("Thank you for using this program! :-)");
                        Console.ReadLine();
                        break;
                }
            }
            // Avkommentera koden nedan för att köra metoden ReadDocument 3 gånger, och sedan lista i ordning efter söktermen "och"
            // test();
            // Console.ReadLine();

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



        public static void SearchAndPrint()
        {
            string searchWord = Input();
            var temp = Algorithm.Search(searchWord, Texts);
            foreach (var item in temp)
            {
                Console.WriteLine(item);
                searchResult.Add(item);
            }
        }

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

        private static int DocumentMenu(){
            List<string> documents = new();

            Console.Clear();
            
            foreach (var item in Texts)
            {
                documents.Add(item.filename);
            }
            
            documents.Add("Back to main menu");

            Menu documentMenu = new(documents);
            documentMenu.CreateMenu();
            if (documentMenu.Choice == documents.Count) Start();

            return documentMenu.Choice;
        }

        /// <summary>
        /// Testfunktion
        /// </summary>
        private static void test()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                Texts.Add(ReadDocument());
            }
            var list = Algorithm.Search("till", Texts);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}