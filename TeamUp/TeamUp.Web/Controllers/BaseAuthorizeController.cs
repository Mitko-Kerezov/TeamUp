namespace TeamUp.Web.Controllers
{
    using System.Web.Mvc;

    using TeamUp.Data;

    [Authorize]
    public abstract class BaseAuthorizeController : BaseController
    {
        public BaseAuthorizeController(ITeamUpData data)
            : base(data)
        {

        }
    }
}