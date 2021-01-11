using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IAuthorRepository
    {
        public Author Add(Author a);
        public Author GetByID(int id);
        public Author GetByName(string firstname, string lastname);
        public List<Author> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Author a);
        public bool Exist(Author a, bool ignoreId = false);
        public bool HasStrips(int id);
    }
}