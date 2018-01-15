using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchieBirds.Controllers;
using System.Web.Mvc;
using System.Text;

namespace ArchieBirds.Tests
{
    [TestClass]
    public class ArchiesControllerTests
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("This is a simple demo application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Create()
        {
            ArchiesController controller = new ArchiesController();
            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
