using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1;
using WebApplication1.Controllers;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Theory()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Article(1) as ViewResult;

            // Assert
            Assert.AreEqual("Теория", result.ViewBag.Message);
        }

        [TestMethod]
        public void Test()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Test(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetQuestListTest()
        {
            //arrange
            string ids = "1,2,3,4";
            string checkString = "";
            HomeController controller = new HomeController();

            //act
            List<int> list = controller.GetQuestList(ids);
            for (var i=0;i<list.Count;i++)
            {
                checkString += list[i].ToString();
                if (i!=(list.Count-1))
                {
                    checkString += ",";
                }
            }

            //assert
            Assert.AreEqual(checkString, ids);
        }

        [TestMethod()]
        public void GetQuestListFormatTest()
        {
            //arrange
            string ids = "1. d";
            HomeController controller = new HomeController();

            //act and assert
            Assert.ThrowsException<System.FormatException>(() => controller.GetQuestList(ids));
        }

        [TestMethod()]
        public void GetQuestListNullTest()
        {
            //arrange
            HomeController controller = new HomeController();

            //act and assert
            Assert.ThrowsException<System.NullReferenceException>(() => controller.GetQuestList(null));
        }
    }
}
