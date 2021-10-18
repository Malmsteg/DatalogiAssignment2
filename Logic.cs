using System.Collections.Generic;
using System;
using System.IO;

public static class Logic
{
    public static void Start()
    {
        List<(string filename, string text)> test = ReadDocuments();
        Console.WriteLine(test[0].text);
    }

    /// <summary>
    /// Reads amount number of .txt-files. Returns a List tuple of filenames and file contents.
    /// </summary>
    /// <param name="amount">The number of files the method should read</param>
    /// <returns>A tuple of filenames and their contents</returns>
    public static List<(string filnames, string texts)> ReadDocuments(int amount = 1)
    {
        List<(string filenames, string texts)> result = new();
        int i = 0;
        while (i < amount)
        {
            Console.WriteLine("Please provide an exact filepath for .txt file you want to read into the program:\n\n");
            string path = Console.ReadLine();
            if (String.IsNullOrEmpty(path) || path.Length < 4 || !path.Substring(path.Length - 4).Equals(".txt") || !File.Exists(path) || new FileInfo(path).Length == 0)
            {
                Console.WriteLine("Please enter a valid, non-empty text file.");
            }
            else
            {
                result.Add((path, System.IO.File.ReadAllText(path)));
                i++;
                Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1) + " has been added to the list.");
            }
            Console.WriteLine("\n\nPress enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        return result;
    }
}