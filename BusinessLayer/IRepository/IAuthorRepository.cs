using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IAuthorRepository
    {
        public void Add(Author DataAuthor);
        public Author GetByID(int ID);
        public List<Author> GetAll();
        public void DeleteByID(int ID);
        public void DeleteAll();
    }
}
