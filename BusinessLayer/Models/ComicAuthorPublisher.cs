using System;

namespace BusinessLayer
{
    public class ComicAuthorPublisher
    {
        #region Attributes
        public ComicStrip Comic { get; private set; }
        public Author Author { get; private set; }
        public Publisher Publisher { get; private set; }
        #endregion

        #region Constructor 
        public ComicAuthorPublisher(ComicStrip comic_ID, Author author_ID, Publisher publisher_ID)
        {
            SetAuthor(author_ID);
            SetPublisher(publisher_ID);
            SetComic(comic_ID);
        }
        public void SetAuthor(Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException();
            }
            this.Author = author;
        }
        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException();
            }
            this.Publisher = publisher;
        }
        public void SetComic(ComicStrip comic)
        {
            if (comic == null)
            {
                throw new ArgumentNullException();
            }
            this.Comic = comic;
        }
        #endregion

        #region Methods 
        //todo
        #endregion
    }
}
