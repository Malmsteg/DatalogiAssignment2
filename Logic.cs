using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace DatalogiAssignment2
{
    public static class Logic
    {
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
                "Presentera de sökningar du hittills har gjort.",
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
                        break;
                    case 2:
                        if (Texts.Count > 0)
                        {
                            SearchAndPrint();
                        }
                        else
                        {
                            Console.WriteLine("Mata in textfil i programmet " + 
                                "innan du använder den här funktionen.");
                        }
                        break;
                    case 3:
                        GetSearchResults();
                        break;
                    case 4:
                        if (Texts.Count > 0)
                        {
                            int choice = DocumentMenu();
                            string document = Texts[choice - 1].text;
                            string[] text = SplitStrings(document);
                            Algorithm.Sort(text);
                            Console.Write(
                                "Hur många ord skall skrivas ut?\n> ");
                            int numOfWords = InputInt();
                            for (int i = 0; i < numOfWords && i < text.Length; 
                                i++)
                            {
                                Console.WriteLine(text[i]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Mata in textfil i programmet " + 
                                "innan du använder den här funktionen.");
                        }
                        Console.ReadLine();
                        break;
                    case 5:
                        exit = true;
                        Console.WriteLine("Tack för att du använde det här " + 
                            "programmet! :-D");
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
                Console.WriteLine("Vänligen ange en exakt sökväg för en " + 
                    ".txt-fil som du vill läsa in till programmet:\n\n");
                string path = Console.ReadLine();
                if (String.IsNullOrEmpty(path) || path.Length < 5 || 
                    !path.Substring(path.Length - 4).Equals(".txt") || 
                    !File.Exists(path) || new FileInfo(path).Length == 0)
                {
                    Console.WriteLine("Vänligen ange en giltig, icke-tom " + 
                        "textfil..");
                }
                else
                {
                    result = (path, File.ReadAllText(path));
                    ok = true;
                    Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1)
                         + " har lagts till listan.");
                }
                Console.WriteLine("\n\nTryck enter för att fortsätta.");
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
            if (String.IsNullOrEmpty(path) || path.Length < 4 || 
                !path.Substring(path.Length - 4).Equals(".txt") || 
                !File.Exists(path) || new FileInfo(path).Length == 0)
            {
                Console.WriteLine($"Filen {path} var inte en giltig .txt-fil.");
            }
            else
            {
                result = (path, File.ReadAllText(path));
                Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1) + 
                    " har lagts till.");
            }
            Console.WriteLine("\n\nTryck enter för att fortsätta.");
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
                Console.WriteLine("Skriv in ett sökord.");
                result = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(
                result));

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
                    Console.WriteLine($"Du måste ange ett nummer större än 0 " +
                        "och mindre än {int.MaxValue}");
                }
                else
                {
                    ok = true;
                }
            } while (!ok);
            return result;
        }

        /// <summary>
        /// Prints a menu of all documents.
        /// </summary>
        /// <returns>The user's choice.</returns>
        private static int DocumentMenu()
        {
            List<string> documents = new();

            Console.Clear();

            foreach (var item in Texts)
            {
                documents.Add(item.filename);
            }

            documents.Add("Åter till huvudmenyn");

            Menu documentMenu = new(documents);
            documentMenu.CreateMenu();
            if (documentMenu.Choice == documents.Count) Start();

            return documentMenu.Choice;
        }

        /// <summary>
        /// Lets the user search for the occurence of a word in the texts, and
        /// prints to screen the result
        /// </summary>
        public static void SearchAndPrint()
        {
            string searchWord = Input();
            List<(string str, int count)> searchResult = Algorithm.Search(
                searchWord, Texts);
            foreach (var item in searchResult)
            {
                Console.WriteLine(item);
                Logic.searchResult.Add(item);
            }

            if (tree.Add(searchWord.ToLower(), searchResult))
            {
                Console.WriteLine("\n\nSökresultatet lades till sökträdet!");
            }
            else
            {
                Console.WriteLine("\n\nSökresultatet lades inte till " +
                    "sökträdet.\nFörmodligen för att du har sökt efter samma " +
                    " sak förut.");
            }
            Console.WriteLine("\n\nTryck enter för att fortsätta.");
            Console.ReadLine();
        }
        /// <summary>
        /// Presents the searches that a user has made during run-time.
        /// </summary>
        public static void GetSearchResults()
        {
            if (tree.Root is null)
            {
                Console.WriteLine("Du behöver utföra några sökningar innan du" +
                    " använder den här funktionen.\n\n");
                Console.WriteLine("\n\nTryck enter för att fortsätta.");
                Console.ReadLine();
                return;
            }

            var result = tree.GetNodes(tree.Root);
            Console.WriteLine("Presenterar sökresultat:");
            foreach (var item in result)
            {
                Console.WriteLine(item.SearchWord);
                foreach (var item2 in item.SearchResult)
                {
                    Console.WriteLine(item2.Filename + " med " + item2.count + 
                        " sökträffar.");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\nTryck enter för att fortsätta.");
            Console.ReadLine();
        }
    }
}