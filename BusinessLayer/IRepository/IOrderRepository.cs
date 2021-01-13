using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IRepository
{
    public interface IOrderRepository
    {
        public Order Add(Order o);
        public Order GetByID(int id);
        public List<Order> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Order o);
        public bool Exist(Order o, bool ignoreId = false);
    }
}
