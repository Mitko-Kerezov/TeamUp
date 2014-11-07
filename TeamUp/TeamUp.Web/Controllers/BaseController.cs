namespace TeamUp.Web.Controllers
{
    using System.Web.Mvc;

    using TeamUp.Data;

    public class BaseController : Controller
    {
        private ITeamUpData data;

        public BaseController(ITeamUpData data)
        {
            this.data = data;
        }

        protected ITeamUpData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}
