using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Export_import.DTO
{
    public class Auteurs
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public Auteurs() { }


        public Author ToDomain()//public Author(int id, string firstname, string surname)
        {
            return new Author(this.ID, this.Naam, "none"); ;
        }
        public static Auteurs FromDomain(Author author)
        {
            return new Auteurs{ ID = author.ID, Naam = author.Firstname +" "+ author.Surname};
        }
        public Auteurs FromDomain2(Author author)
        {
            return new Auteurs { ID = author.ID, Naam = author.Firstname + " " + author.Surname };
        }
    }
}
