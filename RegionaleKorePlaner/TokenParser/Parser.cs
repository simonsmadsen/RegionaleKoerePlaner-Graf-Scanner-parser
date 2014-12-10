using System.Collections.Generic;
using RegionaleKorePlaner.Regionskoereplan;
using RegionaleKorePlaner.TokenScanner;

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
                scanner.Scan();
            else
                doParse();        
        }

        private void doParse()
        {
            // Hvis der ikke er tokens venter vi på flere fra scanneren.
            if (tokens.Count == 0)
            {
                return;
            }
            // Der er ikke flere tokens, 
            if (tokens.Peek().type == TokenType.End)
            {
                symanticCheck();
                return;
            }

            // Tjekker om den første i køen er den forventede type
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
                // Sætter en ny forventet type
                setNextExpectedType();
                // Kalder do Parse, for at parse den næste i køen
                doParse();
            }
            // Starter forfra, med at forvente koreplan no
            goToNextKoreplanNo();
            // Kalder do Parse, for at parse den næste i køen
            doParse();
        }

        private void goToNextKoreplanNo()
        {
            // Fjerner fra køen indtil den er tom, der er et endtoken eller token er et køreplans no.
            while (tokens.Count != 0 && tokens.Peek().type != TokenType.End && tokens.Peek().type != TokenType.KoreplanNo)
            {
                tokens.Dequeue();
            }
            currentObject = Regionskoereplan;
            expectedType = TokenType.KoreplanNo;
        }

        private void symanticCheck()
        {
            // Check symantic
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
