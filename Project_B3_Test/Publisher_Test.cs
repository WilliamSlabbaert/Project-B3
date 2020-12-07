//using BusinessLayer;
//using DataLayer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using FluentAssertions;
//using System;

//namespace Project_B3_Test
//{
//    [TestClass]
//    public class Publisher_Test
//    {
//        private PublisherManager PM;

//        [TestInitialize]
//        public void SetUp()
//        {
//            PM = new PublisherManager(new UnitOfWork("test"));
//            PM.DeleteAll();
//        }

//        //---------------------------------------------------------------------------
//        //              Testen van database funcionalitijd:
//        //---------------------------------------------------------------------------
//        [TestMethod]
//        public void PublisherManager_add3_correctPublisherManager()
//        {
//            Publisher tempPublisher1 = new Publisher(0,"William1");
//            Publisher tempPublisher2 = new Publisher(1,"William2");
//            Publisher tempPublisher3 = new Publisher(2,"William3");

//            PM.Add(tempPublisher1);
//            PM.Add(tempPublisher2);
//            PM.Add(tempPublisher3);

//            PM.GetAll().Should().NotBeEmpty().And.HaveCount(3);
//            //Checking tempPublisher1:
//            Publisher DB_tempComicStrip1 = (Publisher)PM.GetById(tempPublisher1.ID);
//            DB_tempComicStrip1.Name.Should().Be(tempPublisher1.Name);
//            //Checking tempPublisher2:
//            Publisher DB_tempComicStrip2 = (Publisher)PM.GetById(tempPublisher2.ID);
//            DB_tempComicStrip2.Name.Should().Be(tempPublisher2.Name);
//            //Checking tempPublisher3:
//            Publisher DB_tempComicStrip3 = (Publisher)PM.GetById(tempPublisher3.ID);
//            DB_tempComicStrip3.Name.Should().Be(tempPublisher3.Name);

//        }
//        [TestMethod]
//        public void PublisherManager_Delete_OnlyOne()
//        {
//            Publisher tempPublisher1 = new Publisher(0, "William1");
//            Publisher tempPublisher2 = new Publisher(1, "William2");
//            Publisher tempPublisher3 = new Publisher(2, "William3");
//            PM.Add(tempPublisher1);
//            PM.Add(tempPublisher2);
//            PM.Add(tempPublisher3);


//            PM.DeleteById(0);

//            PM.GetAll().Should().NotBeEmpty().And.HaveCount(2);
//            //Checking tempPublisher2:
//            Publisher DB_tempComicStrip2 = (Publisher)PM.GetById(tempPublisher2.ID);
//            DB_tempComicStrip2.Name.Should().Be(tempPublisher2.Name);
//            //Checking tempPublisher3:
//            Publisher DB_tempComicStrip3 = (Publisher)PM.GetById(tempPublisher3.ID);
//            DB_tempComicStrip3.Name.Should().Be(tempPublisher3.Name);
//        }
//        [TestMethod]
//        public void PublisherManager_Delete_all()
//        {
//            Publisher tempPublisher1 = new Publisher("William1");
//            Publisher tempPublisher2 = new Publisher("William2");
//            Publisher tempPublisher3 = new Publisher("William3");
//            PM.Add(tempPublisher1);
//            PM.Add(tempPublisher2);
//            PM.Add(tempPublisher3);


//            PM.DeleteAll();

//            PM.GetAll().Should().BeNullOrEmpty();
//        }
//        //---------------------------------------------------------------------------
//        //              Testen van classe logica:
//        //---------------------------------------------------------------------------
//        [TestMethod]
//        public void PublisherManager_create_correctPublisher()
//        {
//            Action act = () => { Publisher tempPublisher = new Publisher("William"); };

//            act.Should().NotThrow();
//        }
//        [TestMethod]
//        public void PublisherManager_create_Publisher_withNoName()
//        {
//            Action act = () => { Publisher tempPublisher = new Publisher(""); };

//            act.Should().Throw<ArgumentNullException>();
//        }
//    }
//}
