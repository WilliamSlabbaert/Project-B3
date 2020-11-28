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
        public Publisher(string name)
        {
            SetName(name);
        }
        #endregion

        #region Methods 
        public void SetName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException();
            }
            Name = newName;
        }
        public void SetID(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            ID = id;
        }
        #endregion

    }
}
