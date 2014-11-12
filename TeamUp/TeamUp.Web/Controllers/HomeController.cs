namespace TeamUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    using TeamUp.Data;
    using TeamUp.Models;

    public class HomeController : BaseController
    {
        public HomeController(ITeamUpData data)
            : base(data)
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}