using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Project_B3_Test
{
    [TestClass]
    public class Author_Test
    {
        private AuthorManager AM;

        [TestInitialize]
        public void SetUp()
        {
            AM = new AuthorManager(new UnitOfWork("test"));
            AM.DeleteAll();
        }

        //---------------------------------------------------------------------------
        //              Testen van database funcionalitijd:
        //---------------------------------------------------------------------------
        [TestMethod]
        public void PublisherManager_add3_correctPublisherManager()
        {
            Author tempAuthor1 = new Author(0, "William1", "Slabbaert");
            Author tempAuthor2 = new Author(1, "William2", "Slabbaert");
            Author tempAuthor3 = new Author(2, "William3", "Slabbaert");

            AM.Add(tempAuthor1);
            AM.Add(tempAuthor2);
            AM.Add(tempAuthor3);

            AM.GetAll().Should().NotBeEmpty().And.HaveCount(3);
            //Checking tempAuthor1:
            Author DB_tempAuthor1 = (Author)AM.GetByID(tempAuthor1.ID);
            DB_tempAuthor1.Firstname.Should().Be(tempAuthor1.Firstname);
            DB_tempAuthor1.Surname.Should().Be(tempAuthor1.Surname);
            //Checking tempAuthor2:
            Author DB_tempAuthor2 = (Author)AM.GetByID(tempAuthor2.ID);
            DB_tempAuthor2.Firstname.Should().Be(tempAuthor2.Firstname);
            DB_tempAuthor2.Surname.Should().Be(tempAuthor1.Surname);
            //Checking tempAuthor3:
            Author DB_tempAuthor3 = (Author)AM.GetByID(tempAuthor3.ID);
            DB_tempAuthor3.Firstname.Should().Be(tempAuthor3.Firstname);
            DB_tempAuthor3.Surname.Should().Be(tempAuthor1.Surname);

        }
        [TestMethod]
        public void PublisherManager_Delete_OnlyOne()
        {
            Author tempAuthor1 = new Author(0, "William1", "Slabbaert");
            Author tempAuthor2 = new Author(1, "William2", "Slabbaert");
            Author tempAuthor3 = new Author(2, "William3", "Slabbaert");
            AM.Add(tempAuthor1);
            AM.Add(tempAuthor2);
            AM.Add(tempAuthor3);


            AM.DeleteByID(0);

            AM.GetAll().Should().NotBeEmpty().And.HaveCount(2);
            //Checking tempAuthor2:
            Author DB_tempAuthor2 = (Author)AM.GetByID(tempAuthor2.ID);
            DB_tempAuthor2.Firstname.Should().Be(tempAuthor2.Firstname);
            DB_tempAuthor2.Surname.Should().Be(tempAuthor1.Surname);
            //Checking tempAuthor3:
            Author DB_tempAuthor3 = (Author)AM.GetByID(tempAuthor3.ID);
            DB_tempAuthor3.Firstname.Should().Be(tempAuthor3.Firstname);
            DB_tempAuthor3.Surname.Should().Be(tempAuthor1.Surname);
        }
        [TestMethod]
        public void PublisherManager_Delete_all()
        {
            Author tempAuthor1 = new Author("William1", "Slabbaert");
            Author tempAuthor2 = new Author("William2", "Slabbaert");
            Author tempAuthor3 = new Author("William3", "Slabbaert");
            AM.Add(tempAuthor1);
            AM.Add(tempAuthor2);
            AM.Add(tempAuthor3);


            AM.DeleteAll();

            AM.GetAll().Should().BeNullOrEmpty();
        }
        //---------------------------------------------------------------------------
        //              Testen van classe logica:
        //---------------------------------------------------------------------------
        [TestMethod]
        public void Author_create_correctAuthor()
        {
            Action act = () => { Author tempAuthor = new Author("William1", "Slabbaert"); };

            act.Should().NotThrow();
        }
        [TestMethod]
        public void Author_create_Author_withNoFirstname()
        {
            Action act = () => { Author tempAuthor = new Author("", "Slabbaert"); };

            act.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void Author_create_Author_withNoSurname()
        {
            Action act = () => { Author tempAuthor = new Author("William1", ""); };

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
