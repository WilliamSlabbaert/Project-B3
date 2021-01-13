using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class PublisherManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the publishers
        /// </summary>
        public PublisherManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Publisher 
        /// </summary>
        public Publisher Add(Publisher p)
        {
            if (uow.Publishers.Exist(p, true)) throw new ExistException("publisher");
            try
            {
                 return uow.Publishers.Add(p);
            }
            catch (Exception) { throw new AddException("publisher"); }
        }

        /// <summary> 
        /// Get a publisher by ID 
        /// </summary>
        public Publisher Get(int id)
        {
            return uow.Publishers.GetByID(id);
        }

        /// <summary> 
        /// Get a publisher by Name 
        /// </summary>
        public Publisher GetByName(String name)
        {
            return uow.Publishers.GetByName(name);
        }

        /// <summary> 
        /// Get list of all publishers 
        /// </summary>
        public List<Publisher> GetAll()
        {
            return uow.Publishers.GetAll();
        }

        /// <summary> 
        /// Delete publisher by ID 
        /// </summary>
        public void Delete(int id)
        {
            if (uow.Publishers.HasStrips(id)) throw new DeleteConnectionException("publisher", "comicstrip");
            uow.Publishers.Delete(id);
        }

        /// <summary> 
        /// Delete all Publishers 
        /// </summary>
        public void DeleteAll()
        {
            uow.Publishers.DeleteAll();
        }

        /// <summary> 
        /// Update existing Publisher 
        /// </summary>
        public void Update(Publisher p)
        {

            if (uow.Publishers.Exist(p, true)) throw new ExistException("publisher");
            try
            {
                uow.Publishers.Update(p);
            }
            catch (Exception) { throw new AddException("publisher"); }
        }

        /// <summary> 
        /// Check if Publisher exist
        /// </summary>
        public bool Exist(Publisher p, bool ignoreId = false)
        {
            return uow.Publishers.Exist(p, ignoreId);
        }

        /// <summary> 
        /// Check if Publisher is included at Comicstrips
        /// </summary>
        public bool HasStrips(Publisher p)
        {
            return uow.Publishers.HasStrips(p.ID);
        }
    }
}