using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class AuthorManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the authors
        /// </summary>
        public AuthorManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Author 
        /// </summary>
        public Author Add(Author a)
        {
            if (uow.Authors.Exist(a)) throw new ExistException("author");
            try
            {
                return uow.Authors.Add(a);
            }
            catch (Exception) { throw new AddException("author"); }
        }

        /// <summary> 
        /// Get an Author by Id
        /// </summary>
        public Author Get(int id)
        {
            return uow.Authors.GetByID(id);
        }

        /// <summary> 
        /// Get an Author by Name
        /// </summary>
        public Author GetByName(string firstname, string lastname)
        {
            return uow.Authors.GetByName(firstname, lastname);
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
            if (uow.Authors.HasStrips(id)) throw new DeleteConnectionException("author", "comicstrip");
            uow.Authors.Delete(id);
        }

        /// <summary> 
        /// Delete all publishers
        /// </summary>
        public void DeleteAll()
        {
            uow.Authors.DeleteAll();
        }

        /// <summary> 
        /// Update existing Author 
        /// </summary>
        public void Update(Author a)
        {

            if (uow.Authors.Exist(a, true)) throw new ExistException("author");
            try
            {
                uow.Authors.Update(a);
            }
            catch (Exception) { throw new AddException("author"); }
        }

        /// <summary> 
        /// Check if Author exist
        /// </summary>
        public bool Exist(Author a, bool ignoreId = false)
        {
            return uow.Authors.Exist(a, ignoreId);
        }

        /// <summary> 
        /// Check if Author is included at Comicstrips
        /// </summary>
        public bool HasStrips(Author a)
        {
            return uow.Authors.HasStrips(a.ID);
        }
    }
}
