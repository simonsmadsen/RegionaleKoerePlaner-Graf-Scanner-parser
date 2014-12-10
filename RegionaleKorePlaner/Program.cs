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

            Scanner scanner = new Scanner("RKP.txt");
            scanner.Add(new CityPattern());
            scanner.Add(new KoreplanNoPattern());
            scanner.Add(new LineBreakPattern());
            scanner.Add(new TimePattern());
            scanner.Scan();
            scanner.Print();

            if (scanner.Errors.Count != 0)
            {
                Console.WriteLine("*** Scanner Errors ***");
                foreach (string error in scanner.Errors)
                {
                    Console.WriteLine(error);
                }               
            }

            Parser parser = new Parser(scanner.Tokens);
            parser.Parse();
            
            // Debug og se det virker! :)
            RegionaleKorePlaner.Regionskoereplan.Regionskoereplan regionskoereplan = parser.Regionskoereplan;

            Scanner scanner2 = new Scanner("RKP.txt");
            scanner2.Add(new CityPattern());
            scanner2.Add(new KoreplanNoPattern());
            scanner2.Add(new LineBreakPattern());
            scanner2.Add(new TimePattern());
            Parser parser2 = new Parser(scanner2);
            parser2.Parse();

            // Debug og se det virker! :)
            RegionaleKorePlaner.Regionskoereplan.Regionskoereplan regionskoereplan2 = parser2.Regionskoereplan;

            Console.ReadKey();
        }
    }
}
