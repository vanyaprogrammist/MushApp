using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushApp.Strategs
{
    public static class KfLowStrategy
    {
        public static decimal LTochn(decimal Kf, decimal YkfDop)
        {
            decimal ltochn = (10 + 10 * Kf) / YkfDop;
            
            return ltochn;
        }

        public static decimal LMochn(decimal Kf, decimal P, decimal Po)
        {
            decimal lmochn = (decimal)Math.Sqrt((double)(((P/1000) * Kf) / Po))*10000;
           
            return lmochn;
        }

        public static decimal Maximum(decimal ltochn, decimal lmochn, decimal lmin = 100)
        {
            return new [] {ltochn, lmochn, lmin}.Max();
        }

        public static decimal LRasch(decimal maximum)
        {
            //делаем милиметры
            decimal mil = maximum / 1000;
            //Округляем по нормальным правилам
            decimal round = Math.Round(mil, 2, MidpointRounding.AwayFromZero);
            decimal clipNumber = Clip(round);

            if ((decimal)0.03>clipNumber)
            {
                return round - clipNumber;
            }if((decimal)0.03<=clipNumber && (decimal)0.07>=clipNumber)
            {
                return round - clipNumber + (decimal) 0.05;
            }
            return round - clipNumber + (decimal) 0.1;
        }

        public static decimal Brasch(decimal lrasch, decimal Kf)
        {
            return lrasch / Kf;
        }

        public static decimal LPoln(decimal lrasch)
        {
            return lrasch + (decimal) 0.2;
        }

        public static decimal Square(decimal lpoln, decimal brasch)
        {
            return lpoln * brasch;
        }

        private static decimal Clip(decimal number)
        {
            decimal a = number * 10;
            int b = (int) a;
            decimal c = a - b;
            c = c / 10;
            return c;
        }
    }
}
