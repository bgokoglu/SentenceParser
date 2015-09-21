using System;
using System.Collections.Generic;

namespace BurakGokoglu_SentenceParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //write a program that parses a sentence and replaces each word with the following: 
            //first letter, number of distinct characters between first and last character, and last letter. 
            //For example, Smooth would become S3h. Words are separated by spaces or non-alphabetic characters and 
            //these separators should be maintained in their original form and location in the answer.
            ParseSentence();
        }

        private static void ParseSentence()
        {
            string input = "Lorem 19ipsum5dolor?sit,amet(,consectetur*adipiscing elit.Donec sit amet&feugiat#nunc, sit amet lacinia purus.";

            string output = "";
            bool isPreviousCharAlpha = true; //flag to keep track whether last processed char alpha vs non-alpha  
            int lastIndex = 0; //last location of char where we find alpha after non-alpha vice versa

            //iterate through input
            for (int index = 0; index < input.Length; index++)
            {
                if (Char.IsLetter(input[index]))
                {
                    //current char is alphabetic
                    //check if previous char is non-alphabetic
                    //if it's then we found a word after seperator, add seperator to output
                    //if not, we have a word, skip until we find non-alphabetic char
                    if (!isPreviousCharAlpha)
                    {
                        output += input.Substring(lastIndex, index - lastIndex);
                        lastIndex = index;
                        isPreviousCharAlpha = true;
                    }
                }
                else
                {
                    //current char is non-alphabetic 
                    //check if previous char is alphabetic
                    //if it's then we found seperator after word, parse the word and add to output
                    //if not, we have multiple char seperator, skip until we find alphabetic char
                    if (isPreviousCharAlpha)
                    {
                        output += ParseWord(input.Substring(lastIndex, index - lastIndex));
                        lastIndex = index;
                        isPreviousCharAlpha = false;
                    }
                }
            }

            //last word or seperator in sentence
            if (isPreviousCharAlpha)
                output += ParseWord(input.Substring(lastIndex, input.Length - lastIndex));
            else
                output += input.Substring(lastIndex, input.Length - lastIndex);

            Console.WriteLine("input : {0}", input);
            Console.WriteLine("output: {0}", output);
            Console.ReadLine();
        }

        private static string ParseWord(string word)
        {
            if (word.Length <= 1) //if word is only one letter return itself
                return word;

            var firstLetter = word.Substring(0, 1);
            var lastLetter = word.Substring(word.Length - 1, 1);

            //trim first letter and last letter from string
            word = word.TrimStart(firstLetter.ToCharArray()).TrimEnd(lastLetter.ToCharArray());

            //get number of distinct characters 
            var numOfDistinctChars = GetNumOfDistinctCharsInString(word);

            return string.Format("{0}{1}{2}", firstLetter, numOfDistinctChars, lastLetter);
        }

        private static int GetNumOfDistinctCharsInString(string word)
        {
            var hash = new HashSet<char>();

            // hashset add will only add it if it doesn't exist already
            foreach (char c in word)
                hash.Add(char.ToLower(c)); //convert char to lowercase to prevent adding letter in both uppercase and lowercase

            return hash.Count;
        }
    }
}
