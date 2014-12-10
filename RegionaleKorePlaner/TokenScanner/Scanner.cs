using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using RegionaleKorePlaner.TokenScanner.ScannerPatterns;

namespace RegionaleKorePlaner.TokenScanner
{
    public class Scanner
    {
        private List<IScannerPattern> scannerPatterns;
        private Queue<char> input;
        public List<Token> Tokens { get; private set; }
        public List<string> Errors { get; private set; }

        public Scanner(string filename)
        {
            scannerPatterns = new List<IScannerPattern>();
            this.input = new Queue<char>(System.IO.File.ReadAllText(@"C:\temp\"+filename));
            Tokens = new List<Token>();
            Errors = new List<string>();
        }

        public void Print()
        {
            foreach (Token token in Tokens)
            {
                Console.WriteLine(token.value);
            }
        }

        public void Scan()
        { 
            while (input.Count != 0)
            {
                bool patternMatch = false;
                foreach (IScannerPattern scannerPattern in scannerPatterns)
                {
                    if (scannerPattern.CheckPattern(input, Tokens, Errors))
                    {
                        patternMatch = true;
                        break;
                    }
                }

                if (!patternMatch)
                {
                    input.Dequeue();
                }            
            }
        }

        public bool Add(IScannerPattern scannerPattern)
        {
            if (!isTriggerCharUsed(scannerPattern))
            {
                scannerPatterns.Add(scannerPattern);
                return true;
            }
            return false;
        }

        private bool isTriggerCharUsed(IScannerPattern scannerPattern)
        {
            bool result = false;
            foreach (IScannerPattern pattern in scannerPatterns)
            {
                if (scannerPattern.TriggerChar == pattern.TriggerChar)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
