using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner.Regionskoereplan
{
    public class Afgang
    {
        public string city;
        public string time;
        public KorePlan koreplan;

        public Afgang(KorePlan koreplan, string city)
        {
            this.koreplan = koreplan;
            this.city = city;
        }
        
    }
}
