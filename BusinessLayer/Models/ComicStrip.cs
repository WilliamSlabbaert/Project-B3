using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    //test
    public class ComicStrip
    {
        #region Attributes
        public int ID { get; private set; }
        public String Titel { get; private set; }
        public string Serie { get; private set; }
        public int Number { get; private set; }
        public virtual List<Author> Authors { get; private set; }
        public virtual Publisher Publisher { get; private set; }
        #endregion

        #region Constructor 
        public ComicStrip(int id, string titel, string serie, int number, List<Author> authors, Publisher publisher)
        {
            this.ID = id;
            this.SetTitel(titel);
            this.SetSerie(serie);
            this.SetNumber(number);
            this.SetAuthors(authors);
            this.SetPublisher(publisher);
        }
        public ComicStrip(string titel, string serie, int number, List<Author> authors, Publisher publisher)
        {
            this.SetTitel(titel);
            this.SetSerie(serie);
            this.SetNumber(number);
            this.SetAuthors(authors);
            this.SetPublisher(publisher);
        }
        #endregion

        #region Methods 
        public void SetTitel(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new InvalidTitleException();
            this.Titel = title;
        }

        public void SetSerie(string serie)
        {
            if (string.IsNullOrWhiteSpace(serie)) throw new InvalidSerieException();
            this.Serie = serie;
        }
        
        public void SetNumber(int number)
        {
            this.Number = number;
        }

        public void SetAuthors(List<Author> authors)
        {
            if (authors.Count <= 0) throw new InvalidAuthorsListException();
            this.Authors = authors;
        }

        public void AddAuthor(Author author)
        {
            if (author == null) throw new InvalidAuthorException();
            if (this.Authors.Contains(author)) throw new AuthorAlreadyPresentException();
            this.Authors.Add(author);
        }

        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null) throw new InvalidPublisherException();
            this.Publisher = publisher;
        }
        #endregion

        #region Exceptions
        public class InvalidTitleException : Exception
        {
            public InvalidTitleException() : base(String.Format("The strips title cannot be empty")) { }
        }
        public class InvalidSerieException : Exception
        {
            public InvalidSerieException() : base(String.Format("The strips serie cannot be empty")) { }
        }
        public class InvalidAuthorsListException : Exception
        {
            public InvalidAuthorsListException() : base(String.Format("The strip must have at least one author")) { }
        }
        public class InvalidAuthorException : Exception
        {
            public InvalidAuthorException() : base(String.Format("The author cannot be empty")) { }
        }
        public class AuthorAlreadyPresentException : Exception
        {
            public AuthorAlreadyPresentException() : base(String.Format("You cannot add twice the same author")) { }
        }
        public class InvalidPublisherException : Exception
        {
            public InvalidPublisherException() : base(String.Format("The authors publishers cannot be empty")) { }
        }
        #endregion
    }
}
