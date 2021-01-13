using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DomainManagers
{
    public class OrderManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Orders
        /// </summary>
        public OrderManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Order 
        /// </summary>
        public Order Add(Order o)
        {
            try
            {
                return uow.Orders.Add(o);
            }
            catch (Exception) { throw new AddException("order"); }
        }

        /// <summary> 
        /// Get an Order by Id
        /// </summary>
        public Order Get(int id)
        {
            return uow.Orders.GetByID(id);
        }

        /// <summary> 
        /// Get list of all Orders 
        /// </summary>
        public List<Order> GetAll()
        {
            return uow.Orders.GetAll();
        }

        /// <summary> 
        /// Delete Order by ID 
        /// </summary>
        public void Delete(int id)
        {
            uow.Orders.Delete(id);
        }

        /// <summary> 
        /// Delete all Orders
        /// </summary>
        public void DeleteAll()
        {
            uow.Orders.DeleteAll();
        }

        /// <summary> 
        /// Update existing Order 
        /// </summary>
        public void Update(Order o)
        {

            if (!uow.Orders.Exist(o, true)) throw new ExistException("order");
            try
            {
                uow.Orders.Update(o);
            }
            catch (Exception) { throw new AddException("order"); }
        }

        /// <summary> 
        /// Check if Orders exist
        /// </summary>
        public bool Exist(Order o, bool ignoreId = false)
        {
            return uow.Orders.Exist(o, ignoreId);
        }
    }
}
