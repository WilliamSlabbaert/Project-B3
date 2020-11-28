using System;

namespace BusinessLayer
{
    public class Author
    {
        #region Attributes
        public int ID { get; private set; }
        public string Firstname { get; private set; }
        public string Surname { get; private set;  }
        #endregion

        #region Constructor 
        public Author(string firstname, string surname)
        {
            SetFirstName(firstname);
            SetSurname(surname);
        }
        #endregion

        #region Methods 
        public void SetFirstName(string newFirstName)
        {
            if (string.IsNullOrEmpty(newFirstName))
            {
                throw new ArgumentNullException();
            }
            Firstname = newFirstName;
        }
        public void SetSurname(string newSurname)
        {
            if (string.IsNullOrEmpty(newSurname))
            {
                throw new ArgumentNullException();
            }
            Surname = newSurname;
        }
        public void SetID(int id)
        {
            if (id == null)
            {
                throw new ArgumentException();
            }
            ID = id;
        }
        #endregion
    }
}
