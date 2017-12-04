using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    static class Format
    {
        public static string[] Tweets(string input)
        {

            List<string> content = input.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < content.Count(); i++)
            {
                if (content[i].Length > 51)
                {
                    content[i] = content[i].Substring(24, content[i].Length - 30);
                    string regex = "(\\[.*\\])";
                    content[i] = Regex.Replace(content[i], regex, "");
                }
            }

            return content.ToArray();
        }
    }
}
