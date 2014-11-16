namespace TeamUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using TeamUp.Data;
    using TeamUp.Models;
    using TeamUp.Web.Models;
    using TeamUp.Web.Models.Projects;

    public class ProjectsController : BaseAuthorizeController
    {
        public ProjectsController(ITeamUpData data)
            : base(data)
        {

        }

        public ActionResult Index(ProjectMessage? message)
        {
            ViewBag.StatusMessage =
                message == ProjectMessage.CreateProjectSuccess ? "Project created successfully."
                : message == ProjectMessage.Error ? "An error has occurred."
                : null;

            return View();
        }

        // GET: SeeMine
        public ActionResult SeeMine()
        {
            var model = this.CurrentUser.Projects;
            return View(model);
        }

        // GET: CreateProject
        public ActionResult CreateProject()
        {
            var model = new CreateProjectViewModel();
            return View(model);
        }

        // POST: CreateProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = new Project();
                project = Mapper.Map(model, project);

                project.Users.Add(this.CurrentUser);
                this.CurrentUser.Projects.Add(project);
                this.Data.Projects.Add(project);

                this.Data.SaveChanges();

                return RedirectToAction("Index", new { Message = ProjectMessage.CreateProjectSuccess });
            }

            return View(model);
        }

        // GET: Projects/Details/{id}
        public ActionResult Details(string id)
        {
            var projectInDb = this.Data.Projects.All().FirstOrDefault(p => p.Id.ToString() == id);
            if (projectInDb == null)
            {
                return RedirectToAction("Index");
            }

            var project = new ProjectDetailsViewModel();
            project = Mapper.Map(projectInDb, project);

            ViewBag.CanInvite = false;

            if (project.Users.Contains(this.CurrentUser))
            {
                ViewBag.CanInvite = true;
                ViewBag.CurrentUserId = this.CurrentUser.Id;
            }

            return View(project);
        }

        // GET: Projects/All
        public ActionResult All()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.Data.Projects
                            .All()
                            .Project().To<GridProjectViewModel>()
                            .ToDataSourceResult(request);
            return this.Json(users);
        }

        #region Helpers

        public enum ProjectMessage
        {
            CreateProjectSuccess,
            Error
        }

        #endregion
    }
}