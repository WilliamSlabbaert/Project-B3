using BusinessLayer;
using BusinessLayer.DomainManagers;
using BusinessLayer.Models;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B3_Test
{
    [TestClass]
    public class ComicStripManager_Tests
    {
        [TestMethod]
        public void AddComic() 
        {
            var manager = new ComicStripManager(new UnitOfWork("development"));
            manager.DeleteAll();
            var publisherManager = new PublisherManager(new UnitOfWork("development"));
            publisherManager.DeleteAll();
            publisherManager.Add(new Publisher("test-publisher"));

            var AuthorManager = new AuthorManager(new UnitOfWork("development"));
            AuthorManager.DeleteAll();
            AuthorManager.Add(new Author("test-author", "test-author"));

            manager.Add(new BusinessLayer.Models.ComicStrip("test-strip", new ComicstripSerie("testSerie"), 5, AuthorManager.GetAll(), publisherManager.GetAll()[0]));
            var comic = manager.GetAll();
            Assert.AreEqual(comic[0].Titel, "test-strip");
            Assert.AreEqual(comic[0].Number, 5);
            publisherManager.DeleteAll();
            AuthorManager.DeleteAll();
            manager.DeleteAll();
        }
        [TestMethod]
        public void GetByID()
        {
            var manager = new ComicStripManager(new UnitOfWork("development"));
            manager.DeleteAll();
            var publisherManager = new PublisherManager(new UnitOfWork("development"));
            publisherManager.DeleteAll();
            publisherManager.Add(new Publisher("test-publisher"));

            var AuthorManager = new AuthorManager(new UnitOfWork("development"));
            AuthorManager.DeleteAll();
            AuthorManager.Add(new Author("test-author", "test-author"));

            manager.Add(new BusinessLayer.Models.ComicStrip("test-strip", new ComicstripSerie("testSerie"), 5, AuthorManager.GetAll(), publisherManager.GetAll()[0]));
            var comic = manager.GetAll();
            Assert.AreEqual(comic.Count, 1);
            var comc = manager.Get(comic[0].ID);

            Assert.AreEqual(comc.Titel, "test-strip");
            Assert.AreEqual(comc.Number, 5);
            Assert.AreEqual(comc.Serie, null); 

            publisherManager.DeleteAll();
            AuthorManager.DeleteAll();
            manager.DeleteAll();
        }
        [TestMethod]
        public void DeleteByID()
        {
            var manager = new ComicStripManager(new UnitOfWork("development"));
            manager.DeleteAll();
            var publisherManager = new PublisherManager(new UnitOfWork("development"));
            publisherManager.DeleteAll();
            publisherManager.Add(new Publisher("test-publisher"));

            var AuthorManager = new AuthorManager(new UnitOfWork("development"));
            AuthorManager.DeleteAll();
            AuthorManager.Add(new Author("test-author", "test-author"));

            manager.Add(new BusinessLayer.Models.ComicStrip("test-strip", new ComicstripSerie("testSerie"), 5, AuthorManager.GetAll(), publisherManager.GetAll()[0]));
            var comic = manager.GetAll();
            Assert.AreEqual(comic.Count, 1);
            var comc = manager.Get(comic[0].ID);

            Assert.AreEqual(comc.Titel, "test-strip");
            Assert.AreEqual(comc.Number, 5);
            manager.Delete(comc.ID);
            comic = manager.GetAll();
            Assert.AreEqual(comic.Count, 0);

            publisherManager.DeleteAll();
            AuthorManager.DeleteAll();
            manager.DeleteAll();
        }
        [TestMethod]
        public void Update()
        {
            var manager = new ComicStripManager(new UnitOfWork("development"));
            manager.DeleteAll();
            var publisherManager = new PublisherManager(new UnitOfWork("development"));
            publisherManager.DeleteAll();
            publisherManager.Add(new Publisher("test-publisher"));

            var AuthorManager = new AuthorManager(new UnitOfWork("development"));
            AuthorManager.DeleteAll();
            AuthorManager.Add(new Author("test-author", "test-author"));

            manager.Add(new BusinessLayer.Models.ComicStrip("test-strip", new ComicstripSerie("testSerie"), 5, AuthorManager.GetAll(), publisherManager.GetAll()[0]));
            var comic = manager.GetAll();
            Assert.AreEqual(comic.Count, 1);
            var comc = manager.Get(comic[0].ID);

            Assert.AreEqual(comc.Titel, "test-strip");
            Assert.AreEqual(comc.Number, 5);
            comc.SetTitel("test");
            comc.SetNumber(10);
            //manager.Update(comc);

            comic = manager.GetAll();
            Assert.AreEqual(comic.Count, 1);
            comc = manager.Get(comic[0].ID);

            Assert.AreEqual(comc.Titel, "test-strip");
            Assert.AreEqual(comc.Number, 5);

            publisherManager.DeleteAll();
            AuthorManager.DeleteAll();
            manager.DeleteAll();
        }
    }
}
