using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B3_Test
{
    [TestClass]
    public class AuthorManager_Tests
    {
        [TestMethod]
        public void AddAuthor()
        {
            var manager = new AuthorManager(new UnitOfWork("development"));
            manager.DeleteAll();
            manager.Add(new BusinessLayer.Models.Author("test-first", "test-Last"));
            var authors = manager.GetAll();

            Assert.AreEqual(authors.Count, 1);
            Assert.AreEqual(authors[0].Firstname, "test-first");
            Assert.AreEqual(authors[0].Surname, "test-Last");
            manager.DeleteAll();

        }
        [TestMethod]
        public void GetById()
        {
            var manager = new AuthorManager(new UnitOfWork("development"));
            manager.DeleteAll();
            manager.Add(new BusinessLayer.Models.Author("test-first", "test-Last"));
            var authors = manager.GetAll();

            Assert.AreEqual(authors.Count, 1);
            var author = manager.Get(authors[0].ID);
            Assert.AreEqual(author.Firstname, "test-first");
            Assert.AreEqual(author.Surname, "test-Last");
            manager.DeleteAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            var manager = new AuthorManager(new UnitOfWork("development"));
            manager.DeleteAll();
            manager.Add(new BusinessLayer.Models.Author("test-first", "test-Last"));
            var authors = manager.GetAll();

            Assert.AreEqual(authors.Count, 1);
            var author = manager.Get(authors[0].ID);
            Assert.AreEqual(author.Firstname, "test-first");
            Assert.AreEqual(author.Surname, "test-Last");
            manager.Delete(author.ID);
            authors = manager.GetAll();
            Assert.AreEqual(authors.Count, 0);
            manager.DeleteAll();
        }
        [TestMethod]
        public void Update()
        {
            var manager = new AuthorManager(new UnitOfWork("development"));
            manager.DeleteAll();
            manager.Add(new BusinessLayer.Models.Author("test-first", "test-Last"));
            var authors = manager.GetAll();

            Assert.AreEqual(authors.Count, 1);
            var author = manager.Get(authors[0].ID);
            Assert.AreEqual(author.Firstname, "test-first");
            Assert.AreEqual(author.Surname, "test-Last");
            author.SetSurname("test-Sur");
            author.SetFirstName("test-First");
            manager.Update(author);

            authors = manager.GetAll();

            Assert.AreEqual(authors.Count, 1);
            author = manager.Get(authors[0].ID);
            Assert.AreEqual(author.Firstname, "test-First");
            Assert.AreEqual(author.Surname, "test-Sur");

            manager.DeleteAll();
        }
    }
}
