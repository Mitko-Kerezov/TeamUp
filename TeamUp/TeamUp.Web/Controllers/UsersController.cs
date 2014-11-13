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
    using System.Linq.Expressions;
    using TeamUp.Models;

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

        
        // Kendo UI's grid gets data from here
        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.Data.Users
                            .All()
                            .Project().To<GridUserViewModel>()
                            .ToDataSourceResult(request);
            return this.Json(users);
        }
    }
}