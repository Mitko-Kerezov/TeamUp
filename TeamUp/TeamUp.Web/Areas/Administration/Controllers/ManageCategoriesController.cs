namespace TeamUp.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using TeamUp.Data;
    using TeamUp.Web.Areas.Administration.Models;

    using Model = TeamUp.Models.ProgrammingCategory;
    using ViewModel = TeamUp.Web.Areas.Administration.Models.ProgrammingCategoryViewModel;

    public class ManageCategoriesController : KendoGridAdministrationController
    {
        public ManageCategoriesController(ITeamUpData data)
            : base(data)
        {

        }

        // GET: Administration/ManageCategories
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.ProgrammingCategories.All().Project().To<ProgrammingCategoryViewModel>();
        }
        protected override T GetById<T>(object id)
        {
            return this.Data.ProgrammingCategories.GetById(id) as T;
        }

        protected override T GetByName<T>(string name)
        {
            return this.Data.ProgrammingCategories.All().FirstOrDefault(s => s.Name == name) as T;
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