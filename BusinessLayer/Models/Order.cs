using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class Order
    {
        #region Attributes
        public int ID { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public virtual List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public DateTime Date;
        #endregion

        #region Constructor 
        public Order(int id, string firstname, string lastname, string email, string phone, DateTime date, List<OrderItem> items)
        {
            this.ID = id;
            SetFirstname(firstname);
            SetLastname(lastname);
            SetEmail(email);
            SetPhone(phone);
            SetItems(items);
            SetDate(date);
        }
        public Order(string firstname, string lastname, string email, string phone, DateTime date, List<OrderItem> items)
        {
            SetFirstname(firstname);
            SetLastname(lastname);
            SetEmail(email);
            SetPhone(phone);
            SetItems(items);
            SetDate(date);
        }
        #endregion

        #region Methods 
        public void SetFirstname(string firstname)
        {
            this.Firstname = firstname;
        }
        public void SetLastname(string lastname)
        {
            this.Lastname = lastname;
        }
        public void SetEmail(string email)
        {
            this.Email = email;
        }
        public void SetPhone(string phone)
        {
            this.Phone = phone;
        }
        public void SetItems(List<OrderItem> orderItems)
        {
            if (orderItems.Count <= 0) throw new InvalidItemListException();
            this.Items = orderItems;
        }
        public void AddItem(OrderItem orderItem)
        {
            if (orderItem == null) throw new InvalidItemException();
            if (this.Items.Contains(orderItem)) throw new ItemAlreadyPresentException();
            this.Items.Add(orderItem);
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
            Delivery d = (Delivery)obj;
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
            public InvalidDateExcetion() : base(String.Format("The order date cannot be empty")) { }
        }
        public class InvalidItemListException : Exception
        {
            public InvalidItemListException() : base(String.Format("The order must have at least one item")) { }
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
