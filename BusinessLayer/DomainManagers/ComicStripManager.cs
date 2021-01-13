using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ComicStripManager
    {
        public IUnitOfWork uow { get; set; }

        /// <summary> 
        /// Manage the comicstrips
        /// </summary>
        public ComicStripManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new ComicStrip 
        /// </summary>
        public ComicStrip Add(ComicStrip s)
        {
            try
            {
                // Add Serie if not exists
                if (!uow.Comicstrips.ExistSerie(s.Serie))
                    s.SetSerie(uow.Comicstrips.AddSerie(s.Serie));
                // Fetch existing serie if id is not set
                else if (s.Serie.ID < 0)
                    s.SetSerie(uow.Comicstrips.GetSerieByName(s.Serie.Name));
            }
            catch (Exception) { throw new AddException("comicstrip serie"); }
            // Check if comicstrip exists
            if (uow.Comicstrips.Exist(s)) throw new ExistException("comicstrip");
            try
            {
                // Add comicstrip and return object with generated id
                return uow.Comicstrips.Add(s);
            }
            catch (Exception) { throw new AddException("comicstrip"); }
        }

        /// <summary> 
        /// Get a ComicStirp by ID 
        /// </summary>
        public ComicStrip Get(int id)
        {
            return uow.Comicstrips.GetByID(id);
        }

        /// <summary> 
        /// Get list of all ComicStrips 
        /// </summary>
        public List<ComicStrip> GetAll()
        {
            return uow.Comicstrips.GetAll();
        }

        /// <summary> 
        /// Delete comicStrip by ID 
        /// </summary>
        public void Delete(int id)
        {
            uow.Comicstrips.Delete(id);
        }

        /// <summary> 
        /// Delete all ComicStrips 
        /// </summary>
        public void DeleteAll()
        {
            uow.Comicstrips.DeleteAll();
        }

        /// <summary> 
        /// Update existing ComicStrips 
        /// </summary>
        public void Update(ComicStrip s)
        {
            if (!uow.Comicstrips.Exist(s, /* Ignore Id Search */ true)) throw new ExistException("comicstrip");
            try
            {
                uow.Comicstrips.Update(s);
            }
            catch (Exception) { throw new UpdateException("comicstrip"); }
        }

        /// <summary> 
        /// Check if ComicStrips exist
        /// </summary>
        public bool Exist(ComicStrip s, bool ignoreId = false)
        {
           return uow.Comicstrips.Exist(s, ignoreId);
        }

        /// <summary> 
        /// Add a new ComicstripSerie 
        /// </summary>
        public ComicstripSerie AddSerie(ComicstripSerie cs)
        {
            // Check if comicstrip serie exists
            if (uow.Comicstrips.ExistSerie(cs)) throw new ExistException("comicstrip serie");
            try
            {
                // Add comicstrip serie and return object with generated id
                return uow.Comicstrips.AddSerie(cs);
            }
            catch (Exception) { throw new AddException("comicstrip serie"); }
        }

        /// <summary> 
        /// Get ComicstripSerie by ID 
        /// </summary>
        public ComicstripSerie GetSerie(int id)
        {
            return uow.Comicstrips.GetSerie(id);
        }

        /// <summary> 
        /// Get ComicstripSerie by Name 
        /// </summary>
        public ComicstripSerie GetSerieByName(String name)
        {
            return uow.Comicstrips.GetSerieByName(name);
        }

        /// <summary> 
        /// Get a list of all ComicstripSeries
        /// </summary>
        public List<ComicstripSerie> GetAllSeries()
        {
            return uow.Comicstrips.GetAllSeries();
        }

        /// <summary> 
        /// Check if ComicstripSerie exist
        /// </summary>
        public bool ExistSerie(ComicstripSerie cs, bool ignoreId = false)
        {
            return uow.Comicstrips.ExistSerie(cs, ignoreId);
        }
    }
}
