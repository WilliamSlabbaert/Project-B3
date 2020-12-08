using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Export_import.DTO
{
    public class Strip
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int? Nr { get; set; }
        public Reeks Reeks { get; set; }
        public Uitgeverij Uitgeverij { get; set; }
        public List<Auteurs> Auteurs { get; set; }
        
        public Strip() { }

        public ComicStrip ToDomain()//public ComicStrip(int id, string titel, string serie, int number, List<Author> authors, Publisher publisher)
        {
            if(Nr is null)
                throw new NoNrException();
            if (Auteurs is null)
                throw new NoAuthorException();
            if (Uitgeverij is null)
                throw new NoUitgeverijException();
            if (Reeks is null)
                throw new NoReeksException();

            List<Author> Authors = Auteurs.Select(x => x.ToDomain()).ToList();

            ComicStrip tempComicStrip = new ComicStrip(this.Titel, this.Reeks.Naam, (int)this.Nr, Authors, Uitgeverij.ToDomain());
            return tempComicStrip;
        }
        public static Strip FromDomain(ComicStrip comicStrip)
        {
            Auteurs tempAuteur = new Auteurs();
            List<Auteurs> Authors = comicStrip.Authors.Select(x => tempAuteur.FromDomain2(x)).ToList();
            return new Strip { ID = comicStrip.ID, Titel = comicStrip.Titel, Nr = comicStrip.Number, Reeks = Reeks.FromDomain(comicStrip.Serie), Auteurs = Authors };
        }

        public class NoAuthorException : Exception
        {
            public NoAuthorException() : base(String.Format("Geen authors")) { }
        }
        public class NoUitgeverijException : Exception
        {
            public NoUitgeverijException() : base(String.Format("Geen uitgeverij")) { }
        }
        public class NoReeksException : Exception
        {
            public NoReeksException() : base(String.Format("Geen Reeks")) { }
        }
        public class NoNrException : Exception
        {
            public NoNrException() : base(String.Format("Geen Nr")) { }
        }
    }
}
