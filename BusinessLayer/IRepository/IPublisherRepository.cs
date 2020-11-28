using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IPublisherRepository
    {
        public List<Publisher> GetAll();
        public Publisher GetByID(int id);
        public void DeleteByID(int id);
        public void Add(Publisher publisher);
        public void DeleteAll();
    }
}
