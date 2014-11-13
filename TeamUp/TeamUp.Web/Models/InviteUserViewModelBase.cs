namespace TeamUp.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InviteUserViewModelBase
    {
        [Display(Name = "Email")]
        [UIHint("SingleLineText")]
        public string Email { get; set; }
    }
}