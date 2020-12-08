﻿using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Export_import.DTO
{
    public class Uitgeverij
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public Uitgeverij() { }

        public Publisher ToDomain()//public Publisher(int id, string name)
        {
            Publisher tempPublisher = new Publisher(this.ID, this.Naam) ;
            return tempPublisher;
        }
        public static Uitgeverij FromDomain(Publisher publisher)
        {
            return new Uitgeverij {Naam = publisher.Name};
        }

    }
}
