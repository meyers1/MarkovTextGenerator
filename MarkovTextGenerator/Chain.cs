using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    class Chain
    {
        public Dictionary<String, List<Word>> words;
        private Random rand;

        public Chain ()
        {
            words = new Dictionary<String, List<Word>>();
            rand = new Random();
        }

        // This may not be the best approach.. better may be to actually store
        // a separate list of actual sentence starting words and randomly choose from that
        public String GetRandomStartingWord()
        {
            return words.Keys.ElementAt(rand.Next() % words.Keys.Count);
        }

        // Adds a sentence to the chain
        // You can use the empty string to indicate the sentence will end
        //
        // For example, if sentence is "The house is on fire" you would do the following:
        //  AddPair("The", "house")
        //  AddPair("house", "is")
        //  AddPair("is", "on")
        //  AddPair("on", "fire")
        //  AddPair("fire", "")

        public void AddString(String sentence)
        {
            string[] words = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string word1 = "", word2 = "";
            for (int i = 0; i < words.Length - 1; i++)
            {
                word1 = words[i];
                word2 = words[i + 1];
                //if (words[i + 1] == null)
                //{
                //    word2 = "";
                //}
                //else
                //{
                //    word2 = words[i + 1];
                //}
                AddPair(word1, word2);
            }
            word1 = word2;
            word2 = "";
            // TODO: Break sentence up into word pairs
            // TODO: Add each word pair to the chain
        }

        // Adds a pair of words to the chain that will appear in order
        public void AddPair(String word, String word2)
        {
            if (!words.ContainsKey(word))
            {
                words.Add(word, new List<Word>());
                words[word].Add(new Word(word2));
            }
            else
            {
                bool inDictionary = false;

                foreach (Word s in words[word])
                {
                    if (s.ToString() == word2)
                    {
                        s.Count++;
                        inDictionary = true;
                    }
                    if (!inDictionary)
                    {
                        words[word].Add(new Word(word2));
                    }
                }
            }
        }

        // Given a word, randomly chooses the next word
        public String GetNextWord(String word)
        {
            if (words.ContainsKey(word))
            {
                double choice = rand.NextDouble();
                double num = 0;
                foreach (Word s in words[word])
                {
                    num += s.Probability;
                    if (choice < num)
                    {
                        return s.ToString();
                    }
                }
            }
            return "";  
        }

        public void UpdateProbabilities()
        {
            foreach (String word in words.Keys)
            {
                double sum = 0;  // Total sum of all the occurences of each followup word

                // Step 1:  Get the sum of all occurences of each followup word
                foreach (Word s in words[word])
                {
                    sum += s.Count;
                }

                // Step 2:  Update the probabilities
                foreach (Word s in words[word])
                {
                    s.Probability = s.Count / sum;
                }
            }
        }
    }
}
