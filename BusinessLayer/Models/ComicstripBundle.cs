using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{

    public class ComicstripBundle
    {
        #region Attributes
        public int ID { get; private set; }
        public String Titel { get; private set; }
        public virtual List<ComicStrip> Comicstrips { get; private set; }
        public virtual Publisher Publisher { get; private set; }
        #endregion

        #region Constructor s
        public ComicstripBundle(int id, string titel, List<ComicStrip> comicstrips, Publisher publisher)
        {
            this.ID = id;
            this.SetTitel(titel);
            this.SetStrips(comicstrips);
            this.SetPublisher(publisher);
        }
        public ComicstripBundle(string titel, List<ComicStrip> comicstrips, Publisher publisher)
        {
            this.SetTitel(titel);
            this.SetStrips(comicstrips);
            this.SetPublisher(publisher);
        }
        #endregion

        #region Methods 
        public void SetTitel(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new NullTitleException();
            if (title.Length > 255) throw new ToLongTitleException();
            this.Titel = title;
        }
        public void SetStrips(List<ComicStrip> comicstrips)
        {
            if (comicstrips.Count <= 0) throw new InvalidComicstripListException();
            this.Comicstrips = comicstrips;
        }
        public void AddStrip(ComicStrip comicstrip)
        {
            if (comicstrip == null) throw new InvalidComicstripException();
            if (this.Comicstrips.Contains(comicstrip)) throw new ComicstripAlreadyPresentException();
            this.Comicstrips.Add(comicstrip);
        }
        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null) throw new InvalidPublisherException();
            this.Publisher = publisher;
        }
        #endregion

        #region Exceptions
        public class NullTitleException : Exception
        {
            public NullTitleException() : base(String.Format("The strips title cannot be empty")) { }
        }
        public class ToLongTitleException : Exception
        {
            public ToLongTitleException() : base(String.Format("The strips title cannot be longer than 255")) { }
        }
        public class NullSerieException : Exception
        {
            public NullSerieException() : base(String.Format("The strips serie cannot be empty")) { }
        }
        public class InvalidComicstripListException : Exception
        {
            public InvalidComicstripListException() : base(String.Format("The bundle must have at least one comicstrip")) { }
        }
        public class InvalidComicstripException : Exception
        {
            public InvalidComicstripException() : base(String.Format("The strip cannot be empty")) { }
        }
        public class ComicstripAlreadyPresentException : Exception
        {
            public ComicstripAlreadyPresentException() : base(String.Format("You cannot add twice the same comicstrip")) { }
        }
        public class InvalidPublisherException : Exception
        {
            public InvalidPublisherException() : base(String.Format("The authors publishers cannot be empty")) { }
        }
        #endregion
    }
}
