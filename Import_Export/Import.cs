using BusinessLayer;
using BusinessLayer.Models;
using DataLayer;
using Export_import.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Import_Export
{
    public class Import
    {
        public static Dictionary<string, List<Strip>> Errors = new Dictionary<string, List<Strip>>();
        public static int countCorrect = 0;

        public static void import(string location)
        {
            UnitOfWork uow = new UnitOfWork();
            ComicStripManager CM = new ComicStripManager(uow);
            AuthorManager AM = new AuthorManager(uow);
            PublisherManager PM = new PublisherManager(uow);

            //Publisher , Author en comicStrip aan de databank toe voegen
            foreach (ComicStrip comicStrip in ReadJson(location))
            {
                try
                {
                    // Publisher toe voegen 
                    if (!PM.Exist(comicStrip.Publisher, true))
                    {
                        comicStrip.SetPublisher(PM.Add(comicStrip.Publisher));
                    }
                    else
                    {
                        comicStrip.SetPublisher(PM.GetByName(comicStrip.Publisher.Name));
                    }
                   
                    // Authors toe voegen 
                    List<Author> tempAuthor = new List<Author>();
                    foreach (Author author in comicStrip.Authors)
                    {
                        if (AM.Exist(author, true))
                        {
                            tempAuthor.Add(AM.GetByName(author.Firstname,author.Surname));
                        }
                        else
                        {
                            tempAuthor.Add(AM.Add(author));
                        }
                    }
                    // Reeks toe voegen 
                    if (!CM.ExistSerie(comicStrip.Serie, true))
                    {
                        comicStrip.SetSerie(CM.AddSerie(comicStrip.Serie));
                    }
                    else
                    {
                        comicStrip.SetSerie(CM.GetSerieByName(comicStrip.Serie.Name));
                    }
                    // Commiting Changes 
                    comicStrip.SetAuthors(tempAuthor);
                    CM.Add(comicStrip);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //get file dir
            List<string> split = location.Split("\\").ToList();
            split.RemoveAt(split.Count - 1);
            string newLocation = "";

            foreach (string s in split)
                newLocation += s + "\\";

            //get all bad comics 
            List<Strip> rejectstrips = new List<Strip>();

            foreach (var key in Errors.Keys)
                foreach (Strip strip in Errors[key])
                    rejectstrips.Add(strip);

            //dump data
            string newrawJson = JsonConvert.SerializeObject(rejectstrips);
            DirectoryInfo dir = new DirectoryInfo(newLocation);
            File.WriteAllText(dir + "\\RejectDump.json", newrawJson);
        }
        public static List<ComicStrip> ReadJson(String location)
        {
            //read file 
            string rawJson = "";
            foreach (string line in File.ReadAllLines(location))
                rawJson += line.Trim();

            //get stripDTO from rawjson 
            List<Strip> strips = JsonConvert.DeserializeObject<Strip[]>(rawJson).ToList();

            //setting up for converting 
            List<ComicStrip> ComicStrips = new List<ComicStrip>();

            // Chage DTO to Domain classes 
            foreach (Strip strip in strips)
            {
                try
                {
                    ComicStrips.Add(strip.ToDomain());
                    countCorrect++;
                }
                catch (Exception e)
                {
                    if (!Errors.ContainsKey(e.Message))
                        Errors[e.Message] = new List<Strip>();

                    Errors[e.Message].Add(strip);
                }
                
            }
            return ComicStrips;
        }
    }
}
