﻿using System;
using System.Linq;
using ClosedXML.Excel;

namespace MushApp.Strategs
{
    public static class KfHighStrategy
    {
        public static decimal BTochn(decimal Kf,decimal YkfDop)
        {
            decimal btochn = (10 + (10 / Kf)) / YkfDop;
            
            return btochn;
        }

        public static decimal BMochn(decimal Kf,decimal P, decimal Po)
        {
            decimal bmochn = (decimal)Math.Sqrt((double)((P/1000) / (Kf * Po)))*10000;
            
            return bmochn;
        }

        public static decimal Maximum(decimal btochn, decimal bmochn, decimal bmin =100)
        {
            return new [] {btochn, bmochn, bmin }.Max();
        }

        public static decimal Mil(decimal micron)
        {
            return micron / 1000;
        }

        public static decimal BRasch(decimal maximum)
        {
            decimal round;
            decimal two = ClipReverse(maximum, 2);
            decimal twotwo = ClipReverse(maximum, 2);
            decimal three = ClipReverse(maximum, 3);
            if ((ClipReverse(maximum, 2) == (decimal) 0.05 || ClipReverse(maximum, 2) == 0) && ClipReverse(maximum, 3) != 0)
            {
                
                    round = Clip(maximum,2)+(decimal)0.01;
            }
            else
            {
                //Округляем по нормальным правилам
                round = Math.Round(maximum, 2, MidpointRounding.AwayFromZero);
            }
            //1.1 | +0.5 | 1.15 
            decimal five = Clip10(round) + (decimal)0.05;

            
            decimal result = five - round;
            

            if (result >= 0)
            {

                if (result == (decimal)0.05)
                {
                    return round;
                }
                return five;
            }
            else
            {
                return five + (decimal)0.05;
            }
        }

        public static decimal LRasch(decimal Kf, decimal brasch)
        {
            return 
            KfLowStrategy.LRasch(Kf * brasch);
        }

        public static decimal LPoln(decimal lrasch)
        {
            return lrasch + (decimal) 0.2;
        }

        public static decimal Square(decimal lpoln, decimal brasch)
        {
            return lpoln * brasch;
        }

        public static decimal Clip(decimal number, int numeral)
        {
            number = number * (decimal)Math.Pow(10,numeral);
            int a = (int)number;
            number = (decimal)a / (decimal)Math.Pow(10, numeral);
            return number;
        }

        public static decimal ClipReverse(decimal number, int numeral)
        {
            numeral = numeral - 1;
            decimal a = number * (decimal)Math.Pow(10,numeral);
            int b = (int)a;
            decimal c = a - b;
            c = c / (decimal)Math.Pow(10, numeral);
            c = Clip(c, numeral + 1);
            return c;
        }
       

        private static decimal Clip10(decimal number)
        {
            number = number * 10;
            int a = (int) number;
            number = (decimal) a / 10;
            return number;
        }

        private static decimal Clip100(decimal number)
        {
            number = number * 100;
            int a = (int)number;
            number = (decimal)a / 100;
            return number;
        }
    }
}
