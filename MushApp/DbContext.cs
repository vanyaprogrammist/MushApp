using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MushApp.Models;
using Newtonsoft.Json;

namespace MushApp
{
    public class DbContext
    {
        private List<Resistor> resistors = null;

        public List<Resistor> Resistors
        {
            get
            {
                using (StreamReader file = File.OpenText(@"..\..\..\Resistors.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    List<Resistor> resistors = (List<Resistor>) serializer.Deserialize(file, typeof(List<Resistor>));
                    if (resistors != null)
                    {
                        return resistors;
                    }
                    
                        Console.WriteLine("Не могу вернуть резисторы, что-то пошло не так, проверь JSON file Resistors");
                        return null;
                    
                    
                }
            }
        }


    }
}
