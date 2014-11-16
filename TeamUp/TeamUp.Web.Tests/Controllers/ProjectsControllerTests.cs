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
    using TeamUp.Data.Repositories;
    using TeamUp.Models;
    using TeamUp.Web.Tests.Helpers;
    using TeamUp.Web.Models;

    [TestClass]
    public class ProjectsControllerTests
    {
        [TestMethod]
        public void ProjectsCreateProjectShouldReturnViewWithWrongModelState()
        {
            // Arrange
            var data = Mock.Create<ITeamUpData>();
            var controller = ControllerHelpers.GetSetuppedController<ProjectsController>(data);
            var wrongModel = new CreateProjectViewModel();
            controller.ModelState.AddModelError("key", "value");

            // Act
            var result = controller.CreateProject(wrongModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProjectsCreateProjectShouldReturnRedirectWithRightModelState()
        {
            // Arrange
            var data = Mock.Create<ITeamUpData>();
            var controller = ControllerHelpers.GetSetuppedController<ProjectsController>(data);
            var wrongModel = new CreateProjectViewModel();

            // Act
            var result = controller.CreateProject(wrongModel) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProjectsCreateProjectShouldCreateProjectWithRightModelState()
        {
            // Arrange
            var projectForDb = new Project { };
            var repo = Mock.Create<IGenericRepository<Project>>();
            List<Project> projectsDatabase = new List<Project>();

            Mock.Arrange(() => repo.All())
                .Returns(() => projectsDatabase.AsQueryable());

            Mock.Arrange(() => repo.Add(Arg.IsAny<Project>()))
                .DoInstead(() => projectsDatabase.Add(projectForDb));

            var data = Mock.Create<ITeamUpData>();

            Mock.Arrange(() => data.Projects)
                .Returns(() => repo);

            var controller = ControllerHelpers.GetSetuppedController<ProjectsController>(data);
            var wrongModel = new CreateProjectViewModel()
            {
                Name = "Test project",
                Description = "Test description"
            };

            // Act
            var result = controller.CreateProject(wrongModel) as RedirectToRouteResult;

            // Assert
            Assert.AreNotEqual(0, projectsDatabase.Count);
        } 
    }
}
