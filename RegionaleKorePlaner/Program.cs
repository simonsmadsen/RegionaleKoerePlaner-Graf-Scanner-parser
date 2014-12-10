using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RegionaleKorePlaner.TokenParser;
using RegionaleKorePlaner.TokenScanner;
using RegionaleKorePlaner.TokenScanner.ScannerPatterns;

namespace RegionaleKorePlaner
{
    class Program
    {
        private const string Odense = "Odense";
        private const string Middelfart = "Middelfart";
        private const string Otterup = "Otterup";
        private const string Ringe = "Ringe";
        private const string Assens = "Assens";
        private const string Kerteminde = "Kerteminde";
        private const string Svendborg = "Svendborg";
        private const string Nyborg = "Nyborg";
        private const string Bogense = "Bogense";

        static void Main(string[] args)
        {
            Console.WindowWidth = 200;

            Graf grafFynBus = new Graf();

            Console.WriteLine("--------FYNBUS GRAF-----------");

            grafFynBus.AddRoute(Odense, 67, Middelfart);
            grafFynBus.AddRoute(Bogense, 67, Otterup);
            grafFynBus.AddRoute(Otterup, 22, Odense);
            grafFynBus.AddRoute(Ringe, 20, Assens);
            grafFynBus.AddRoute(Assens, 30, Odense);
            grafFynBus.AddRoute(Odense, 31, Kerteminde);
            grafFynBus.AddRoute(Svendborg, 41, Odense);
            grafFynBus.AddRoute(Odense, 42, Bogense);
            grafFynBus.AddRoute(Bogense, 43, Middelfart);
            grafFynBus.AddRoute(Middelfart, 44, Assens);
            grafFynBus.AddRoute(Svendborg, 45, Nyborg);
            grafFynBus.AddRoute(Nyborg, 46, Odense);

            grafFynBus.PrintAllNodeEdges();

            Console.WriteLine("DepthFirst : " + grafFynBus.DepthFirst());
            Console.WriteLine("BreadthFirst : " + grafFynBus.BreadthFirst());

            grafFynBus.GetMinSpanningTreeByKruskal().PrintAllNodeEdges();

            // Opretter scanner
            Scanner scanner = new Scanner("RKP.txt");
            scanner.Add(new CityPattern());
            scanner.Add(new KoreplanNoPattern());
            scanner.Add(new LineBreakPattern());
            scanner.Add(new TimePattern());
            // Scanner filen
            scanner.Scan();
            // Printer det resultat scanneren kom med.
            scanner.Print();

            // Printer hvis der forekom en fejl under scanningen.
            if (scanner.Errors.Count != 0)
            {
                Console.WriteLine("*** Scanner Errors ***");
                foreach (string error in scanner.Errors)
                {
                    Console.WriteLine(error);
                }               
            }
            // Opretter en nu parser, med en kø af tokens.
            Parser parser = new Parser(scanner.Tokens);
            // Parser køen af tokens.
            parser.Parse();
            
            // Debug og se det virker! :)
            RegionaleKorePlaner.Regionskoereplan.Regionskoereplan regionskoereplan = parser.Regionskoereplan;

            // Opretter scanner
            Scanner scanner2 = new Scanner("RKP.txt");
            scanner2.Add(new CityPattern());
            scanner2.Add(new KoreplanNoPattern());
            scanner2.Add(new LineBreakPattern());
            scanner2.Add(new TimePattern());
            // Opretter parser med scanner.
            Parser parser2 = new Parser(scanner2);
            parser2.Parse();

            // Debug og se det virker! :)
            RegionaleKorePlaner.Regionskoereplan.Regionskoereplan regionskoereplan2 = parser2.Regionskoereplan;

            Console.ReadKey();
        }
    }
}
