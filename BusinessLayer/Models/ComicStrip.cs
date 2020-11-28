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
        public int ComicStripNumber { get; private set; }
        private List<Author> AuthorList { get;  set; }
        #endregion

        #region Constructor 
        public ComicStrip(string titel, string serie, int comicStripNumber)
        {
            SetTitel(titel);
            SetSerie(serie);
            
            SetComicStripNumber(comicStripNumber);
        }

        #endregion

        #region Methods 
        public void SetTitel(string newTitel)
        {
            if (string.IsNullOrEmpty(newTitel))
            {
                throw new ArgumentNullException();
            }
            Titel = newTitel;
        }
        public void AddAuthor(Author author)
        {
            if (AuthorList.Contains(author))
            {
                throw new ArgumentNullException();
            }
            AuthorList.Add(author);
        }
        public IReadOnlyCollection<Author> GetAllAuthors()
        {
            return AuthorList.AsReadOnly();
        }
        public void SetSerie(string newSerie)
        {
            if (string.IsNullOrEmpty(newSerie))
            {
                throw new ArgumentNullException();
            }
            Serie = newSerie;
        }
        
        public void SetComicStripNumber(int newComicStripNumber)
        {
            if (newComicStripNumber.Equals(null))
            {
                throw new ArgumentNullException();
            }
            ComicStripNumber = newComicStripNumber;
        }
        public void SetID(int id)
        {
            if (id.Equals(null))
            {
                throw new ArgumentNullException();
            }
            ID = id;
        }
        #endregion
    }
}
