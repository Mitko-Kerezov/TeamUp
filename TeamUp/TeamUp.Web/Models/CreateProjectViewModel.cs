namespace TeamUp.Web.Models
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class CreateProjectViewModel : IMapFrom<Project>
    {
        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}