﻿namespace TeamUp.Web.Controllers
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
