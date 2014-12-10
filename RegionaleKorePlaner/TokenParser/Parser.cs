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

        public void Parse()
        {
            if (tokens.Count == 0)
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
                        Afgang afgang = new Afgang((KorePlan)currentObject,tokens.Dequeue().value);
                        ((KorePlan)currentObject).Afgange.Add(afgang);
                        currentObject = afgang;
                        break;
                    case TokenType.Time:
                        ((Afgang) currentObject).time = tokens.Dequeue().value;
                        currentObject = ((Afgang) currentObject).koreplan;
                        break;
                }
                setNextExpectedType();
                Parse();
            }
            goToNextKoreplanNo();
            Parse();
        }

        private void goToNextKoreplanNo()
        {
            while (tokens.Count != 0 && tokens.Peek().type != TokenType.KoreplanNo)
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
