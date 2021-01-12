using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class Delivery
    {
        #region Attributes
        public int ID { get; private set; }
        public string Supplier { get; private set; }
        public virtual List<DeliveryItem> Items { get; private set; } = new List<DeliveryItem>();
        public DateTime Date;
        #endregion

        #region Constructor 
        public Delivery(int id, string supplier, DateTime date, List<DeliveryItem> items)
        {
            this.ID = id;
            SetSupplier(supplier);
            SetItems(items);
            SetDate(date);
        }
        public Delivery(string supplier, DateTime date, List<DeliveryItem> items)
        {
            SetSupplier(supplier);
            SetItems(items);
            SetDate(date);
        }
        #endregion

        #region Methods 
        public void SetSupplier(string supplier)
        {
            this.Supplier = supplier;
        }
        public void SetItems(List<DeliveryItem> deliveryItems)
        {
            if (deliveryItems.Count <= 0) throw new InvalidItemListException();
            this.Items = deliveryItems;
        }
        public void AddItem(DeliveryItem deliveryItem)
        {
            if (deliveryItem == null) throw new InvalidItemException();
            if (this.Items.Contains(deliveryItem)) throw new ItemAlreadyPresentException();
            this.Items.Add(deliveryItem);
        }
        public void SetDate(DateTime date)
        {
            if (date == null) throw new InvalidDateExcetion();
            this.Date = date;
        }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            Delivery d = (Delivery) obj;
            if (this.ID == d.ID) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID);
        }
        #endregion

        #region Exceptions
        public class InvalidDateExcetion : Exception
        {
            public InvalidDateExcetion() : base(String.Format("The delivery date cannot be empty")) { }
        }
        public class InvalidItemListException : Exception
        {
            public InvalidItemListException() : base(String.Format("The delivery must have at least one item")) { }
        }
        public class InvalidItemException : Exception
        {
            public InvalidItemException() : base(String.Format("The item cannot be empty")) { }
        }
        public class ItemAlreadyPresentException : Exception
        {
            public ItemAlreadyPresentException() : base(String.Format("You cannot add twice the same item")) { }
        }
        #endregion
    }
}
