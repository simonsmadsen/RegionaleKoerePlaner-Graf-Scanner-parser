using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner.Regionskoereplan
{
    public class Regionskoereplan
    {
        public List<KorePlan> Koereplan;

        public Regionskoereplan(List<KorePlan> koreplan = null)
        {
            this.Koereplan = koreplan;
            if (koreplan == null)
            {
                this.Koereplan = new List<KorePlan>();
            }
        }

    }
}
