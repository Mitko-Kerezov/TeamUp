namespace TeamUp.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using System.Data.Entity;

    using TeamUp.Data;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using AutoMapper;
    using TeamUp.Web.Areas.Administration.Models;
    
    public abstract class KendoGridAdministrationController : BaseAdminController
    {
        public KendoGridAdministrationController(ITeamUpData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();
        protected abstract T GetById<T>(object id) where T : class;

        protected abstract T GetByName<T>(string name) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data =
                this.GetData()
                .ToDataSourceResult(request);
            return this.Json(data);
        }

        [NonAction]
        protected virtual T Create<T>(object model, string uniqueName) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                if (this.IsUnique<T>(uniqueName))
                {
                    var dbModel = Mapper.Map<T>(model);
                    this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                    return dbModel;
                }
            }

            return null;
        }

        [NonAction]
        protected virtual void Destroy<TModel, TViewModel>(TViewModel model)
            where TModel : class
            where TViewModel : AdministrationViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(model.Id.Value);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Deleted);
            }
        }
        
        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model)
            where TModel : class
            where TViewModel : AdministrationViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                if (this.IsUnique<TModel>(model.Name))
                {
                    var dbModel = this.GetById<TModel>(model.Id.Value);
                    Mapper.Map<TViewModel, TModel>(model, dbModel);
                    this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                }
            }
        }
        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }

        private bool IsUnique<T>(string uniqueName) where T : class
        {
            if (this.GetByName<T>(uniqueName) != null)
            {
                ModelState.AddModelError("Unique", "Name must be unique");
                return false;
            }

            return true;
        }
    }
}