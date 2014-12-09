using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegionaleKorePlaner.TokenScanner.ScannerPatterns
{
    public class CityPattern : IScannerPattern
    {
        public string TriggerChar { get; set; }

        public CityPattern()
        {
            TriggerChar = "[a-z]";
        }

        public bool CheckPattern(Queue<char> input, List<string> tokens, List<string> errorList)
        {
            if (! isTriggerChar(input.Peek()))
                return false;

            string city = input.Dequeue().ToString();
            while (Regex.IsMatch(input.Peek().ToString(), "[a-z]", RegexOptions.IgnoreCase))
            {
                city += input.Dequeue();
            }
            tokens.Add(city);
            return true;
        }

        private bool isTriggerChar(char firstChar)
        {
            return Regex.IsMatch(firstChar.ToString(), "[a-z]", RegexOptions.IgnoreCase);
        } 
    }
}