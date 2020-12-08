using BusinessLayer;
using Export_import.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer;

namespace Export_import
{//"C:\Users\kevin\Documents\project werk graduaat\Project-B3-master(1)\dump(1).json"
    public class Program
    {
            
        static void Main(string[] args)
        {
            Console.WriteLine("I'm Aive!");
            
            
            Console.WriteLine("Export of Import?");
            string ant = Console.ReadLine();
            string location = GetJsonLocation();
            switch (ant)
            {
                case "Export":
                    Export(location);
                    break;
                case "e":
                    Export(location);
                    break;
                case "Import":
                    Import(location);
                    break;
                case "i":
                    Import(location);
                    break;
                default:
                    Console.WriteLine("thats not a option");
                    break;
            }

        }
        public static string GetJsonLocation()
        {
            Console.WriteLine("Waar is de Json file?");
            string ant = Console.ReadLine();
            if (ant == "a") { ant = @"C:\Users\kevin\Documents\project werk graduaat\Project-B3-master(1)\dump(1).json"; }
            if (ant == "b") { ant = @"C:\Users\kevin\Documents\project werk graduaat\Project-B3-master(1)"; }
            return ant;
        }

        public static void Import(string location)
        {
            UnitOfWork uow = new UnitOfWork();
            ComicStripManager CM = new ComicStripManager(uow);

            string rawJson = "";
            foreach (string line in File.ReadAllLines(location))
            {
                rawJson += line.Trim();
            }


            //get stripDTO from rawjson 
            List<Strip> strips =  JsonConvert.DeserializeObject<Strip[]>(rawJson).ToList();

            //setting up for converting 
            List<ComicStrip> ComicStrips = new List<ComicStrip>();
            Dictionary<string, List<Strip>> Errors = new Dictionary<string, List<Strip>>();
            int countCorrect = 0;
            int count = 0;

            // Chage DTO to Domain classes 
            foreach (Strip strip in strips)
            {
                Console.WriteLine("Currently at nr " + count); count++;
                try
                {
                    ComicStrips.Add(strip.ToDomain());
                    countCorrect++;
                }
                catch (Exception e)
                {
                    if (!Errors.ContainsKey(e.Message))
                    {
                        Errors[e.Message] = new List<Strip>();
                    }
                    Errors[e.Message].Add(strip);
                }
            }
            Console.WriteLine("Errors:");
            foreach (string errorMessage in Errors.Keys)
            {
                Console.WriteLine(errorMessage + " " + Errors[errorMessage].Count);
            }
            Console.WriteLine("Correct imports: " + countCorrect);
            
            foreach (ComicStrip comicStrip in ComicStrips)
            {
                CM.Add(comicStrip);
            }
            Console.WriteLine("i have not crashed :) ");
        }
        public static void Export(string location)
        {
            UnitOfWork uow = new UnitOfWork();
            ComicStripManager CM = new ComicStripManager(uow);

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
            File.WriteAllText(dir+"\\DatabaseDump.json", rawJson);

            Console.WriteLine("i have not crashed :) ");

        }
        

    }
    

}
