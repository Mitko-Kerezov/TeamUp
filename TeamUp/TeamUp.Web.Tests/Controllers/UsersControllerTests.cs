namespace TeamUp.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Net.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AutoMapper;

    using Telerik.JustMock;

    using TeamUp.Web;
    using TeamUp.Web.Controllers;
    using TeamUp.Data;
    using TeamUp.Data.Repositories;
    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;
    using TeamUp.Web.Tests.Helpers;

    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void UsersIndexReturnsNonNullView()
        {
            // Arrange
            var data = Mock.Create<ITeamUpData>();
            var controller = new UsersController(data);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UsersDetailsWithInvalidIdShouldReturnRedirect()
        {
            // Arrange
            var data = Mock.Create<ITeamUpData>();
            var controller = new UsersController(data);

            // Act
            var result = controller.Details(null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UsersDetailsWithValidIdShouldReturnView()
        {
            // Arrange
            var repo = Mock.Create<IGenericRepository<TeamUpUser>>();
            TeamUpUser[] users = 
            {
                new TeamUpUser()
                {
                    Id="testId",
                    Email = "test"
                }
            };

            Mock.Arrange(() => repo.All())
                .Returns(() => users.AsQueryable());

            var data = Mock.Create<ITeamUpData>();

            Mock.Arrange(() => data.Users)
                .Returns(() => repo);

            var controller = ControllerHelpers.GetSetuppedController<UsersController>(data);

            // Act
            var result = controller.Details("testId") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        
    }
}
