namespace TeamUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using AutoMapper.QueryableExtensions;

    using TeamUp.Data;
    using TeamUp.Web.Helpers;
    using TeamUp.Web.Models;
    using TeamUp.Models;

    public class InviteController : BaseAuthorizeController
    {
        private static IQueryable<TeamUpUser> searchedForUsers;

        public InviteController(ITeamUpData data)
            : base(data)
        {

        }

        // GET: Invite?projectId={projectId}&userId={userId}
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

            InviteUserViewModelGet model = new InviteUserViewModelGet();
            var categories = SkillAndCategoryHelper.GetCategoriesFromDbAsCheckBoxes(this.Data);
            var skills = SkillAndCategoryHelper.GetSkillsFromDbAsCheckBoxes(this.Data);

            model.AdditionalCategories = new AdditionalCategoryAndSkillModel(categories, skills);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(InviteUserViewModelPost model)
        {
            if (model.Email == null)
            {
                model.Email = string.Empty;
            }
            var users = this.Data.Users
                                .All()
                                .Where(u => u.Email.Contains(model.Email) && u.Id != this.CurrentUser.Id);
            if (model.ProgrammingCategories != null)
            {
                foreach (var category in model.ProgrammingCategories)
                {
                    var categoryInDb = this.Data.ProgrammingCategories.All().First(c => c.Name == category);
                    users = users.Where(u => u.ProgrammingCategories.Any(pc => pc.Id == categoryInDb.Id));
                }
            }

            if (model.Skills != null)
            {
                foreach (var skill in model.Skills)
                {
                    var skillInDb = this.Data.Skills.All().First(s => s.Name == skill);

                    users = users.Where(u => u.Skills.Any(s => s.Id == skillInDb.Id));

                }
            }

            searchedForUsers = users;

            return RedirectToAction("SearchResults");
        }

        public ActionResult SearchResults()
        {
            return View();
        }

        // Kendo UI's grid gets data from here
        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = searchedForUsers
                        .Project().To<GridUserViewModel>()
                        .ToDataSourceResult(request);
            return this.Json(users);
        }
    }
}