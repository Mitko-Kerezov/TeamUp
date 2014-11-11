using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TeamUp.Web.Models;
using TeamUp.Models;
using TeamUp.Data;

namespace TeamUp.Web.Controllers
{
    public class ManageController : BaseAuthorizeController
    {
        public ManageController(ITeamUpData data)
            : base(data)
        {
        }

        public ManageController(ITeamUpData data, ApplicationUserManager userManager)
            : this(data)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.ManageCategoriesSuccess ? "Your categories have been modified."
                : message == ManageMessageId.ManageSkillsSuccess ? "Your skills have been modified."
                : message == ManageMessageId.ChangeThemeSuccess ? "Your theme has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            return View();
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/ChangeCategories
        public ActionResult ChangeCategories()
        {
            var userCategories = this.CurrentUser.ProgrammingCategories.ToList();
            var categories = this.Data.ProgrammingCategories
                                .All()
                                .Select(c => new CheckBoxItem()
                                {
                                    Text = c.Name,
                                    Value = c.Name,
                                    Selected = false
                                })
                                .ToList();
            categories.ForEach(c => c.Selected = userCategories.FirstOrDefault(uc => uc.Name == c.Text) == null ? false : true);
            
            var model = new ChangeCategoriesModel(categories);
            return View(model);
        }

        //
        // POST: /Manage/ChangeCategories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeCategories(ChangeCategoriesPostModel model)
        {
            foreach (var category in this.CurrentUser.ProgrammingCategories)
            {
                category.Users.Remove(this.CurrentUser);

            }
            
            if (model.ProgrammingCategories != null)
            {
                foreach (var categoryName in model.ProgrammingCategories)
                {
                    var categoryInDb = this.Data.ProgrammingCategories.All().First(c => c.Name == categoryName);
                    categoryInDb.Users.Add(this.CurrentUser);
                    this.CurrentUser.ProgrammingCategories.Add(categoryInDb);
                }
            }

            this.Data.SaveChanges();
            return RedirectToAction("Index", new { Message = ManageMessageId.ManageCategoriesSuccess });
        }

        //
        // GET: /Manage/ChangeSkills
        public ActionResult ChangeSkills()
        {
            var userSkills = this.CurrentUser.Skills.ToList();
            var skills = this.Data.Skills
                                .All()
                                .Select(c => new CheckBoxItem()
                                {
                                    Text = c.Name,
                                    Value = c.Name,
                                    Selected = false
                                })
                                .ToList();
            skills.ForEach(c => c.Selected = userSkills.FirstOrDefault(uc => uc.Name == c.Text) == null ? false : true);

            var model = new ChangeSkillsModel(skills);
            return View(model);
        }

        //
        // POST: /Manage/ChangeSkills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeSkills(ChangeSkillsPostModel model)
        {
            foreach (var skill in this.CurrentUser.Skills)
            {
                skill.Users.Remove(this.CurrentUser);
            }

            if (model.Skills != null)
            {
                foreach (var skillName in model.Skills)
                {
                    var skillInDb = this.Data.Skills.All().First(c => c.Name == skillName);
                    skillInDb.Users.Add(this.CurrentUser);
                    this.CurrentUser.Skills.Add(skillInDb);
                }
            }

            this.Data.SaveChanges();
            return RedirectToAction("Index", new { Message = ManageMessageId.ManageSkillsSuccess });
        }

        //
        // GET: /Manage/ChangeTheme
        public ActionResult ChangeTheme()
        {
            return View(this.CurrentUser);
        }

        //
        // POST: /Manage/ChangeTheme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeTheme(int ThemeChosen)
        {
            this.CurrentUser.ThemeChosen = (ThemeChoice)ThemeChosen;
            this.Data.SaveChanges();
            return RedirectToAction("Index", new { Message = ManageMessageId.ChangeThemeSuccess });
        }

#region Helpers
        
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(TeamUpUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            ManageCategoriesSuccess,
            ManageSkillsSuccess,
            ChangeThemeSuccess,
            Error
        }

#endregion
    }
}