using BusinessLayer;
using BusinessLayer.Models;
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
        public static void export(string exportpath)
        {
            // Check if exportpath is valid
            if (Path.GetExtension(exportpath).ToLower() != ".json") throw new InvalidExportpathException();
            ComicStripManager cm = new ComicStripManager(new UnitOfWork());
            // Get all ComicStrips
            List<ComicStrip> ComicStrips = cm.GetAll();
            // Convert to DTO objects
            List<Strip> strips = ComicStrips.Select(x => Strip.FromDomain(x)).ToList();
            // Convert to JSON string
            string rawJson = JsonConvert.SerializeObject(strips);
            File.WriteAllText(exportpath, rawJson);
        }

        public class InvalidExportpathException : Exception
        {
            public InvalidExportpathException() : base(String.Format("Invalid epxortpath provided")) { }
        }
    }
}
