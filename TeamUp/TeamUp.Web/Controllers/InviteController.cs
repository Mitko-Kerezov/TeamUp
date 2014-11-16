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
    using TeamUp.Web.Models.Invitations;

    public class InviteController : BaseAuthorizeController
    {
        private static IQueryable<TeamUpUser> searchedForUsers;

        public InviteController(ITeamUpData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var model = this.CurrentUser.MyInvitations
                        .AsQueryable()
                        .Project().To<ShowInvitationViewModel>()
                        .OrderBy(i => i.ShouldShowButton)
                        .ThenByDescending(i => i.DateSent);

            return View(model);
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

            TempData["projectId"] = projectId;

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

            var projectInDb = this.Data.Projects.All().FirstOrDefault(p => p.Id.ToString() == model.ProjectId);
            if (projectInDb == null)
            {
                return RedirectToAction("Index");
            }

            users = users.Where(u => u.Projects.Count() == 0 || u.Projects.Any(p => p.Id != projectInDb.Id));

            searchedForUsers = users;

            return RedirectToAction("SearchResults", new {projectId = model.ProjectId});
        }

        public ActionResult SearchResults(string projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        // GET: SendInvitation?projectId={projectId}&userId={userId}
        public ActionResult SendInvitation(string projectId, string userId)
        {
            ViewBag.ProjectId = projectId;

            var projectInDb = this.Data.Projects.All().FirstOrDefault(p => p.Id.ToString() == projectId);
            if (projectInDb == null)
            {
                ViewBag.ErrorMessage = "Invalid project";
                return View("SearchResults");
            }

            var invitationRecipient = this.Data.Users.All().FirstOrDefault(u => u.Id.ToString() == userId);
            if (invitationRecipient == null)
            {
                ViewBag.ErrorMessage = "Invalid recipient";
                return View("SearchResults");
            }

            if (CurrentUser.Id == invitationRecipient.Id)
            {
                ViewBag.ErrorMessage = "You cannot send invitations to yourself";
                return View("SearchResults");
            }

            if (!projectInDb.Users.Contains(this.CurrentUser) || !this.CurrentUser.Projects.Contains(projectInDb))
            {
                ViewBag.ErrorMessage = "You are not part of this project";
                return View("SearchResults");
            }

            if (projectInDb.Users.Contains(invitationRecipient))
            {
                ViewBag.ErrorMessage = "User is already part of the project";
                return View("SearchResults");
            }

            var invitation = new Invitation() { 
                AuthorId = this.CurrentUser.Id,
                RecipientId = invitationRecipient.Id,
                Project = projectInDb
            };

            this.Data.Invitations.Add(invitation);
            invitationRecipient.MyInvitations.Add(invitation);
            this.Data.SaveChanges();

            ViewBag.StatusMessage = "Invitation successfully sent";
            return View("SearchResults");
        }

        [HttpPost]
        public ActionResult AcceptInvitation(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var invitationInDb = this.Data.Invitations.GetById(id);
                if (invitationInDb.InvitationResponse == InvitationResponseType.NotRead)
                {
                    invitationInDb.InvitationResponse = InvitationResponseType.Accepted;
                    invitationInDb.Project.Users.Add(this.CurrentUser);
                    this.CurrentUser.Projects.Add(invitationInDb.Project);
                    this.Data.SaveChanges();
                }

                return new EmptyResult();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RejectInvitation(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var invitationInDb = this.Data.Invitations.GetById(id);
                if (invitationInDb.InvitationResponse == InvitationResponseType.NotRead)
                {
                    invitationInDb.InvitationResponse = InvitationResponseType.Rejected;
                    this.Data.SaveChanges();
                }

                return new EmptyResult();
            }

            return RedirectToAction("Index");
        }

        // Kendo UI's grid gets data from here
        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            if (searchedForUsers != null)
            {
                var users = searchedForUsers
                        .Project().To<GridUserViewModel>()
                        .ToDataSourceResult(request);

                return this.Json(users);
            }

            return this.Json(null);
        }
    }
}