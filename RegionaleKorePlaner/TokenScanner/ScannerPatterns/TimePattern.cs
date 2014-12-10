using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegionaleKorePlaner.TokenScanner.ScannerPatterns
{
    public class TimePattern : IScannerPattern
    {
        public string TriggerChar { get; set; }

        public TimePattern()
        {
            TriggerChar = "[0-2]";
        }

        public bool CheckPattern(Queue<char> input, List<Token> tokens, List<string> errorList)
        {
            if (!isTriggerChar(input.Peek()))
                return false;

            string time = input.Dequeue().ToString();
            string pattern;

            if (int.Parse(time) == 2)
            {
                pattern = "[0-3]";
            }
            else
            {
                pattern = "[0-9]";
            }

            if (!Regex.IsMatch(input.Peek().ToString(), pattern))
            {
                errorList.Add("forventer tal inden for " + pattern + " tallet " + input.Peek() + " blev læst  I forbindelse med " + time + " ved læsning af tidspunkt");
            }
            time += input.Dequeue().ToString();

            if (input.Peek() != ':')
            {
                errorList.Add("forventer : efter 2 tal. " + input.Peek() + " blev læst  I forbindelse med " + time);
                return true;
            }
            time += input.Dequeue().ToString();

            if (!Regex.IsMatch(input.Peek().ToString(), "[0-5]"))
            {
                errorList.Add("forventer tal inden for [0-5]. tallet " + input.Peek() + " blev læst. I forbindelse med " + time);
            }
            time += input.Dequeue().ToString();

            if (!Regex.IsMatch(input.Peek().ToString(), "[0-9]"))
            {
                errorList.Add("forventer tal inden for [0-9]. tallet " + input.Peek() + " blev læst. I forbindelse med " + time);
            }
            time += input.Dequeue().ToString();

            tokens.Add(new Token(time,TokenType.Time));
            return true;
        }

        private bool isTriggerChar(char firstChar)
        {
            return Regex.IsMatch(firstChar.ToString(), "[0-2]");
        }  
    }
}