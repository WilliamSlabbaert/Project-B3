using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class DeliveryItem
    {
        #region Attributes
        public int ID { get; private set; }
        public ComicStrip Comicstrip { get; private set; }
        public int Quantity { get; private set; }
        #endregion

        #region Constructor 
        public DeliveryItem(int id, ComicStrip comicstrip, int quantity)
        {
            this.ID = id;
            this.SetStrip(comicstrip);
            this.SetQuantity(quantity);
        }
        public DeliveryItem(ComicStrip comicstrip, int quantity)
        {
            this.SetStrip(comicstrip);
            this.SetQuantity(quantity);
        }
        #endregion

        #region Methods 
        public void SetStrip(ComicStrip comicstrip)
        {
            if (comicstrip == null) throw new InvalidStripException();
            this.Comicstrip = comicstrip;
        }
        public void SetQuantity(int quantity)
        {
            if (quantity == 0) throw new InvalidQuantityException();
            this.Quantity = quantity;
        }
        #endregion

        #region Exceptions
        public class InvalidStripException : Exception
        {
            public InvalidStripException() : base(String.Format("The delivery item strip cannot be empty")) { }
        }
        public class InvalidQuantityException : Exception
        {
            public InvalidQuantityException() : base(String.Format("The delivery item quantity cannot be 0")) { }
        }
        #endregion
    }
}
