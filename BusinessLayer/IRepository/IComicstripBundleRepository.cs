using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IComicstripBundleRepository
    {
        public ComicstripBundle Add(ComicstripBundle b);
        public ComicstripBundle GetByID(int id);
        public List<ComicstripBundle> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(ComicstripBundle b);
        public bool Exist(ComicstripBundle s, bool ignoreId = false);
    }
}