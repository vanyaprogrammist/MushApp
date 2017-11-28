using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using MushApp.Models;
using MushApp.Strategs;
using Newtonsoft.Json;
using Console = System.Console;

namespace MushApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext db = new DbContext();
            var wb = new XLWorkbook(@"..\..\..\Table1.xlsx");
            var ws = wb.Worksheet("Data");
            

            Init init = new Init(db, ws);
            init.Strat();
            wb.Save();
            /*double k = 3.195;
            double round = Math.Round(k, 2, MidpointRounding.AwayFromZero);
            k = k * 10;
            int a = (int)k;
           double b = (double)a / 10;*/
            /*Console.WriteLine(b);
            
            Console.WriteLine(round);

            Console.WriteLine(new double[]{3.1488, 3.166, 3.1667 }.Max());*/



            Console.ReadKey();
        }
    }
}
