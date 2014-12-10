using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner.TokenScanner
{
    public class Token
    {
        public TokenType type;
        public string value;

        public Token(string value,TokenType type)
        {
            this.type = type;
            this.value = value;
        }
    }
}
