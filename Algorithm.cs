using System;
using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Algorithm
    {    
        public static string[] SplitStrings(string text){
            string[] splittedString = text.Split(
                new string[] {",", ".", " ", ";", ":", "\"", "(", ")", "[", "]",
                "-", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries
                );
            return splittedString;
        }    
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
                System.Console.WriteLine(item);
            }
        }
    }
}