using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IPublisherRepository
    {
        public Publisher Add(Publisher p);
        public List<Publisher> GetAll();
        public Publisher GetByID(int id);
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Publisher p);
    }
}
