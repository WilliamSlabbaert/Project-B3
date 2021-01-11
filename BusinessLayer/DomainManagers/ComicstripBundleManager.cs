using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ComicstripBundleManager
    {
        public IUnitOfWork uow { get; set; }

        /// <summary> 
        /// Manage the comicstripBundles
        /// </summary>
        public ComicstripBundleManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new ComicstripBundle 
        /// </summary>
        public ComicstripBundle Add(ComicstripBundle b)
        {
            // Check if comicstrip  bundle exists
            if (uow.ComicstripBundles.Exist(b)) throw new ExistException("comicstrip bundle");
            try
            {
                // Add comicstrip bundle and return object with generated id
                return uow.ComicstripBundles.Add(b);
            }
            catch (Exception) { throw new AddException("comicstrip bundle"); }
        }

        /// <summary> 
        /// Get a ComicstripBundle by ID 
        /// </summary>
        public ComicstripBundle Get(int id)
        {
            return uow.ComicstripBundles.GetByID(id);
        }

        /// <summary> 
        /// Get list of all ComicstripBundles 
        /// </summary>
        public List<ComicstripBundle> GetAll()
        {
            return uow.ComicstripBundles.GetAll();
        }

        /// <summary> 
        /// Delete ComicstripBundle by ID 
        /// </summary>
        public void Delete(int id)
        {
            uow.ComicstripBundles.Delete(id);
        }

        /// <summary> 
        /// Delete all ComicstripBundle 
        /// </summary>
        public void DeleteAll()
        {
            uow.ComicstripBundles.DeleteAll();
        }

        /// <summary> 
        /// Update existing ComicstripBundle 
        /// </summary>
        public void Update(ComicstripBundle b)
        {
            if (uow.ComicstripBundles.Exist(b, /* Ignore Id Search */ true)) throw new ExistException("comicstrip bundle");
            try
            {
                uow.ComicstripBundles.Update(b);
            }
            catch (Exception) { throw new UpdateException("comicstrip bundle"); }
        }

        /// <summary> 
        /// Check if ComicstripBundle exist
        /// </summary>
        public bool Exist(ComicstripBundle b, bool ignoreId = false)
        {
            return uow.ComicstripBundles.Exist(b, ignoreId);
        }
    }
}
