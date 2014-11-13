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

    using Models;
    using TeamUp.Data;

    public class UsersController : BaseAuthorizeController
    {

        public UsersController(ITeamUpData data)
            : base(data)
        {
                
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Details/{id}
        public ActionResult Details(string id)
        {
            var userInDb = this.Data.Users.All().FirstOrDefault(u => u.Id.ToString() == id);
            if (userInDb == null)
            {
                return RedirectToAction("Index");
            }

            var user = new UserDetailsViewModel();
            user = Mapper.Map(userInDb, user);
            return View(user);
        }

        // GET: Users/Invite?projectId={projectId}&userId={userId}
        public ActionResult Invite(string projectId, string userId)
        {
            var projectInDb = this.Data.Projects.All().FirstOrDefault(p => p.Id.ToString() == projectId);
            if (projectInDb == null)
            {
                return RedirectToAction("Index");
            }

            var userInDb = this.Data.Users.All().FirstOrDefault(u => u.Id.ToString() == userId);
            if (userInDb == null)
            {
                return RedirectToAction("Index");
            }

            if (CurrentUser.Id != userInDb.Id)
            {
                return RedirectToAction("Index");
            }

            if (!projectInDb.Users.Contains(userInDb) || !userInDb.Projects.Contains(projectInDb))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.Data.Users
                            .All()
                            .Project().To<GridUserViewModel>()
                            //.Select(u => new
                            //{
                            //    Id = u.Id,
                            //    Email = u.Email,
                            //    Skills = u.Skills.Select(s => s.Name).Select(s => string.Join(" ", s)),
                            //    ProgrammingCategories = u.ProgrammingCategories.Select(p => p.Name)
                            //})
                            .ToDataSourceResult(request);
            return this.Json(users);
        }
    }
}