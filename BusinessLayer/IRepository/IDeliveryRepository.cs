using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IRepository
{
    public interface IDeliveryRepository
    {
        public Delivery Add(Delivery d);
        public Delivery GetByID(int id);
        public List<Delivery> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Delivery d);
        public bool Exist(Delivery d, bool ignoreId = false);
    }
}
