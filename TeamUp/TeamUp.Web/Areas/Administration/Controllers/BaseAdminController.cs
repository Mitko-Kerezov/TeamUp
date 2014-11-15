namespace TeamUp.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using TeamUp.Data;
    using TeamUp.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public abstract class BaseAdminController : BaseController
    {
        public BaseAdminController(ITeamUpData data)
            : base(data)
        {

        }
    }
}