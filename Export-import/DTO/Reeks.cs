using System;
using System.Collections.Generic;
using System.Text;

namespace Export_import.DTO
{
    public class Reeks
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public Strip[] Strips { get; set; }

        public static Reeks FromDomain(string naam)
        {
            return new Reeks { Naam = naam };
        }
    }

    
}
