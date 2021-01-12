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
    public class ComicStripBundleManager_Tests
    {
        [TestMethod]
        public void Add()
        {
            var manager = new ComicStripManager(new UnitOfWork("development"));
            manager.DeleteAll();
            var publisherManager = new PublisherManager(new UnitOfWork("development"));
            publisherManager.DeleteAll();
            publisherManager.Add(new Publisher("test-publisher"));

            var AuthorManager = new AuthorManager(new UnitOfWork("development"));
             
            AuthorManager.Add(new Author("test-author", "test-author"));

            manager.Add(new BusinessLayer.Models.ComicStrip("test-strip", new ComicstripSerie("testSerie"), 5, AuthorManager.GetAll(), publisherManager.GetAll()[0]));
            var comic = manager.GetAll();

            var bundleManager = new ComicstripBundleManager(new UnitOfWork("development"));
            bundleManager.DeleteAll();
            bundleManager.Add(new ComicstripBundle("test-bundle",comic, publisherManager.GetAll()[0]));
            var bundles = bundleManager.GetAll();
            Assert.AreEqual(bundles[0].Titel, "test-bundle");
            Assert.AreEqual(bundles[0].Comicstrips.Count ,1);
            manager.DeleteAll();
            AuthorManager.DeleteAll();
            bundleManager.DeleteAll();
        }
        [TestMethod]
        public void Delete()
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

            var bundleManager = new ComicstripBundleManager(new UnitOfWork("development"));
            bundleManager.DeleteAll();
            bundleManager.Add(new ComicstripBundle("test-bundle", comic, publisherManager.GetAll()[0]));
            var bundles = bundleManager.GetAll();
            Assert.AreEqual(bundles.Count, 1);
            Assert.AreEqual(bundles[0].Titel, "test-bundle");
            Assert.AreEqual(bundles[0].Comicstrips.Count, 1);


            bundleManager.Delete(bundles[0].ID);
            bundles = bundleManager.GetAll();
            Assert.AreEqual(bundles.Count, 0);

            manager.DeleteAll();
            AuthorManager.DeleteAll();
            bundleManager.DeleteAll();
        }
        [TestMethod]
        public void Get()
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

            var bundleManager = new ComicstripBundleManager(new UnitOfWork("development"));
            bundleManager.DeleteAll();
            bundleManager.Add(new ComicstripBundle("test-bundle", comic, publisherManager.GetAll()[0]));
            var bundles = bundleManager.GetAll();
            var bundle = bundleManager.Get(bundles[0].ID);

            Assert.AreEqual(bundles.Count, 1);
            Assert.AreEqual(bundle.Titel, "test-bundle");
            Assert.AreEqual(bundle.Comicstrips.Count, 1);

            manager.DeleteAll();
            AuthorManager.DeleteAll();
            bundleManager.DeleteAll();
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

            var bundleManager = new ComicstripBundleManager(new UnitOfWork("development"));
            bundleManager.DeleteAll();
            bundleManager.Add(new ComicstripBundle("test-bundle", comic, publisherManager.GetAll()[0]));
            var bundles = bundleManager.GetAll();
            var bundle = bundleManager.Get(bundles[0].ID);

            Assert.AreEqual(bundles.Count, 1);
            Assert.AreEqual(bundle.Titel, "test-bundle");
            Assert.AreEqual(bundle.Comicstrips.Count, 1);

            bundle.SetTitel("test");
            bundles = bundleManager.GetAll();

            Assert.AreEqual(bundles.Count, 1);
            Assert.AreEqual(bundle.Titel, "test");
            Assert.AreEqual(bundle.Comicstrips.Count, 1);

            manager.DeleteAll();
            AuthorManager.DeleteAll();
            bundleManager.DeleteAll();
        }
    }
}
