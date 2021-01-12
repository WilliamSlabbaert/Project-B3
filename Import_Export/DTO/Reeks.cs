using BusinessLayer.Models;
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

        public ComicstripSerie ToDomain()
        {
            return new ComicstripSerie(this.Naam); ;

        }
        public static Reeks FromDomain(ComicstripSerie serie) 
        {
            return new Reeks { ID = serie.ID, Naam = serie.Name };
        }
    }

    
}
