using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RegionaleKorePlaner.TokenScanner.ScannerPatterns
{
    public interface IScannerPattern
    {
        string TriggerChar { get; }

        bool CheckPattern(Queue<char> input, List<Token> tokens,List<string> errors);
    }
}