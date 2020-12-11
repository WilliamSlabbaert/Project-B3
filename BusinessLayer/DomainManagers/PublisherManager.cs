using BusinessLayer.Models;
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
        public Publisher Add(Publisher publisher)
        {
            return uow.Publishers.Add(publisher);
        }

        /// <summary> 
        /// Get a publisher by ID 
        /// </summary>
        public Publisher Get(int id)
        {
            return uow.Publishers.GetByID(id);
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
            uow.Publishers.Update(p);
        }

        /// <summary> 
        /// Check if Publisher exist
        /// </summary>
        public bool Exist(Publisher p)
        {
            return uow.Publishers.Exist(p);
        }
    }
}