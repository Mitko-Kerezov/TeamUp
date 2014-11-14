namespace TeamUp.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using TeamUp.Data;
    using TeamUp.Web.Areas.Administration.Models;

    using Model = TeamUp.Models.Skill;
    using ViewModel = TeamUp.Web.Areas.Administration.Models.SkillViewModel;

    public class ManageSkillsController : KendoGridAdministrationController
    {
        public ManageSkillsController(ITeamUpData data)
            : base(data)
        {

        }

        // GET: Administration/Skills
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Skills.All().Project().To<SkillViewModel>();
        }
        protected override T GetById<T>(object id)
        {
            return this.Data.Skills.GetById(id) as T;
        }

        protected override T GetByName<T>(string name)
        {
            return this.Data.Skills.All().FirstOrDefault(s => s.Name == name) as T;
        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model, model.Name);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }
        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model);
            return this.GridOperation(model, request);
        }
        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy<Model, ViewModel>(model);
            return this.GridOperation(model, request);
        }
    }
}