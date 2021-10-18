using System.Collections.Generic;
using System;
using System.IO;

public static class Logic
{
    public static void Start()
    {
        var test = ReadDocuments();
        List<(string filename, string text)> texts = new();
        texts.Add(test);
        Console.WriteLine(texts[0].filename);
        Console.WriteLine(texts[0].text);
    }

    /// <summary>
    /// Reads amount number of .txt-files. Returns a tuple of filenames and file contents.
    /// </summary>
    /// <returns>A tuple of filenames and their contents</returns>
    public static (string filname, string text) ReadDocuments()
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
}