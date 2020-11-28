using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IComicAuthorPublisherRepository
    {
        public void Add(ComicAuthorPublisher DataAuthor);
        public List<ComicAuthorPublisher> GetByComicID(int ID);
        public List<ComicAuthorPublisher> GetByPublisherID(int ID);
        public List<ComicAuthorPublisher> GetByAuthorID(int ID);
        public List<ComicAuthorPublisher> GetAll();
        public void DeleteByPublisherID(int ID);
        public void DeleteByComicID(int ID);
        public void DeleteByAuthorID(int ID);
        public void DeleteAll();
    }
}
