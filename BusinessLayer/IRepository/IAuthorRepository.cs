using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IAuthorRepository
    {
        public Author Add(Author DataAuthor);
        public Author GetByID(int id);
        public List<Author> GetAll();
        public void Delete(int id);
        public void DeleteAll();
    }
}