namespace TeamUp.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InviteUserViewModelPost : InviteUserViewModelBase
    {
        public string[] ProgrammingCategories { get; set; }

        public string[] Skills { get; set; }

        [Required]
        public string ProjectId { get; set; }
    }
}