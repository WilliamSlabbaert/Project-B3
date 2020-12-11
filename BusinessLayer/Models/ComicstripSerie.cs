using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ComicstripSerie
    {
        #region Attributes
        public int ID { get; private set; }
        public String Name { get; private set; }
        #endregion

        #region Constructor 
        public ComicstripSerie(int id, string name)
        {
            this.ID = id;
            this.SetName(name);
        }
        public ComicstripSerie(string name)
        {
            this.SetName(name);
        }
        #endregion

        #region Methods 
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new InvalidNameException();
            this.Name = name;
        }
        #endregion

        #region Exceptions
        public class InvalidNameException : Exception
        {
            public InvalidNameException() : base(String.Format("The serie name cannot be empty")) { }
        }
        #endregion
    }
}
