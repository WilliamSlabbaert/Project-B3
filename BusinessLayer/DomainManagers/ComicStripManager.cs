using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ComicStripManager
    {
        public IUnitOfWork uow { get; set; }

        public ComicStripManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new ComicStrip 
        /// </summary>
        public void Add(ComicStrip comicStrip)
        {
            uow.Comicstrips.Add(comicStrip);
        }

        public void DeleteAll()
        {
            uow.Comicstrips.DeleteAll();
        }
        public List<ComicStrip> GetAll()
        {
            return uow.Comicstrips.GetAll();
        }
        public void Delete(int ID)
        {
            uow.Comicstrips.DeleteByID(ID);
        }
        public ComicStrip GetById(int ID)
        {
            return uow.Comicstrips.GetByID(ID);
        }
    }
}
