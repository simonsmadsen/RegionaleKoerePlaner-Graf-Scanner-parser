using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegionaleKorePlaner.TokenScanner.ScannerPatterns
{
    public class KoreplanNoPattern : IScannerPattern
    {
       public string TriggerChar { get; private set; }

        public KoreplanNoPattern()
        {
            TriggerChar = "#";
        }

        public bool CheckPattern(Queue<char> input, List<string> tokens, List<string> errorList)
        {
            if (! isTriggerChar(input.Peek())) 
                return false;
          
            string koereplanNo = input.Dequeue().ToString();
            while (Regex.IsMatch(input.Peek().ToString(), "[0-9]"))
            {
                koereplanNo += input.Dequeue();
            }
            tokens.Add(koereplanNo);
            return true;
        }

        private bool isTriggerChar(char firstChar)
        {
            return firstChar == '#';
        }
    }
}