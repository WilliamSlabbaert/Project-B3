using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class AuthorManager
    {
        private readonly IUnitOfWork uow;

        public AuthorManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Author 
        /// </summary>
        public Author Add(Author author)
        {
           return uow.Authors.Add(author);
        } 

        public Author GetByID(int ID)
        {
            return uow.Authors.GetByID(ID);
        }

        /// <summary> 
        /// Get list of all authors 
        /// </summary>
        public List<Author> GetAll()
        {
            return uow.Authors.GetAll();
        }

        /// <summary> 
        /// Delete publisher by ID 
        /// </summary>
        public void Delete(int id)
        {
            uow.Authors.Delete(id);
        }

        /// <summary> 
        /// Delete all publishers
        /// </summary>
        public void DeleteAll()
        {
            uow.Authors.DeleteAll();
        }
    }
}
