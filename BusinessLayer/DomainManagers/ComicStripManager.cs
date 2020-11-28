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
        public void AddStrip(ComicStrip comicStrip)
        {
            uow.comisStripRepository.Add(comicStrip);
        }
        public void DeleteAll()
        {
            uow.comisStripRepository.DeleteAll();
        }
        public List<ComicStrip> GetAll()
        {
            return uow.comisStripRepository.GetAll();
        }
        public void DeleteById(int ID)
        {
            uow.comisStripRepository.DeleteByID(ID);
        }
        public ComicStrip GetById(int ID)
        {
            return uow.comisStripRepository.GetByID(ID);
        }
    }
}
