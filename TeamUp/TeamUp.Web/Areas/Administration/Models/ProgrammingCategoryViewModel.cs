namespace TeamUp.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class ProgrammingCategoryViewModel : AdministrationViewModel, IMapFrom<ProgrammingCategory>
    {

    }
}