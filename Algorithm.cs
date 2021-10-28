using System;
using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Algorithm
    {
        /// <summary>
        /// This method sorts an array of strings.
        /// </summary>
        /// <param name="text"><c>text</c> is the array to sort.</param>
        public static void Sort(string[] text)
        {
            Sort(text, text.Length);
        }

        private static void Sort(string[] text, int length)
        {
            for (int i = 0; i < length - 1; i++)
            {
                if (string.Compare(text[i], text[i + 1]) >= 1)
                {
                    string tmp = text[i];
                    text[i] = text[i + 1];
                    text[i + 1] = tmp;           
                }
            }
            if (length - 1 > 1) Sort(text, length - 1);
        }

        /// <summary>
        /// Searches for a word in the loaded documents. Returns the number of matches in the documents, and in descending order.
        /// Variabler för tidskomplexitet är: n1 (antalet dokument), n2 (summan av innehållet av alla dokument).
        /// Tidskomplexiteten är O(2n1 + n2), vilket kan förkortas till O(n)
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <returns>A list of files and matches in descending order</returns>
        public static List<(string filename, int count)> Search(string searchString, List<(string filename, string text)> Texts)
        {
            List<(string filename, int count)> result = new();

            foreach (var item in Texts)
            {
                int count = 0;
                string name = item.filename.Substring(item.filename.LastIndexOf("\\") + 1);
                string[] arr = Logic.SplitStrings(item.text);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].ToLower().Equals(searchString.ToLower()))
                    {
                        count++;
                    }
                }
                result.Add((name, count));
            }
            result.Sort((x, y) => y.count.CompareTo(x.count));
            return result;
        }
    }
}