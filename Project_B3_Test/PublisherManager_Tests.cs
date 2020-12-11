using BusinessLayer;
using BusinessLayer.Models;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B3_Test
{
    [TestClass]
    public class PublisherManager_Tests
    {
        private PublisherManager manager;

        [TestInitialize]
        public void Setup()
        {
            this.manager = new PublisherManager(new UnitOfWork("development"));
            this.manager.DeleteAll();
        }

        [TestMethod]
        public void PublisherManager_Add()
        {
            this.manager.DeleteAll();
            Publisher x = new Publisher("Lannoo");
            this.manager.Add(x);
            Assert.AreEqual(1, this.manager.GetAll().Count);
            this.manager.DeleteAll();
        }

        [TestMethod]
        public void PublisherManager_Get()
        {
            this.manager.DeleteAll();
            Publisher x = new Publisher("Lannoo");
            this.manager.Add(x);
            Publisher x2 = this.manager.Get(1);
            Assert.AreEqual(x.Name, x2.Name);
            this.manager.DeleteAll();
        }

        [TestMethod]
        public void PublisherManager_GetAll()
        {
            this.manager.DeleteAll();
            Publisher x = new Publisher("Lannoo");
            Publisher y = new Publisher("Averbode");
            this.manager.Add(x);
            this.manager.Add(y);
            Assert.AreEqual(2, this.manager.GetAll().Count);
            this.manager.DeleteAll();
        }

        [TestMethod]
        public void PublisherManager_Delete()
        {
            this.manager.DeleteAll();
            Publisher x = new Publisher("Lannoo");
            Publisher y = new Publisher("Averbode");
            this.manager.Add(x);
            this.manager.Add(y);
            this.manager.Delete(2);
            Assert.AreEqual(1, this.manager.GetAll().Count);
            Publisher x2 = this.manager.Get(1);
            Assert.AreEqual("Lannoo", x2.Name);
            this.manager.DeleteAll();
        }

        [TestMethod]
        public void PublisherManager_DeleteAll()
        {
            Publisher x = new Publisher("Lannoo");
            this.manager.Add(x);
            this.manager.DeleteAll();
            Assert.AreEqual(0, this.manager.GetAll().Count);
        }

        [TestMethod]
        public void PublisherManager_Update()
        {
            Publisher x = new Publisher("Lannoo");
            this.manager.Add(x);
            Publisher x2 = this.manager.Get(1);
            Assert.AreEqual("Lannoo", x2.Name);
            x2.SetName("Averbode");
            this.manager.Update(x2);
            Publisher x3 = this.manager.Get(1);
            Assert.AreEqual("Averbode", x3.Name);
            this.manager.DeleteAll();
        }
    }
}
