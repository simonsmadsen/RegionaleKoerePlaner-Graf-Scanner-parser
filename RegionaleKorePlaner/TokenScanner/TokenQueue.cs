using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner.TokenScanner
{
    public delegate void TokenAddedDelegate(Token token);

    public class TokenQueue : Queue<Token>
    {
        public event TokenAddedDelegate TokenAdded;
        public void Add(Token item)
        {
            if (TokenAdded != null)
            {
                TokenAdded(item);
             
            }
            this.Enqueue(item);
        }
    }
}
