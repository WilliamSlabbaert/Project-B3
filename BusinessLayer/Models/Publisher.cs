using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Publisher
    {
        #region Attributes
        public int ID { get; private set; }
        public string Name { get; private set; }
        #endregion

        #region Constructor 
        public Publisher(int id, string name)
        {
            this.ID = id;
            SetName(name);
        }
        public Publisher(string name)
        {
            SetName(name);
        }
        #endregion

        #region Methods 
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidNameExcetion();
            this.Name = name;
        }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            Publisher p = (Publisher)obj;
            if (this.ID == p.ID) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID, this.Name);
        }
        #endregion

        #region Exceptions
        public class InvalidNameExcetion : Exception
        {
            public InvalidNameExcetion() : base(String.Format("The publishers name cannot be empty")) { }
        }
        #endregion
    }
}
