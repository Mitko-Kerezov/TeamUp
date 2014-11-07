namespace TeamUp.Web.Controllers
{
    using System.Web.Mvc;

    using TeamUp.Data;

    public class BaseController : Controller
    {
        private ITeamUpData data;

        public BaseController()
        {
            this.data = new TeamUpData(new TeamUpDbContext());
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
