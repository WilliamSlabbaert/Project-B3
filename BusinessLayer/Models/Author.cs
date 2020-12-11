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
        public Author(int id, string firstname, string surname)
        {
            this.ID = id;
            this.SetFirstName(firstname);
            this.SetSurname(surname);
        }
        public Author(string firstname, string surname)
        {
            this.SetFirstName(firstname);
            this.SetSurname(surname);
            
        }
        #endregion

        #region Methods 
        public void SetFirstName(string newFirstName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
                throw new InvalidFirstnameException();
            this.Firstname = newFirstName;
        }
        public void SetSurname(string newSurname)
        {
            if (string.IsNullOrWhiteSpace(newSurname))
                throw new InvalidSurnameException();
            this.Surname = newSurname;
        }
        
        #endregion

        #region Exceptions
        public class InvalidFirstnameException : Exception
        {
            public InvalidFirstnameException() : base(String.Format("The authors firstname cannot be empty")) { }
        }

        public class InvalidSurnameException : Exception
        {
            public InvalidSurnameException() : base(String.Format("The authors surname cannot be empty")) { }
        }
        #endregion
    }
}
