using System.Collections.Generic;
using Newtonsoft.Json;

namespace MushApp.Models
{
    [JsonObject]
    public class Resistor
    {
        public int Number { get; set; }
        public int R { get; set; }
        public int Gr { get; set; }
        public int C { get; set; }
        public int Gc { get; set; }
        public string I { get; set; }
        public string P { get; set; }
        public string Kf { get; set; }
        public int GroupNumber { get; set; }
    }

    
}
