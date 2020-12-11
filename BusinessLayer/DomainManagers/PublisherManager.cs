using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class PublisherManager
    {
        private readonly IUnitOfWork uow;

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
        public Publisher Get(int ID)
        {
            return uow.Publishers.GetByID(ID);
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
    }
}
