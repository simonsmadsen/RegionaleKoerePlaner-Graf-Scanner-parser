using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner.Regionskoereplan
{
    public class KorePlan
    {
        public string No;
        public List<Afgang> Afgange;

        public KorePlan(string no,List<Afgang> afgange = null)
        {
            this.No = no;
            if (afgange == null)
            {
                this.Afgange = afgange;
            }
            this.Afgange = new List<Afgang>();
        }
    }
}
