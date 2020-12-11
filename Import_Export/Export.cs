using BusinessLayer;
using DataLayer;
using Export_import.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Import_Export
{
    public class Export
    {
        public static void export(string location)
        {
            UnitOfWork uow = new UnitOfWork();
            ComicStripManager CM = new ComicStripManager(uow);
            AuthorManager AM = new AuthorManager(uow);
            PublisherManager PM = new PublisherManager(uow);

            //getting all 
            List<ComicStrip> ComicStrips = CM.GetAll();

            //change to DTO classes
            List<Strip> strips = ComicStrips.Select(x => Strip.FromDomain(x)).ToList();
            /*  TODO: add autors en uitgeverijen
            List<Auteurs> auteurs = Authors.Select(x => Auteurs.FromDomain(x)).ToList();
            List<Uitgeverij> uitgeverijs = Publishers.Select(x => Uitgeverij.FromDomain(x)).ToList();
            */
            string rawJson = JsonConvert.SerializeObject(strips);

            Console.WriteLine(rawJson);
            DirectoryInfo dir = new DirectoryInfo(location);
            File.WriteAllText(dir + "\\DatabaseDump.json", rawJson);

            Console.WriteLine("i have not crashed :) ");
        }
    }
}
