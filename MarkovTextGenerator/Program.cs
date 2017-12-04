using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Chain chain = new Chain();

            //Console.WriteLine("Welcome to Marky Markov's Random Text Generator!");

            //Console.WriteLine("Enter some text I can learn from (enter single ! to finish): ");

            //while (true)
            //{

            //    Console.Write("> ");

            //    String line = Console.ReadLine();
            //    if (line == "!")
            //        break;

            //    chain.AddString(line);  // Let the chain process this string
            //}

            string input = File.ReadAllText(@"C:\Users\eahscs\Documents\GitHub\MarkovTextGenerator\Test.txt");
            //string input = File.ReadAllText(@"C:\Users\eahscs\Documents\GitHub\MarkovTextGenerator\TrumpTweets.txt");
            //string input = File.ReadAllText(@"C:\Users\SUNNY\Documents\GitHub\MarkovTextGenerator\TrumpTweets.txt");

            string[] FormattedInput = Format.Tweets(input);

            foreach (string tweet in FormattedInput)
            {
                chain.AddString(tweet);
            }

            // Now let's update all the probabilities with the new data
            chain.UpdateProbabilities();

            // Okay now for the fun part
            //Console.WriteLine("Done learning!  Now give me a word and I'll tell you what comes next.");
            //Console.Write("> ");
            //String word = Console.ReadLine();

            string word = chain.GetRandomStartingWord();

            String nextWord = chain.GetNextWord(word);

            Console.Write(word);

            while (nextWord != "")
            {
                Console.Write(" " + nextWord);
                nextWord = chain.GetNextWord(nextWord);
            }
        }
    }
}
