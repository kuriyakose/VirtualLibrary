using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryFacade;
using NUnit.Framework;

namespace LibraryFacadeTest
{
    [TestFixture]
    public class LibraryServiceTest
    {
        string message;

        [Test]
        public void Add()
        {
            var libraryService = new LibraryService();
            Assert.AreEqual(true, libraryService.Add("1,ddddddd,0", out message));
            Assert.AreEqual(true, libraryService.Add("2,cccccc,1", out message));
            Assert.AreEqual(true, libraryService.Add("3,bbbbb,2", out message));
            Assert.AreEqual(true, libraryService.Add("4,aaaa,2", out message));
            Assert.AreEqual(4, libraryService.Count());

            Assert.AreEqual(false, libraryService.Add("1,ddddddd,0", out message));
            Assert.AreEqual("Id already exist, start again with different ID", message);
            Assert.AreEqual(true, libraryService.Add("5,ddddddd,0", out message));
            Assert.AreEqual("Added", message);
            Assert.AreEqual(false, libraryService.Add("6,Invalid Type,5", out message));
            Assert.AreEqual("Invalid Type", message);
        }

        [TestCase("2,x x,2", Result = true)]
        [TestCase("1,1,1", Result = true)]
        public bool Add(string inputString)
        {
            var libraryService = new LibraryService();
            return libraryService.Add(inputString, out message);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Exception()
        {
            var libraryService = new LibraryService();
            libraryService.Add("", out message);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Exception_OneParam()
        {
            var libraryService = new LibraryService();
            libraryService.Add("1", out message);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_Exception_TwoParam()
        {
            var libraryService = new LibraryService();
            libraryService.Add("x,333", out message);
        }

        [Test]
        public void Lent()
        {
            var libraryService = new LibraryService();
            Assert.AreEqual(true, libraryService.Add("1,ddddddd,0", out message));
            Assert.AreEqual(true, libraryService.Add("2,cccccc,1", out message));
            Assert.AreEqual(true, libraryService.Add("3,bbbbb,2", out message));
            Assert.AreEqual(true, libraryService.Add("4,aaaa,2", out message));
            Assert.AreEqual(4, libraryService.Count());
            Assert.AreEqual("Lent out", libraryService.Lent("4"));
            Assert.AreEqual("Id already lent out, start again with different ID", libraryService.Lent("4"));
            Assert.AreEqual("Id not exists, start again with different ID", libraryService.Lent("5"));
        }

        [Test]
        public void CheckIn()
        {
            var libraryService = new LibraryService();
            Assert.AreEqual(true, libraryService.Add("1,ddddddd,0", out message));
            Assert.AreEqual(true, libraryService.Add("2,cccccc,1", out message));
            Assert.AreEqual(true, libraryService.Add("3,bbbbb,2", out message));
            Assert.AreEqual(true, libraryService.Add("4,aaaa,2", out message));
            Assert.AreEqual(4, libraryService.Count());
            Assert.AreEqual("Id already checked in, start again with different ID", libraryService.CheckIn("4"));
            Assert.AreEqual("Lent out", libraryService.Lent("4"));
            Assert.AreEqual("Checked In", libraryService.CheckIn("4"));
            Assert.AreEqual("Id not exists, start again with different ID", libraryService.CheckIn("5"));
        }

        [Test]
        public void LentOutItems()
        {
            var libraryService = new LibraryService();
            Assert.AreEqual(true, libraryService.Add("4,ddddddd,0", out message));
            Assert.AreEqual(null, libraryService.AllItems().FirstOrDefault().LentDate);
            Assert.AreEqual(true, libraryService.Add("2,cccccc,1", out message));
            Assert.AreEqual(true, libraryService.Add("3,bbbbb,2", out message));
            Assert.AreEqual(true, libraryService.Add("1,aaaa,2", out message));
            Assert.AreEqual(4, libraryService.Count());
            Assert.AreEqual("Id already checked in, start again with different ID", libraryService.CheckIn("4"));
            Assert.AreEqual("Lent out", libraryService.Lent("4"));
            Assert.AreEqual("Checked In", libraryService.CheckIn("4"));
            Assert.AreEqual("Id not exists, start again with different ID", libraryService.CheckIn("5"));
            Assert.AreEqual(0, libraryService.LentOutItems().Count());
            Assert.AreEqual("Lent out", libraryService.Lent("1"));
            Assert.AreEqual(1, libraryService.LentOutItems().Count());
            Assert.AreEqual("1", libraryService.LentOutItems().FirstOrDefault().Id);
            Assert.AreEqual(true, libraryService.LentOutItems().FirstOrDefault().LentDate != null);
        }

        [Test]
        public void AllItems()
        {
            var libraryService = new LibraryService();
            Assert.AreEqual(true, libraryService.Add("1,ddddddd,2", out message));
            Assert.AreEqual(true, libraryService.Add("2,bbbbb,2", out message));
            Assert.AreEqual(true, libraryService.Add("3,bbbbb,0", out message));
            Assert.AreEqual(true, libraryService.Add("4,aaaa,2", out message));
            Assert.AreEqual(4, libraryService.Count());
            Assert.AreEqual("Id already checked in, start again with different ID", libraryService.CheckIn("4"));
            Assert.AreEqual("Lent out", libraryService.Lent("4"));
            Assert.AreEqual("Checked In", libraryService.CheckIn("4"));
            Assert.AreEqual("Id not exists, start again with different ID", libraryService.CheckIn("5"));
            Assert.AreEqual(0, libraryService.LentOutItems().Count());
            Assert.AreEqual("Lent out", libraryService.Lent("1"));
            Assert.AreEqual(4, libraryService.AllItems().Count());
            Assert.AreEqual("3", libraryService.AllItems().FirstOrDefault().Id);
            Assert.AreEqual("4", libraryService.AllItems().ElementAt(1).Id);
        }
    }
}
