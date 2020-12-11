using System;

namespace BusinessLayer.Models
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

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            Author a = (Author)obj;
            if (this.Firstname == a.Firstname && this.Surname == a.Surname) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID, this.Firstname, this.Surname);
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.Firstname, this.Surname);
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
