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

            string[] input = File.ReadAllLines("TrumpTweets.txt");

            string[] FormattedInput = Format.Tweets(input);

            foreach (string tweet in FormattedInput)
            {
                chain.AddString(tweet);
            }

            //update all the probabilities with the new data
            chain.UpdateProbabilities();

            while(true)
            {
                string word = chain.GetRandomStartingWord();

                String nextWord = chain.GetNextWord(word);

                Console.Write(word);

                while (nextWord != "")
                {
                    Console.Write(" " + nextWord);
                    nextWord = chain.GetNextWord(nextWord);
                }
                Console.ReadLine();
                Console.WriteLine();
            }
        }
    }
}