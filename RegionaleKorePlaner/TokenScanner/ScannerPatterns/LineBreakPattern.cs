using System;
using System.Collections.Generic;

namespace RegionaleKorePlaner.TokenScanner.ScannerPatterns
{
    public class LineBreakPattern : IScannerPattern
    {
        public string TriggerChar { get; private set; }

        public LineBreakPattern()
        {
            TriggerChar = "\\";
        }

        public bool CheckPattern(Queue<char> input, List<Token> tokens,List<string> errorList)
        {
            if (! isTriggerChar(input.Peek()))
            {
                return false;
            }

            input.Dequeue();
            if (input.Peek() != 'r')
            {
                errorList.Add("Forventer r efter \\");
                return true;
            }
            input.Dequeue();
            if (input.Peek() != '\\')
            {
                errorList.Add("Forventer \\ efter \\r");
                return true;
            }
            input.Dequeue();
            if (input.Peek() != 'n')
            {
                errorList.Add("Forventer n efter \\r\\");
                return true;
            }
            input.Dequeue();
            return true;
        }

        private bool isTriggerChar(char firstChar)
        {
            return firstChar == '\\';
        }

    }
}