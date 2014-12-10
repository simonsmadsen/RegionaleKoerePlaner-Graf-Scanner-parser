using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using RegionaleKorePlaner.Regionskoereplan;
using RegionaleKorePlaner.TokenScanner;
using RegionaleKorePlaner.TokenScanner.ScannerPatterns;

namespace RegionaleKorePlaner.TokenParser
{
    public class Parser
    {
        private Queue<Token> tokens;
        private Scanner scanner;
        private TokenType expectedType;
        public Regionskoereplan.Regionskoereplan Regionskoereplan;
        private object currentObject;
        
        public Parser(Queue<Token> tokens)
        {
            this.tokens = tokens;
            expectedType = TokenType.KoreplanNo;
            Regionskoereplan = new Regionskoereplan.Regionskoereplan();
            currentObject = Regionskoereplan;
        }

        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
            this.scanner.Tokens.TokenAdded += tokenAdded;
            this.tokens = new Queue<Token>();
            expectedType = TokenType.KoreplanNo;
            Regionskoereplan = new Regionskoereplan.Regionskoereplan();
            currentObject = Regionskoereplan;
        }

        private void tokenAdded(Token token)
        {
            tokens.Enqueue(token);
            doParse();
        }  

        public void Parse()
        {
            if (scanner != null)
            {
                scanner.Scan();
            }
            else
            {
                doParse();   
            }      
        }

        private void doParse()
        {
            if (tokens.Count == 0)
            {
                return;
            }

            if (tokens.Peek().type == TokenType.End)
            {
                symanticCheck();
                return;
            }

            if (tokens.Peek().type == expectedType)
            {
                switch (expectedType)
                {
                    case TokenType.KoreplanNo:
                        KorePlan korePlan = new KorePlan(tokens.Dequeue().value);
                        ((Regionskoereplan.Regionskoereplan)currentObject).Koereplan.Add(korePlan);
                        currentObject = korePlan;
                        break;
                    case TokenType.City:
                        Afgang afgang = new Afgang((KorePlan)currentObject, tokens.Dequeue().value);
                        ((KorePlan)currentObject).Afgange.Add(afgang);
                        currentObject = afgang;
                        break;
                    case TokenType.Time:
                        ((Afgang)currentObject).time = tokens.Dequeue().value;
                        currentObject = ((Afgang)currentObject).koreplan;
                        break;
                }
                setNextExpectedType();
                doParse();
            }
            goToNextKoreplanNo();
            doParse();
        }

        private void goToNextKoreplanNo()
        {
            while (tokens.Count != 0 && tokens.Peek().type != TokenType.End && tokens.Peek().type != TokenType.KoreplanNo)
            {
                tokens.Dequeue();
            }
            currentObject = Regionskoereplan;
            expectedType = TokenType.KoreplanNo;
        }

        private void symanticCheck()
        {
            
        }

        private void setNextExpectedType()
        {
            switch (expectedType)
            {
                case TokenType.KoreplanNo :
                    expectedType = TokenType.City;
                    break;
                case TokenType.City :
                    expectedType = TokenType.Time;
                    break;
                case TokenType.Time :
                    expectedType = TokenType.City;
                    break;
            }
        }

    }
}
