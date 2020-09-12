using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleUI
{
    class Passage
    {
        // Properties (auto-implemented)
        public string FileName { get; set; }
        public int WordCount { get; set; }
        public double AverageWordLength { get; set; }
        public Dictionary<string, int> WordFrequency { get; set; } = new Dictionary<string, int>(); // word, frequency
        public Dictionary<char, int> StartCharFrequency { get; set; } = new Dictionary<char, int>(); // char, frequency

        // Constructors
        public Passage(string fileName)
        {
            this.FileName = fileName;

            using(StreamReader sr = new StreamReader(FileName))
            {
                string[] words = StripPunctuation(sr.ReadToEnd()).Split(" ", StringSplitOptions.RemoveEmptyEntries);
                this.WordCount = words.Length;
                this.AverageWordLength = words.Average(w => w.Length);

                foreach(string word in words)
                {
                    // Determine frequency of words
                    if(WordFrequency.ContainsKey(word))
                    {
                        WordFrequency[word]++;
                    }
                    else
                    {
                        WordFrequency.Add(word, 1);
                    }

                    // Count starting character of each word
                    if(StartCharFrequency.ContainsKey(word[0]))
                    {
                        StartCharFrequency[word[0]]++;
                    }
                    else
                    {
                        StartCharFrequency.Add(word[0], 1);
                    }
                }
            }
        }

        //// Methods
        // Private Methods
        private string StripPunctuation(string s)
        {
            var sb = new StringBuilder();
            foreach(char c in s)
            {
                if(!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return Regex.Replace(sb.ToString(), @"\t|\n|\r", " ");
        }
        private void PrintWordFrequency()
        {
            int i = 1;
            Console.WriteLine("Frequency of each word:");
            foreach (KeyValuePair<string, int> kvp in this.WordFrequency.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{i} Word: {kvp.Key} Frequency: {kvp.Value}");
                i++;
            }
        }
        private void PrintStartCharFrequency()
        {
            int i = 1;
            Console.WriteLine("Frequency of starting character of each word");
            foreach (KeyValuePair<char, int> kvp in this.StartCharFrequency.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{i} Char: {kvp.Key} Frequency: {kvp.Value}");
                i++;
            }
        }

        // Public Methods
        public void PrintAnalytics()
        {
            Console.WriteLine(string.Join("", Enumerable.Repeat("-", 100)));

            Console.WriteLine($"Words in file: {this.WordCount}");
            Console.WriteLine($"Average legnth of word: {Math.Round(this.AverageWordLength,2)}");

            Console.WriteLine(string.Join("", Enumerable.Repeat("+", 50)));
            PrintWordFrequency();
            Console.WriteLine(string.Join("", Enumerable.Repeat("+", 50)));
            PrintStartCharFrequency();
            Console.WriteLine(string.Join("", Enumerable.Repeat("+", 50)));
        }      
    }
}
