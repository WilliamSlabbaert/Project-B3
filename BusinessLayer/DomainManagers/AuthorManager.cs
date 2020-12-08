using System;
using System.Collections.Generic;
using System.Text;

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

        public void DeleteAll()
        {
            uow.Authors.DeleteAll();
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

        public void DeleteByID(int ID)
        {
            uow.Authors.DeleteByID(ID);
        }
    }
}
