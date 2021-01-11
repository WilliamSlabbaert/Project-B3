using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IComicStripRepository
    {
        public ComicStrip Add(ComicStrip s);
        public ComicStrip GetByID(int id);
        public List<ComicStrip> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(ComicStrip s);
        public bool Exist(ComicStrip s, bool ignoreId = false);
        public ComicstripSerie AddSerie(ComicstripSerie cs);
        public ComicstripSerie GetSerie(int serie);
        public ComicstripSerie GetSerieByName(String name);
        public List<ComicstripSerie> GetAllSeries();
        public bool ExistSerie(ComicstripSerie cs, bool ignoreId = false);
    }
}