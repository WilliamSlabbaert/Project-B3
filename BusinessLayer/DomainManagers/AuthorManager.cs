using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class AuthorManager
    {
        public IUnitOfWork uow;

        public AuthorManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(Author author)
        {
            uow.authorRepository.Add(author);
        }
        public void DeleteAll()
        {
            uow.authorRepository.DeleteAll();
        }
        public Author GetByID(int ID)
        {
            return uow.authorRepository.GetByID(ID);
        }
        public List<Author> GetAll()
        {
            return uow.authorRepository.GetAll();
        }
        public void DeleteByID(int ID)
        {
            uow.authorRepository.DeleteByID(ID);
        }
    }
}
