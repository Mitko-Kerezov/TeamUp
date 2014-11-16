namespace TeamUp.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Telerik.JustMock;

    using TeamUp.Web;
    using TeamUp.Web.Controllers;
    using TeamUp.Data;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexReturnsNonNullView()
        {
            // Arrange
            var data = Mock.Create<ITeamUpData>();
            var controller = new HomeController(data);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        } 
    }
}
