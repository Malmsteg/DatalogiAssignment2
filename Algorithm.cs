using System;
using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Algorithm
    {
        /// <summary>
        /// This method removes unnecessary characters.
        /// </summary>
        /// <param name="text"><c>text</c> is the string to remove characters
        /// from.</param>
        /// <returns>The splitted string.</returns>
        public static string[] SplitStrings(string text){
            string[] splittedString = text.Split(
                new string[] {",", ".", " ", ";", ":", "\"", "(", ")", "[", "]",
                "-", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries
                );
            return splittedString;
        }

        /// <summary>
        /// This method sorts an array of strings.
        /// </summary>
        /// <param name="text"><c>text</c> is the array to array to sort.
        /// </param>
        public static void Sort(string[] text){
            for (int i = 0; i < text.Length; i++)
            {
                int lowestPosition = i;
                for (int j = i + 1; j < text.Length; j++)
                {
                    if (string.Compare(text[lowestPosition], text[j]) >= 1){
                        lowestPosition = j;
                    } 
                }

                string tmp = text[i];
                text[i] = text[lowestPosition];
                text[lowestPosition] = tmp;
            }

            foreach (var item in text)
            {
                Console.WriteLine(item);
            }
        }
    }
}