namespace TeamUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using TeamUp.Data;
    using TeamUp.Models;
    using TeamUp.Web.Models;
    
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
                : "";

            return View();
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
                // TODO: Better mapping
                Mapper.CreateMap<CreateProjectViewModel, Project>();
                var project = new Project();
                project = Mapper.Map(model, project);

                project.DateCreated = DateTime.Now;
                project.Users.Add(this.CurrentUser);
                this.CurrentUser.Projects.Add(project);

                this.Data.SaveChanges();

                return RedirectToAction("Index", new { Message = ProjectMessage.CreateProjectSuccess });
            }

            return View(model);
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