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
    public class ComicStrip_Test
    {
        private ComicStripManager CM;

        [TestInitialize]
        public void SetUp()
        {
            CM = new ComicStripManager(new UnitOfWork("test"));
            CM.DeleteAll();
        }

        //---------------------------------------------------------------------------
        //              Testen van database funcionalitijd:
        //---------------------------------------------------------------------------
        [TestMethod]
        public void ComicStrip_add3_correctComicStrip()
        {
            ComicStrip tempComicStrip1 = new ComicStrip(0,"Guust1", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip2 = new ComicStrip(1,"Guust2", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip3 = new ComicStrip(2,"Guust3", "de boze heks", "komedie", 135);

            CM.AddStrip(tempComicStrip1);
            CM.AddStrip(tempComicStrip2);
            CM.AddStrip(tempComicStrip3);

            CM.GetAll().Should().NotBeEmpty().And.HaveCount(3);
            //Checking tempComicStrip1:
            ComicStrip DB_tempComicStrip1 = (ComicStrip)CM.GetById(tempComicStrip1.ID);
            DB_tempComicStrip1.Titel.Should().Be(tempComicStrip1.Titel);
            DB_tempComicStrip1.Serie.Should().Be(tempComicStrip1.Serie);
            DB_tempComicStrip1.Genre.Should().Be(tempComicStrip1.Genre);
            DB_tempComicStrip1.ComicStripNumber.Should().Be(tempComicStrip1.ComicStripNumber);
            //Checking tempComicStrip2:
            ComicStrip DB_tempComicStrip2 = (ComicStrip)CM.GetById(tempComicStrip2.ID);
            DB_tempComicStrip2.Titel.Should().Be(tempComicStrip2.Titel);
            DB_tempComicStrip2.Serie.Should().Be(tempComicStrip2.Serie);
            DB_tempComicStrip2.Genre.Should().Be(tempComicStrip2.Genre);
            DB_tempComicStrip2.ComicStripNumber.Should().Be(tempComicStrip1.ComicStripNumber);
            //Checking tempComicStrip3:
            ComicStrip DB_tempComicStrip3 = (ComicStrip)CM.GetById(tempComicStrip3.ID);
            DB_tempComicStrip3.Titel.Should().Be(tempComicStrip3.Titel);
            DB_tempComicStrip3.Serie.Should().Be(tempComicStrip3.Serie);
            DB_tempComicStrip3.Genre.Should().Be(tempComicStrip3.Genre);
            DB_tempComicStrip3.ComicStripNumber.Should().Be(tempComicStrip3.ComicStripNumber);

        }
        [TestMethod]
        public void ComicStrip_Delete_OnlyOne()
        {
            ComicStrip tempComicStrip1 = new ComicStrip(0, "Guust1", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip2 = new ComicStrip(1, "Guust2", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip3 = new ComicStrip(2, "Guust3", "de boze heks", "komedie", 135);
            CM.AddStrip(tempComicStrip1);
            CM.AddStrip(tempComicStrip2);
            CM.AddStrip(tempComicStrip3);


            CM.DeleteById(0);

            CM.GetAll().Should().NotBeEmpty().And.HaveCount(2);
            //Checking tempComicStrip2:
            ComicStrip DB_tempComicStrip2 = (ComicStrip)CM.GetById(tempComicStrip2.ID);
            DB_tempComicStrip2.Titel.Should().Be(tempComicStrip2.Titel);
            DB_tempComicStrip2.Serie.Should().Be(tempComicStrip2.Serie);
            DB_tempComicStrip2.Genre.Should().Be(tempComicStrip2.Genre);
            DB_tempComicStrip2.ComicStripNumber.Should().Be(tempComicStrip1.ComicStripNumber);
            //Checking tempComicStrip3:
            ComicStrip DB_tempComicStrip3 = (ComicStrip)CM.GetById(tempComicStrip3.ID);
            DB_tempComicStrip3.Titel.Should().Be(tempComicStrip3.Titel);
            DB_tempComicStrip3.Serie.Should().Be(tempComicStrip3.Serie);
            DB_tempComicStrip3.Genre.Should().Be(tempComicStrip3.Genre);
            DB_tempComicStrip3.ComicStripNumber.Should().Be(tempComicStrip3.ComicStripNumber);
        }
        [TestMethod]
        public void ComicStrip_Delete_all()
        {
            ComicStrip tempComicStrip1 = new ComicStrip(0, "Guust1", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip2 = new ComicStrip(1, "Guust2", "de boze heks", "komedie", 135);
            ComicStrip tempComicStrip3 = new ComicStrip(2, "Guust3", "de boze heks", "komedie", 135);
            CM.AddStrip(tempComicStrip1);
            CM.AddStrip(tempComicStrip2);
            CM.AddStrip(tempComicStrip3);


            CM.DeleteAll();

            CM.GetAll().Should().BeNullOrEmpty();
        }
        //---------------------------------------------------------------------------
        //              Testen van classe logica:
        //---------------------------------------------------------------------------
        [TestMethod]
        public void ComicStrip_create_correctComicStrip()
        {
            Action act = () => { ComicStrip tempComicStrip = new ComicStrip("Guust", "de boze heks", "komedie", 135); };

            act.Should().NotThrow();
        }
        [TestMethod]
        public void ComicStrip_create_ComicStrip_withNoTitle()
        {
            Action act = () => { ComicStrip tempComicStrip = new ComicStrip("", "de boze heks", "komedie", 135); };

            act.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void ComicStrip_create_ComicStrip_withNoSerie()
        {
            Action act = () => { ComicStrip tempComicStrip = new ComicStrip("Guust", "", "komedie", 135); };

            act.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void ComicStrip_create_ComicStrip_withNoGenre()
        {
            Action act = () => { ComicStrip tempComicStrip = new ComicStrip("Guust", "", "", 135); };

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
