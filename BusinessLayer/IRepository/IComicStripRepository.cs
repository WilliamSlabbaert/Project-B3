using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IComicStripRepository
    {
        public ComicStrip Add(ComicStrip strip);
        public ComicStrip GetByID(int ID);
        public List<ComicStrip> GetAll();
        public void DeleteByID(int ID);
        public void DeleteAll();
    }
}
