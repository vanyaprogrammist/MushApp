using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MushApp.Models;

namespace MushApp.Strategs
{
    public class Init
    {
        private DbContext db;

        private IXLWorksheet ws;

        public Init(DbContext db, IXLWorksheet ws)
        {
            this.db = db;
            this.ws = ws;
        }



        private readonly decimal PoIgroup = 1;
        private readonly decimal PoIIgroup = 2;
        private readonly decimal YkfDopIgroup = (decimal) 0.017;
        private readonly decimal YkfDopIIgroup = (decimal) 0.016;

        public void Strat()
        {
            for(int i=0;i<db.Resistors.Count;i++)
            {
                decimal Kf = Decimal.Parse(db.Resistors[i].Kf, CultureInfo.InvariantCulture);
                decimal P = Decimal.Parse(db.Resistors[i].P, CultureInfo.InvariantCulture);
                if (10 >= Kf && Kf >= 1)
                {
                    if (db.Resistors[i].GroupNumber == 1)
                    {
                        decimal btochn = KfHighStrategy.BTochn(Kf, YkfDopIgroup);
                        decimal bmochn = KfHighStrategy.BMochn(Kf,
                            P, PoIgroup);
                        decimal maximum = KfHighStrategy.Maximum(btochn, bmochn);
                        decimal brasch = KfHighStrategy.BRasch(maximum);
                        decimal lrasch = KfHighStrategy.LRasch(Kf, brasch);
                        decimal lpoln = KfHighStrategy.LPoln(lrasch);
                        decimal square = KfHighStrategy.Square(lpoln, brasch);

                        ws.Cell($"A{i + 2}").Value = db.Resistors[i].Number;
                        ws.Cell($"B{i + 2}").Value = db.Resistors[i].R;
                        ws.Cell($"C{i + 2}").Value = Kf;
                        ws.Cell($"H{i + 2}").Value = brasch;
                        ws.Cell($"I{i + 2}").Value = Math.Round(lrasch, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"J{i + 2}").Value = Math.Round(lpoln, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"K{i + 2}").Value = Math.Round(square, 3, MidpointRounding.AwayFromZero);

                        ws.Cell($"D{i + 2}").Value = Math.Round(btochn, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"E{i + 2}").Value = Math.Round(bmochn, 3, MidpointRounding.AwayFromZero);

                        Console.WriteLine(
                            $"{db.Resistors[i].Number}: bточн: {btochn} | bмощн: {bmochn} | maximum: {maximum} | bрасч: {brasch} | lрасч: {lrasch} | lполн: {lpoln} | lsq: {square}");
                    }
                    else
                    {
                        decimal btochn = KfHighStrategy.BTochn(Kf, YkfDopIIgroup);
                        decimal bmochn = KfHighStrategy.BMochn(Kf,
                            P, PoIIgroup);
                        decimal maximum = KfHighStrategy.Maximum(btochn, bmochn);
                        decimal brasch = KfHighStrategy.BRasch(maximum);
                        decimal lrasch = KfHighStrategy.LRasch(Kf, brasch);
                        decimal lpoln = KfHighStrategy.LPoln(lrasch);
                        decimal square = KfHighStrategy.Square(lpoln, brasch);

                        ws.Cell($"A{i + 2}").Value = db.Resistors[i].Number;
                        ws.Cell($"B{i + 2}").Value = db.Resistors[i].R;
                        ws.Cell($"C{i + 2}").Value = Kf;
                        ws.Cell($"H{i + 2}").Value = brasch;
                        ws.Cell($"I{i + 2}").Value = Math.Round(lrasch, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"J{i + 2}").Value = Math.Round(lpoln, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"K{i + 2}").Value = Math.Round(square, 3, MidpointRounding.AwayFromZero);

                        ws.Cell($"D{i + 2}").Value = Math.Round(btochn, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"E{i + 2}").Value = Math.Round(bmochn, 3, MidpointRounding.AwayFromZero);

                        Console.WriteLine(
                            $"{db.Resistors[i].Number}: bточн: {btochn} | bмощн: {bmochn} | maximum: {maximum} | bрасч: {brasch} | lрасч: {lrasch} | lполн: {lpoln} | lsq: {square}");
                    }
                }
                else if ((decimal) 0.1 <= Kf && Kf < 1)
                {
                    if (db.Resistors[i].GroupNumber == 1)
                    {
                        decimal ltochn = KfLowStrategy.LTochn(Kf, YkfDopIgroup);
                        decimal lmochn = KfLowStrategy.LMochn(Kf, P, PoIgroup);
                        decimal maximum = KfLowStrategy.Maximum(ltochn, lmochn);
                        decimal lrasch = KfLowStrategy.LRasch(maximum);
                        decimal brasch = KfLowStrategy.Brasch(lrasch, Kf);
                        decimal lpoln = KfLowStrategy.LPoln(lrasch);
                        decimal square = KfLowStrategy.Square(lpoln, brasch);

                        ws.Cell($"A{i + 2}").Value = db.Resistors[i].Number;
                        ws.Cell($"B{i + 2}").Value = db.Resistors[i].R;
                        ws.Cell($"C{i + 2}").Value = Kf;
                        ws.Cell($"H{i + 2}").Value = Math.Round(brasch, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"I{i + 2}").Value = lrasch;
                        ws.Cell($"J{i + 2}").Value = Math.Round(lpoln, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"K{i + 2}").Value = Math.Round(square, 3, MidpointRounding.AwayFromZero);

                        ws.Cell($"F{i + 2}").Value = Math.Round(ltochn, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"G{i + 2}").Value = Math.Round(lmochn, 3, MidpointRounding.AwayFromZero);

                        Console.WriteLine(
                            $"{db.Resistors[i].Number}: lточн: {ltochn} | lмощн: {lmochn} | maximum: {maximum} | lрасч: {lrasch} | bрасч: {brasch} | lполн: {lpoln} | lsq: {square}");
                    }
                    else
                    {
                        decimal ltochn = KfLowStrategy.LTochn(Kf, YkfDopIIgroup);
                        decimal lmochn = KfLowStrategy.LMochn(Kf, P, PoIIgroup);
                        decimal maximum = KfLowStrategy.Maximum(ltochn, lmochn);
                        decimal lrasch = KfLowStrategy.LRasch(maximum);
                        decimal brasch = KfLowStrategy.Brasch(lrasch, Kf);
                        decimal lpoln = KfLowStrategy.LPoln(lrasch);
                        decimal square = KfLowStrategy.Square(lpoln, brasch);

                        ws.Cell($"A{i + 2}").Value = db.Resistors[i].Number;
                        ws.Cell($"B{i + 2}").Value = db.Resistors[i].R;
                        ws.Cell($"C{i + 2}").Value = Kf;
                        ws.Cell($"H{i + 2}").Value = Math.Round(brasch, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"I{i + 2}").Value = lrasch;
                        ws.Cell($"J{i + 2}").Value = Math.Round(lpoln, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"K{i + 2}").Value = Math.Round(square, 3, MidpointRounding.AwayFromZero);

                        ws.Cell($"F{i + 2}").Value = Math.Round(ltochn, 3, MidpointRounding.AwayFromZero);
                        ws.Cell($"G{i + 2}").Value = Math.Round(lmochn, 3, MidpointRounding.AwayFromZero);

                        Console.WriteLine(
                            $"{db.Resistors[i].Number}: lточн: {ltochn} | lмощн: {lmochn} | maximum: {maximum} | lрасч: {lrasch} | bрасч: {brasch} | lполн: {lpoln} | lsq: {square}");
                    }
                }
            }
        }
    }
}
