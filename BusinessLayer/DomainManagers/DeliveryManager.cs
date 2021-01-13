using BusinessLayer.Models;
using BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DomainManagers
{
    public class DeliveryManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Deliveries
        /// </summary>
        public DeliveryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Delivery 
        /// </summary>
        public Delivery Add(Delivery d)
        {
            try
            {
                return uow.Deliveries.Add(d);
            }
            catch (Exception) { throw new AddException("delivery"); }
        }

        /// <summary> 
        /// Get an Delivery by Id
        /// </summary>
        public Delivery Get(int id)
        {
            return uow.Deliveries.GetByID(id);
        }

        /// <summary> 
        /// Get list of all Deliveries 
        /// </summary>
        public List<Delivery> GetAll()
        {
            return uow.Deliveries.GetAll();
        }

        /// <summary> 
        /// Delete Delivery by ID 
        /// </summary>
        public void Delete(int id)
        {
            uow.Deliveries.Delete(id);
        }

        /// <summary> 
        /// Delete all Deliveries
        /// </summary>
        public void DeleteAll()
        {
            uow.Deliveries.DeleteAll();
        }

        /// <summary> 
        /// Update existing Author 
        /// </summary>
        public void Update(Delivery d)
        {

            if (!uow.Deliveries.Exist(d, true)) throw new ExistException("delivery");
            try
            {
                uow.Deliveries.Update(d);
            }
            catch (Exception) { throw new AddException("delivery"); }
        }

        /// <summary> 
        /// Check if Delivery exist
        /// </summary>
        public bool Exist(Delivery d, bool ignoreId = false)
        {
            return uow.Deliveries.Exist(d, ignoreId);
        }
    }
}
