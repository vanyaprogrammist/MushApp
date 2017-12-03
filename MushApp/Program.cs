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
           



            Console.ReadKey();
        }
    }
}
