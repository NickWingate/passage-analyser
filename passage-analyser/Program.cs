using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileLocation = GetFileLocation();
            Passage pas = new Passage(fileLocation);
            Console.WriteLine($"Analysing {GetFileName(fileLocation)}");
            pas.PrintAnalytics();
        }

        /// <summary>
        /// Gets file location from user 
        /// </summary>
        /// <returns></returns>
        static string GetFileLocation()
        {
            string fileName;
            Console.Write("File location(e.g. ~/Documents/passage.txt): ");
            fileName = Console.ReadLine();
            return fileName;
        }

        /// <summary>
        /// Gets filename from full file location path
        /// </summary>
        /// <param name="location">
        /// location of file
        /// </param>
        /// <returns></returns>
        static string GetFileName(string location)
        {
            string[] splitFileLocation = location.Split(@"\");
            return splitFileLocation[splitFileLocation.Length - 1];
        }
        
    }
}
