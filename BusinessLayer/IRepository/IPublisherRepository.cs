using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IPublisherRepository
    {
        public Publisher Add(Publisher p);
        public Publisher GetByID(int id);
        public List<Publisher> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Publisher p);
        public bool Exist(Publisher p, bool ignoreId = false);
    }
}