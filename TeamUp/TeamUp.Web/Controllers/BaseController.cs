namespace TeamUp.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    using TeamUp.Data;
    using TeamUp.Models;
    using System;
    using System.Web.Routing;

    public abstract class BaseController : Controller
    {
        private ITeamUpData data;
        private TeamUpUser currentUser;

        public BaseController(ITeamUpData data)
        {
            this.data = data;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            // Work with data before BeginExecute to prevent "NotSupportedException: A second operation started on this context before a previous asynchronous operation completed."
            var currentUserId = requestContext.HttpContext.User.Identity.GetUserId();
            if (this.CurrentUser == null || this.CurrentUser.Id != currentUserId)
            {
                this.CurrentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == currentUserId);
            }

            // Calling BeginExecute before PrepareSystemMessages for the TempData to has values
            var result = base.BeginExecute(requestContext, callback, state);
            this.ViewBag.ThemeChosen = this.CurrentUser == null ? ThemeChoice.Default : this.CurrentUser.ThemeChosen;
            if (this.CurrentUser != null)
            {
                var unreadMessagesCount = this.CurrentUser.MyMessages.Count(m => m.IsRead == false);
                if (unreadMessagesCount > 0)
                {
                    this.ViewBag.UnreadMessagesCount = unreadMessagesCount;
                }

                var unreadInvitationsCount = this.CurrentUser.MyInvitations.Count(i => i.InvitationResponse == InvitationResponseType.NotRead);
                if (unreadInvitationsCount > 0)
                {
                    this.ViewBag.UnreadInvitationsCount = unreadInvitationsCount;
                }
            }

            return result;
        }

        protected ITeamUpData Data
        {
            get
            {
                return this.data;
            }
        }

        protected TeamUpUser CurrentUser
        {
            get
            {
                return this.currentUser;
            }
            private set
            {
                this.currentUser = value;
            }
        }
    }
}
