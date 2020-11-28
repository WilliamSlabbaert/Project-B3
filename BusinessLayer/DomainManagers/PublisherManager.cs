using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class PublisherManager
    {
        private IUnitOfWork uow;

        public PublisherManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(Publisher publisher)
        {
            uow.publisherRepository.Add(publisher);
        }
        public void DeleteAll()
        {
            uow.publisherRepository.DeleteAll();
        }
        public List<Publisher> GetAll()
        {
            return uow.publisherRepository.GetAll();
        }
        public void DeleteById(int ID)
        {
            uow.publisherRepository.DeleteByID(ID);
        }
        public Publisher GetById(int ID)
        {
            return uow.publisherRepository.GetByID(ID);
        }
    }
}
